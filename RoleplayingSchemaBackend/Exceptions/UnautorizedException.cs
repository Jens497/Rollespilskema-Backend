namespace RoleplayingSchemaBackend.Exceptions
{
    public sealed class UnautorizedException : ApplicationException
    {
        public UnautorizedException(IReadOnlyDictionary<string, string> errDict) : base("Unautorized", "The user was not authorized")
            => ErrorsDict = errDict;

        public IReadOnlyDictionary<string, string> ErrorsDict { get; }
    }
}
