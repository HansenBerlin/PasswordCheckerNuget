using System.Text.RegularExpressions;

namespace PasswordCheckerLibrary;

public class PasswordSecurityChecker
{
    private readonly SecurityModel _securityModel;
    
    /// <summary>
    /// Pass any parameters as needed. Defaults if not passed is 0 for integer values and false for bool values.
    /// If multiple rules are being checked all must be passed to consider a password secure.
    /// </summary>
    /// <param name="minPasswordLength">The minimum required length to consider a password secure</param>
    /// <param name="specialCharactersNeeded">The minimum count of special characters to consider a password secure</param>
    /// <param name="reservedCharactersForbidden">Avoid reserved characters. These are: $, /, \, #, "</param>
    /// <param name="mustContainUpperAndLowerCase">Both upper and lower case characters must be contained</param>
    /// <param name="mustContainDigits">The password needs to contain at least one digit</param>
    public PasswordSecurityChecker(int minPasswordLength = 0, int specialCharactersNeeded = 0,
        bool reservedCharactersForbidden = false, bool mustContainUpperAndLowerCase = false, bool mustContainDigits = false)
    {
        _securityModel = new SecurityModel
        {
            MinLength = minPasswordLength,
            MinimumSpecialCharactersNeeded = specialCharactersNeeded,
            IsRestrictReservedCharactersRuleApplied = reservedCharactersForbidden,
            IsMixLowerAndUpperRuleApplied = mustContainUpperAndLowerCase,
            IsMustContainDigitsRuleApplied = mustContainDigits
        };
    }

    public bool IsPasswordSecure(string password)
    {
        return _securityModel.MinLength <= password.Length && 
               !IsSpecialCharacterRuleBroken(password) && 
               !IsReservedCharacterRuleBroken(password) && 
               !IsMixedCharacterCaseRuleBroken(password) && 
               !IsContainsNumbersRuleBroken(password);
    }

    private bool IsSpecialCharacterRuleBroken(string pw)
    {
        Regex rgx = new("[^A-Za-z0-9]");
        int count = rgx.Matches(pw).Count;
        return _securityModel.MinimumSpecialCharactersNeeded > count;
    }
    
    private bool IsReservedCharacterRuleBroken(string pw)
    {
        if (_securityModel.IsRestrictReservedCharactersRuleApplied == false)
            return false;
        var reserved = new ReservedCharacters();
        return pw.Any(c => reserved.Reserved.Contains(c));
    }
    
    private bool IsMixedCharacterCaseRuleBroken(string pw)
    {
        if (_securityModel.IsMixLowerAndUpperRuleApplied == false)
            return false;
        return !(pw.Any(char.IsUpper) && pw.Any(char.IsLower));
    }
    
    private bool IsContainsNumbersRuleBroken(string pw)
    {
        if (_securityModel.IsMustContainDigitsRuleApplied == false)
            return false;
        return !pw.Any(char.IsDigit);
    }
}