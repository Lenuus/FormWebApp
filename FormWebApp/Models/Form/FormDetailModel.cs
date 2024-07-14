using FormWebApp.Application.Service.FormService.Dto;

namespace FormWebApp.Models.Form
{
    public class FormDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<FieldsDetailModel> Fields { get; set; }
    }
}
