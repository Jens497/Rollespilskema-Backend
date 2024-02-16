using MediatR;
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

        [HttpGet]
        public async Task<ActionResult> GetTemplatesIdName()
        {
            var res = await _mediator.Send(new GetTemplatesIdNameQuery());
            return Ok(res);
        }


        [HttpGet("templates")]
        public async Task<ActionResult> GetTemplates([FromQuery]List<string> Ids)
        {
            var res = await _mediator.Send(new GetTemplatesQuery(Ids));
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> AddTemplate([FromBody]Template template)
        {
            var res = await _mediator.Send(new AddTemplateCommand(template));
            return Ok(res);
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateTemplate([FromBody]Template template)
        {
            var res = await _mediator.Send(new UpdateTemplateCommand(template));
            return Ok(res);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTemplate([FromQuery]string templateId)
        {
            var res = await _mediator.Send(new DeleteTemplateCommand(templateId));
            return Ok(res);
        }

        [HttpDelete("{templateId}/{componentId}")]
        public async Task<ActionResult> DeleteTemplateComponent([FromRoute]string templateId, [FromRoute]string componentId)
        {
            var res = await _mediator.Send(new DeleteTemplateComponentCommand(templateId, componentId));
            return Ok(res);
        }
    }
}
