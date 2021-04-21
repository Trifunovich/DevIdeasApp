using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using Bogus.Extensions;
using DataServiceProvider.Abstractions;
using DataServiceProvider.Models;
using DataServiceProvider.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using TestingConsole.Fakers;

namespace TestingConsole
{
  class Program
  {
    //IConfiguration config = new ConfigurationBuilder()
    //  .AddJsonFile("appsettings.json", true, true)
    //  .Build();


    static async Task Main(string[] args)
    {
      var serviceProvider = new ServiceCollection()
        .AddLogging()
        .AddDataAccessServiceInternals()
        .BuildServiceProvider();
      

      ConfigureLogger(serviceProvider);

      AutoFaker.Configure(builder =>
      {
        builder // Configures the locale to use
          .WithRepeatCount(10000) // Configures the number of items in a collection
          .WithRecursiveDepth(3); // Configures how deep nested types should recurse
      });

      var carUserFaker = new CarUserFaker();
      var fakeCarUsers = carUserFaker.GenerateBetween(0, 100);

     


      var log = serviceProvider.GetService<ILogger<Program>>();
      log.LogInformation("He");
      
      var carUserService = serviceProvider.GetService<ICarUserService>();

      await carUserService.Add(carUserFaker.Generate());
      await carUserService.Insert(fakeCarUsers);
      await carUserService.DeleteById(fakeCarUsers[0].Id);
      await carUserService.UpdateRecord(fakeCarUsers[1]);

      var allUserCars = await carUserService.GetAll();

      var carService = serviceProvider.GetService<ICarService>();
      var allCars = await carService.GetAll();
      //await carUserService.Insert(fakeCarUsers);
      //await carUserService.SaveChanges();

      var mon = serviceProvider.GetService<ICarPictureService>();
      var d = new List<CarPictureInputDto>();
      Tuple<int, int> minMaxCu = new Tuple<int, int>(allUserCars.Min(x => x.Id), allUserCars.Max(x => x.Id));
      Tuple<int, int> minMaxC = new Tuple<int, int>(allCars.Min(x => x.Id), allCars.Max(x => x.Id));
      var carPickerFaker = new CarPictureFaker(minMaxC.Item1, minMaxC.Item2, minMaxCu.Item1, minMaxCu.Item2);
      carPickerFaker.GenerateBetween(20000, 25000).ForEach(x => d.Add(x));

   

      //await mon.Insert(d);

      //var monAll = await mon.GetAll();
//log.LogInformation(monAll.Count().ToString());


      var rav = serviceProvider.GetService<ICarDocumentService>();

      var carDocFaker = new CarDocumentFaker();

      var fakedDocs = carDocFaker.Generate(200);

      await rav.Insert(fakedDocs);
      await rav.SaveChanges();

      var loadedRav = await rav.GetAll();
      var loadedRav2 = await rav.GetAll();
      var loadedRav3 = await rav.GetAll();

      Console.WriteLine();
      Console.Read();
    }

    private static void ConfigureLogger(ServiceProvider provider)
    {
      Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configure())
        .CreateLogger();

      var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
      loggerFactory.AddSerilog();
    }

    private static IConfiguration Configure()
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

      return builder.Build();
    }
  }
}
