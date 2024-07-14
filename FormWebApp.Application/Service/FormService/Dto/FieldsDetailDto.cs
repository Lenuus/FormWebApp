using FormWebApp.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormWebApp.Application.Service.FormService.Dto
{
    public class FieldsDetailDto
    {
        public string Name { get; set; }
        public FieldType FieldType { get; set; }
        public bool Required { get; set; }
    }
}
