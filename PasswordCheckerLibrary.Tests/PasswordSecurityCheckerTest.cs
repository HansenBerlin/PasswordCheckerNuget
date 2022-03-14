using Xunit;

namespace PasswordCheckerLibrary.Tests;

public class PasswordSecurityCheckerTest
{
    // min length for testcases: 12
    private const string PasswordFullfillingAllRules = "12dfHH!(()ddd&g222333";
    private const string PasswordFullfillingAllRulesButDigits = "ttdfHH!(()ddd&gzzHHgeR";
    private const string PasswordFullfillingAllRulesButContaindReservedChars = "12dfH//H!(()ddd&g222333";
    private const string PasswordFullfillingSpecialChars = "!!%%dfHHGGGhhJJffDDDssEE";
    private const string PasswordFullfillingLengthMixedCaseAndDigitRule = "12dfHH3GGGhhJJ3";
    private const string PasswordFullfillingLengthAndMixedCase = "dfHHGGGhhJJffDDDssEE";
    private const string PasswordFullfillingLength = "abcdefghijkl";
    private const string PasswordNotFullfillingLength = "dddd";
        
    [Theory]
    [InlineData(PasswordFullfillingLength)]
    [InlineData(PasswordNotFullfillingLength)]
    [InlineData(PasswordFullfillingAllRulesButContaindReservedChars)]
    public void IsPasswordSecure_ReturnTrue_WhenNoCaseIsChecked(string pw)
    {
        var sut = new PasswordSecurityChecker();

        bool result = sut.IsPasswordSecure(pw);
        
        Assert.True(result);
    }
    
    [Fact]
    public void IsPasswordSecure_ReturnTrue_WhenLengthCaseIsFullfilled()
    {
        var sut = new PasswordSecurityChecker(minPasswordLength:12);

        bool result = sut.IsPasswordSecure(PasswordFullfillingLength);
        
        Assert.True(result);
    }
    
    [Fact]
    public void IsPasswordSecure_ReturnFalse_WhenLengthCaseIsNotFullfilled()
    {
        var sut = new PasswordSecurityChecker(minPasswordLength:12);

        bool result = sut.IsPasswordSecure(PasswordNotFullfillingLength);
        
        Assert.False(result);
    }
    
    [Fact]
    public void IsPasswordSecure_ReturnTrue_WhenSpecialCharsNeededIsFullfilled()
    {
        var sut = new PasswordSecurityChecker(specialCharactersNeeded:4);

        bool result = sut.IsPasswordSecure(PasswordFullfillingSpecialChars);
        
        Assert.True(result);
    }

    [Fact]
    public void IsPasswordSecure_ReturnFalse_WhenSpecialCharsNeededIsNotFullfilled()
    {
        var sut = new PasswordSecurityChecker(specialCharactersNeeded:5);

        bool result = sut.IsPasswordSecure(PasswordFullfillingSpecialChars);
        
        Assert.False(result);
    }
    
    [Fact]
    public void IsPasswordSecure_ReturnTrue_WhenAllCasesAreFullfilled()
    {
        var sut = new PasswordSecurityChecker(12, 5, true, true, true);

        bool result = sut.IsPasswordSecure(PasswordFullfillingAllRules);
        
        Assert.True(result);
    }
    
    [Fact]
    public void IsPasswordSecure_ReturnFalse_WhenAllCasesAreFullfilledExceptDigits()
    {
        var sut = new PasswordSecurityChecker(12, 5, true, true, true);

        bool result = sut.IsPasswordSecure(PasswordFullfillingAllRulesButDigits);
        
        Assert.False(result);
    }
}