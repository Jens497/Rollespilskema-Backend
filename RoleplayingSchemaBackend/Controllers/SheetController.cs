using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Commands;
using RoleplayingSchemaBackend.Requests.Queries;

namespace RoleplayingSchemaBackend.Controllers
{
    [Route("api/sheet")]
    public class SheetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SheetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("sheets")]
        public async Task<ActionResult> GetSheets([FromQuery]List<string> Ids)
        {
            var res = await _mediator.Send(new GetSheetsQuery(Ids));
            return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult> GetSheetsIdName()
        {
            var res = await _mediator.Send(new GetSheetsIdNameQuery());
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> AddSheet([FromBody]AddSheetDTO sheetDto)
        {
            var res = await _mediator.Send(new AddSheetCommand(sheetDto));
            return Ok(res);
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateSheet([FromBody]Sheet sheet)
        {
            var res = await _mediator.Send(new UpdateSheetCommand(sheet));
            return Ok(res);
        }
    }
}
