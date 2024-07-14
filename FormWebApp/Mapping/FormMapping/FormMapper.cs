using AutoMapper;
using FormWebApp.Application.Service.FormService.Dto;
using FormWebApp.Common.Dtos;
using FormWebApp.Models;
using FormWebApp.Models.Form;
using FormWebApp.Models.FormModels;

namespace FormWebApp.Mapping.FormMapping
{
    public class FormMapper : Profile
    {
        public FormMapper()
        {
            CreateMap<GetAllFormRequestModel, GetAllFormRequestDto>();
            CreateMap<CreateFormRequestModel, CreateFormRequestDto>();
            CreateMap<FieldsDetailDto, FieldsDetailModel>();
            CreateMap<FormDetailDto, FormDetailModel>();
            CreateMap<UpdateFormRequestModel, UpdateFormRequestDto>();
            CreateMap<PagedResponseDto<FormDetailDto>, PagedResponseModel<FormDetailModel>>();

        }
    }
}
