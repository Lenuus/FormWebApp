using AutoMapper;
using FormWebApp.Application.Service.FormService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormWebApp.Application.Service.FormService.Mapping
{
    public class FormMapper:Profile
    {
        public FormMapper()
        {
            CreateMap<CreateFormRequestDto, Domain.Form>().
                                           ForMember(dest => dest.Fields, opt => opt.Ignore());

        }
    }
}
