using FormWebApp.Common.Enums;

namespace FormWebApp.Models.Form
{
    public class FieldsDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldType FieldType { get; set; }
        public bool Required { get; set; }
    }
}
