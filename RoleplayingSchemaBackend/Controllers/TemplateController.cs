﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Commands;
using RoleplayingSchemaBackend.Requests.Queries;

namespace RoleplayingSchemaBackend.Controllers
{
    [Route("api/template")]
    public class TemplateController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public TemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /*[HttpGet("{id:int}")]
        public async Task<ActionResult> GetTemplate(int id)
        {

        }*/

        [HttpGet]
        public async Task<ActionResult> GetTemplates()
        {
            var res = await _mediator.Send(new GetTemplatesQuery());
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> AddTemplate([FromBody]Template template)
        {
            var res = await _mediator.Send(new AddTemplateCommand(template));
            return Ok(res);
        }
    }
}
