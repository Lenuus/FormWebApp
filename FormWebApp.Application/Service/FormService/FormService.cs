using AutoMapper;
using FormApp.Application;
using FormWebApp.Application.Service.FormService.Dto;
using Microsoft.EntityFrameworkCore;
using FormWebApp.Application;
using FormWebApp.Common.Dtos;
using FormApp.Common.Helpers;
using Microsoft.AspNetCore.Identity;
using FormApp.Domain;
using Microsoft.AspNetCore.Http;



namespace FormWebApp.Application.Service.FormService
{
    public class FormService : IFormService
    {
        private readonly IRepository<Domain.Form> _formRepository;
        private readonly IRepository<Domain.Field> _fieldsRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FormService(IMapper mapper, IRepository<Domain.Field> fieldsRepository, IRepository<Domain.Form> formRepository, UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _fieldsRepository = fieldsRepository;
            _formRepository = formRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse> CreateForm(CreateFormRequestDto request)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (currentUser == null || currentUser.IsDeleted)
                {
                    return new ServiceResponse(false, "Unable to find user.");
                }

                var formEntity = _mapper.Map<Domain.Form>(request);
                formEntity.UserId = currentUser.Id;
                formEntity.Fields = request.Fields.Select(f => new Domain.Field
                {
                    FieldType = f.FieldType,
                    Name = f.Name,
                    Required = f.Required,
                    CreatedBy = currentUser.Id
                }).ToList();
                await _formRepository.Create(formEntity).ConfigureAwait(false);
                return new ServiceResponse(true, "");
            }

            catch (Exception ex)
            {
                return new ServiceResponse(false, "Could not creat");
            }
        }


        public async Task<ServiceResponse<PagedResponseDto<FormDetailDto>>> GetAllForms(GetAllFormRequestDto request)
        {

            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (currentUser == null || currentUser.IsDeleted)
            {
                return new ServiceResponse<PagedResponseDto<FormDetailDto>>(null, false, "Unable to find user.");
            }
            var query = await _formRepository.GetAll().Include(f => f.Fields).
                                                      Where(f => !f.IsDeleted && f.UserId == currentUser.Id && (!string.IsNullOrEmpty(request.Search) ? f.Name.Contains(request.Search) : true)).
                                                      Select(f => new FormDetailDto
                                                      {
                                                          Id = f.Id,
                                                          Name = f.Name,
                                                          CreatedAt = f.CreatedAt,
                                                          Description = f.Description,
                                                          Fields = f.Fields.Select(f => new FieldsDetailDto
                                                          {
                                                              FieldType = f.FieldType,
                                                              Name = f.Name,
                                                              Required = f.Required,
                                                          }).ToList()

                                                      }).ToPagedListAsync(request.PageSize, request.PageIndex).ConfigureAwait(false);

            return new ServiceResponse<PagedResponseDto<FormDetailDto>>(query, true, "");

        }

        public async Task<ServiceResponse<FormDetailDto>> GetForm(int id)
        {
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (currentUser == null || currentUser.IsDeleted)
            {
                return new ServiceResponse<FormDetailDto>(null, false, "Unable to find user.");
            }
            var query = await _formRepository.GetAll().Include(f => f.Fields).Where(f => !f.IsDeleted && f.UserId == currentUser.Id && f.Id == id).
                                                                   Select(f => new FormDetailDto
                                                                   {
                                                                       Id = f.Id,
                                                                       Name = f.Name,
                                                                       CreatedAt = f.CreatedAt,
                                                                       Description = f.Description,
                                                                       Fields = f.Fields.Where(f => !f.IsDeleted).Select(f => new FieldsDetailDto
                                                                       {
                                                                           FieldType = f.FieldType,
                                                                           Name = f.Name,
                                                                           Required = f.Required,
                                                                       }).ToList()
                                                                   }).FirstOrDefaultAsync(f => f.Id == id).ConfigureAwait(false);

            return new ServiceResponse<FormDetailDto>(query, true, "");
        }

       
    }
}
