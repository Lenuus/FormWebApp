using FormApp.Domain;
using FormWebApp.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormWebApp.Domain
{
    public class Field : IBaseEntity, ISoftDeletable, ICreateAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FormId { get; set; }
        public Form Form { get; set; }
        public string Name { get; set; }
        public FieldType FieldType { get; set; }
        public bool Required { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
