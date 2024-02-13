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

        [HttpGet]
        public async Task<ActionResult> GetSheets()
        {
            var res = await _mediator.Send(new GetSheetsQuery());
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> AddSheet([FromBody]Sheet sheet)
        {
            var res = await _mediator.Send(new AddSheetCommand(sheet));
            return Ok(res);
        }
    }
}
