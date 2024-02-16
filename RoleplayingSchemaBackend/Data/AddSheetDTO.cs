namespace RoleplayingSchemaBackend.Data
{
    public class AddSheetDTO
    {
        public Guid SheetId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Component> Components { get; set; }
        public string TemplateId { get; set; }
        public string UserId { get; set; }
    }
}
