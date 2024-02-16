namespace RoleplayingSchemaBackend.Data
{
    public class Template
    {
        public Guid TemplateId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Component> Components { get; set; }
    }
}
