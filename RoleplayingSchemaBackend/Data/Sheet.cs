using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace RoleplayingSchemaBackend.Data
{
    public class Sheet
    {
        public Guid SheetId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Component> Components { get; set; }
        public string TemplateId { get; set; }
    }
}
