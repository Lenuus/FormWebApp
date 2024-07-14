using AutoMapper;
using Azure.Core;
using FormApp.Domain;
using FormWebApp.Application.Service.FormService;
using FormWebApp.Application.Service.FormService.Dto;
using FormWebApp.Models;
using FormWebApp.Models.Form;
using FormWebApp.Models.FormModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormWebApp.Controllers
{
    public class FormController : Controller
    {
        private readonly IFormService _formService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public FormController(IMapper mapper, IFormService formService, UserManager<User> userManager)
        {
            _mapper = mapper;
            _formService = formService;
            _userManager = userManager;
        }

        [Route("forms/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _formService.GetForm(id).ConfigureAwait(false);
            if (!response.IsSuccesfull)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<FormDetailModel>(response.Data);
            return View(mappedResponse);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PagedList(GetAllFormRequestModel request)
        {
            var mappedRequest = _mapper.Map<GetAllFormRequestDto>(request);
            var response = await _formService.GetAllForms(mappedRequest).ConfigureAwait(false);
            if (!response.IsSuccesfull)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<PagedResponseModel<FormDetailModel>>(response.Data);
            return Ok(mappedResponse);
        }

     

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateFormRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mappedRequest = _mapper.Map<CreateFormRequestDto>(request);
            await _formService.CreateForm(mappedRequest).ConfigureAwait(false);
            return RedirectToAction("Index");
        }

      

        public IActionResult SubmitForm(IFormCollection form)
        {
            return View();
        }
    }
}
