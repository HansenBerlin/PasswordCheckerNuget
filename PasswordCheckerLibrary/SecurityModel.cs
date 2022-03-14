namespace PasswordCheckerLibrary;

public sealed class SecurityModel
{
    public int MinLength { get; init; }
    public bool IsMixLowerAndUpperRuleApplied { get; init; }
    public bool IsMustContainDigitsRuleApplied { get; init; }
    public int MinimumSpecialCharactersNeeded { get; init; }
    public bool IsRestrictReservedCharactersRuleApplied { get; init; }
}