using FormWebApp.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormWebApp.Application.Service.FormService.Dto
{
    public class GetAllFormRequestDto: PagedRequestDto
    {
        public string Search { get; set; }
    }
}
