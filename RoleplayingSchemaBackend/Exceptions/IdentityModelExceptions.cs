namespace RoleplayingSchemaBackend.Exceptions
{
    public sealed class IdentityModelExceptions : ApplicationException
    {
        public IdentityModelExceptions(IReadOnlyDictionary<string, string> errDict) : base("IdentityModel failure", "One or more IdentityModel errors occured.")
            => ErrorsDict = errDict;

        public IReadOnlyDictionary<string, string> ErrorsDict { get; }
    }
}
