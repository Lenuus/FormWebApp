using FormWebApp.Application.Service.FormService.Dto;

namespace FormWebApp.Models.Form
{
    public class CreateFormRequestModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<FieldsDetailDto> Fields { get; set; }
    }
}
