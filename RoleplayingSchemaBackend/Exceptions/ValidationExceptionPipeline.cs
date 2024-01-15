namespace RoleplayingSchemaBackend.Exceptions
{
    public sealed class ValidationExceptionPipeline : ApplicationException
    {
        public ValidationExceptionPipeline(IReadOnlyDictionary<string, string[]> errDict) : base("Validation failure", "One or more validation errors has been found.")
            => ErrorsDict = errDict;

        public IReadOnlyDictionary<string, string[]> ErrorsDict { get; }
    }
}
