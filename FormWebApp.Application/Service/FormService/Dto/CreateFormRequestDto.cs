using FormWebApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormWebApp.Application.Service.FormService.Dto
{
    public class CreateFormRequestDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public string InsertedUser { get; set; }

        public List<FieldsDetailDto> Fields { get; set; }
    }
}
