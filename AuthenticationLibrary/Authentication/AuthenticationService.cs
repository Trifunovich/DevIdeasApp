using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationLibrary.Context;
using AuthenticationLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationLibrary.Authentication
{
  class AuthenticationService : IAuthenticationService
  {
    private readonly UserManager<AuthenticationUserModel> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtParamsInjector _paramInjector;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
      UserManager<AuthenticationUserModel> userManager,
      RoleManager<IdentityRole> roleManager,
      IJwtParamsInjector paramInjector,
      ApplicationDbContext context,
      ILogger<AuthenticationService> logger)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _paramInjector = paramInjector;
      _context = context;
      _logger = logger;

      //context.Database.EnsureCreated();
      context.Database.Migrate();
    }

    public async Task<ResponseModel> TryToLogin(LoginModel model)
    {
      var user = await _userManager.FindByNameAsync(model.Username);

      if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
      {
        return new ResponseModel(ResponseType.Successful) {ResponseData = await GetTokensForUser(user)};
      }

      _logger.LogError("Cannot find user {user}", model.Username);
      return new ResponseModel(ResponseType.UserDoesntExist);
    }

    private async Task<ResponseModel> GetTokensForUser(AuthenticationUserModel user)
    {
      _logger.LogTrace("User {user} found", user.UserName);
      var userRoles = await _userManager.GetRolesAsync(user);

      var authClaims = new List<Claim>
        {
          new("id", user.Id),
          new(ClaimTypes.Name, user.UserName),
          new(ClaimTypes.Email, user.Email),
          new(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

      foreach (var userRole in userRoles)
      {
        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
      }

      _logger.LogTrace("Claims loaded for user {user}", user.UserName);

      var token = await CreateJwtToken(authClaims);
     

      _logger.LogTrace("Token defined for user {user}", user.UserName);

      dynamic result = null;
      
      var existingRefTokens = _context.RefreshModels.Where(r => r.AuthenticationUserModelId == user.Id).ToList();

      //we cannot put it in the same query since the valid is readonly and not in the database
      var existingRefToken = existingRefTokens.FirstOrDefault(r => r.IsValid);

      if (existingRefToken is null)
      {
        //deleting all of the refresh tokens, since they are all expired
        await LogOut(user.Id);

        var refreshToken = await CreateJwtToken(null);
        string textRefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
       
        //adding the new one
        var refModel = new RefreshModel(refreshToken.ValidTo) { RefreshToken = textRefreshToken, AuthenticationUserModelId = user.Id };
        _context.RefreshModels.Add(refModel);
        await _context.SaveChangesAsync();
        _logger.LogTrace("Refresh token written for user {user}, which expires on {expTime}", user.UserName, refreshToken.ValidTo);

        result = new
        {
          token = new JwtSecurityTokenHandler().WriteToken(token),
          refreshToken = refModel.RefreshToken,
          expiration = token.ValidTo,
          refreshExpiration = refModel.ExpiresOn
        };
      }
      else
      {
        result = new
        {
          token = new JwtSecurityTokenHandler().WriteToken(token),
          refreshToken = existingRefToken.RefreshToken,
          expiration = token.ValidTo,
          refreshExpiration = existingRefToken.ExpiresOn
        };
      }

      _logger.LogTrace("Token created for user {user}", user.UserName);

      return new ResponseModel(ResponseType.Successful) { ResponseData = result };
    }

    private Task<JwtSecurityToken> CreateJwtToken(List<Claim> claims)
    {
      bool isRefreshToken = claims is null;
      JwtSecurityToken token;
      if (isRefreshToken)
      {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_paramInjector.RefreshSecret));
        token = new JwtSecurityToken(
          issuer: _paramInjector.ValidIssuer,
          audience: _paramInjector.ValidAudience,
          expires: _paramInjector.RefreshExpires,
          signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

      }
      else
      {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_paramInjector.Secret));
        token = new JwtSecurityToken(
          issuer: _paramInjector.ValidIssuer,
          audience: _paramInjector.ValidAudience,
          expires: _paramInjector.Expires,
          claims: claims,
          signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
      }

      return Task.FromResult(token);
    }

    public async Task<ResponseModel> Register(RegisterModel model)
    {
      return await RegisterUserAndRole(model, UserRole.User);
    }

    public async Task<ResponseModel> RegisterAdmin(RegisterModel model)
    {
      return await RegisterUserAndRole(model, UserRole.Admin);
    }
    
    public async Task<ResponseModel> TryToRefresh(string refreshToken)
    {
      var tokensWithUser =
        _context.RefreshModels.Where(t => t.RefreshToken.Equals(refreshToken)).ToList();

      string userId = tokensWithUser?.FirstOrDefault()?.AuthenticationUserModelId;

      if (string.IsNullOrEmpty(userId))
      {
        _logger.LogError($"Cannot find user with token {refreshToken}", refreshToken);
        return new ResponseModel(ResponseType.UserDoesntExist);
      }

      var token = tokensWithUser?.FirstOrDefault(t => t.AuthenticationUserModelId.Equals(userId) && t.IsValid);

      if (token is null)
      {
        _logger.LogError($"Cannot find credentials");
        return new ResponseModel(ResponseType.CredentialsMissing);
      }

      var user = _context?.Users?.Find(userId);

      return await GetTokensForUser(user);
    }

    public async Task<ResponseModel> LogOut(string id)
    {
      try
      {
        if (string.IsNullOrEmpty(id))
        {
          return new ResponseModel(ResponseType.UserDoesntExist);
        }

        var whatToRemove = _context?.RefreshModels.Where(rm => rm.AuthenticationUserModelId != null && rm.AuthenticationUserModelId.Equals(id));
        
        if (_context is not null && whatToRemove is not null && whatToRemove.Any())
        {
          _context.RemoveRange(whatToRemove);
          await _context.SaveChangesAsync();
        }

        return new ResponseModel(ResponseType.LoggedOut);
      }
      catch (Exception e)
      {
        return new ResponseModel(ResponseType.OperationFailed);
      }
    }

    private async Task<ResponseModel> RegisterUserAndRole(RegisterModel model, UserRole role)
    {
      var userExists = await _userManager.FindByNameAsync(model.Username);

      if (userExists != null)
      {
        _logger.LogError("User {user} already registered", model.Username);
        return new ResponseModel(ResponseType.UserExists);
      }

      AuthenticationUserModel user = new AuthenticationUserModel()
      {
        Email = model.Email,
        SecurityStamp = Guid.NewGuid().ToString(),
        UserName = model.Username
      };

      var result = await _userManager.CreateAsync(user, model.Password);

      if (!result.Succeeded)
      {
        _logger.LogError("Failed to create user {user}", model.Username);
        return new ResponseModel(ResponseType.UserCreationFailed);
      }

      string roleName = UserRoleHelper.GetUserRoleName(role);

      if (!await _roleManager.RoleExistsAsync(roleName))
      {
        _logger.LogInformation("Role {role} created", roleName);
        await _roleManager.CreateAsync(new IdentityRole(roleName));
      }

      if (await _roleManager.RoleExistsAsync(roleName))
      {
        await _userManager.AddToRoleAsync(user, roleName);
        _logger.LogInformation("Role {role} added for user {user}", roleName, model.Username);
      }

      return new ResponseModel(ResponseType.UserSuccessfullyCreated);
    }
  }
}
