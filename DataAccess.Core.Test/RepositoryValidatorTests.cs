using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Core.Validation;
using SharedCodeLibrary;
using Xunit;

namespace DataAccess.Core.Test
{
  public class RepositoryValidatorTests
  {
    private IRepositoryInputValidator _validator => new RepositoryInputValidator(null);

    [Theory]
    [InlineData("2001-05-02","2005-01-03",true)]
    [InlineData("2001-05-02", null, true)]
    [InlineData(null, null, false)]
    [InlineData("1987-03-02", null, false)]
    [InlineData("2005-01-03", "2001-05-02", false)]
    [InlineData("2005-01-03", null, true)]
    public void ValidateGetAllParams(string createdAfterString, string createdBeforeString, bool expected)
    {
      var dateAfter = createdAfterString is null ? DateTime.MinValue : DateTime.Parse(createdAfterString);
      DateTime? dateBefore = string.IsNullOrEmpty(createdBeforeString) ? (DateTime?) null : DateTime.Parse(createdBeforeString);
      Assert.Equal(_validator.ValidateGetAllParams(dateAfter, dateBefore), expected);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(571, true)]
    [InlineData(int.MaxValue, true)]
    [InlineData(0, false)]
    [InlineData(int.MinValue, false)]
    [InlineData(-142, false)]
    public void ValidateIntId(int id, bool expected)
    {
      Assert.Equal(_validator.ValidateIntId(id), expected);
    }

    [Theory]
    [MemberData(nameof(GetValuesToValidateValues))]
    public void ValidateValue<T>(T value, bool expected)
    {
      Assert.Equal(_validator.ValidateValue(value), expected);
    }

    [Theory]
    [MemberData(nameof(GetValuesToValidateLabels))]
    public void ValidateLabel(string label, bool expected)
    {
      Assert.Equal(_validator.ValidateLabel(label), expected);
    }

    [Theory]
    [MemberData(nameof(GetInputValues))]
    public void ValidateInputList<T>(IEnumerable<T> records, bool expected)
    {
      Assert.Equal(_validator.ValidateInputList(records), expected);
    }

    [Theory]
    [InlineData(1, 1, 0, true)]
    [InlineData(1, 2, -1, false)]
    [InlineData(10, 100, 0, true)]
    [InlineData(10, 100, 1000, false)]
    [InlineData(10, 100, 101, false)]
    [InlineData(1, 1, -1, false)]
    [InlineData(0, 0, 0, false)]
    [InlineData(0, 1, 0, false)]
    [InlineData(-1, 5, 0, false)]
    [InlineData(2, -3, 0, false)]
    [InlineData(int.MaxValue, int.MaxValue, int.MaxValue, false)]
    public void ValidatePagingParams(int page, int pageSize, int offset, bool expected)
    {
      var pageParams = new PagingParameters(page, pageSize, offset);
      Assert.Equal(_validator.ValidatePagingParams(pageParams), expected);
    }

    public static IEnumerable<object[]> GetInputValues()
    {
      yield return new object[] { null, false };
      yield return new object[] { new List<object> { new object(), new object() }, true };
      yield return new object[] { new List<object>(), false};
    }

    public static IEnumerable<object[]> GetValuesToValidateValues()
    {
      yield return new object[] { null, false };
      yield return new object[] { new object(), true };
      yield return new object[] { 1, true };
    }

    public static IEnumerable<object[]> GetValuesToValidateLabels()
    {
      yield return new object[] { null, true };
      yield return new object[] { "Some label", true };
      yield return new object[] { "Huuuuuuuuuuuuuuuuuuuuuuuugeeeeeeeeeeeeeeeeeeeee label", true };
      yield return new object[] { "", true };
      yield return new object[] { string.Empty, true };
      yield return new object[] { RandomString(50), true };
      yield return new object[] { RandomString(150), false };
      yield return new object[] { RandomString(250), false };
      yield return new object[] { RandomString(1250), false };
    }

    private static readonly Random Random = new Random();
    public static string RandomString(int length)
    {
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

  }
}