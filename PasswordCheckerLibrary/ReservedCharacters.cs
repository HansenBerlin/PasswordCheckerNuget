namespace PasswordCheckerLibrary;

public sealed record ReservedCharacters()
{
    public readonly char[] Reserved = {'$', '/', '\\', '#', '\"'};
};