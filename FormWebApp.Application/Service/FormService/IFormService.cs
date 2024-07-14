using FormApp.Application;
using FormWebApp.Application.Service.FormService.Dto;
using FormWebApp.Application;
using FormWebApp.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FormWebApp.Application.Service.FormService
{
    public interface IFormService : IApplicationService
    {
       
        Task<ServiceResponse<FormDetailDto>> GetForm(int id);

        Task<ServiceResponse> CreateForm(CreateFormRequestDto request);

        Task<ServiceResponse<PagedResponseDto<FormDetailDto>>> GetAllForms(GetAllFormRequestDto request);

        
    }
}
