﻿using ExamenLenguajes.Constants;
using ExamenLenguajes.Dtos.Common;
using ExamenLenguajes.Dtos.Requests;
using ExamenLenguajes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamenLenguajes.Controllers
{
    [ApiController]
    [Route("api/requests")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class RequestsController : ControllerBase
    {
        private readonly IRequestsService _requestsService;

        public RequestsController(IRequestsService requestsService)
        {
            this._requestsService = requestsService;
        }

        [HttpGet]
		[Authorize(Roles = $"{RolesConstant.HUMAN_RESOURCES}")]
		public async Task<ActionResult<ResponseDto<List<RequestDto>>>> GetAll(string searchTerm = "", int page = 1)
        {
            var response = await _requestsService.GetAllRequestsAsync(searchTerm, page);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<RequestDto>>> Get(Guid id)
        {
            var response = await _requestsService.GetRequestByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
		[Authorize(Roles = $"{RolesConstant.EMPLOYEE}")]
		public async Task<ActionResult<ResponseDto<RequestDto>>> Create(RequestCreateDto dto)
        {
            var response = await _requestsService.CreateRequestAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
		[Authorize(Roles = $"{RolesConstant.HUMAN_RESOURCES}")]
		public async Task<ActionResult<ResponseDto<RequestDto>>> Edit(RequestEditDto dto, Guid id)
        {
            var response = await _requestsService.EditRequestAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
		[Authorize(Roles = $"{RolesConstant.EMPLOYEE}, {RolesConstant.HUMAN_RESOURCES}")]
		public async Task<ActionResult<ResponseDto<RequestDto>>> Delete(Guid id)
        {
            var response = await _requestsService.DeleteRequestAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
