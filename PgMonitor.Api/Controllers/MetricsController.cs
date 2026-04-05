using MediatR;
using Microsoft.AspNetCore.Mvc;
using PgMonitor.Application.Queries;

namespace PgMonitor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var result = await _mediator.Send(new GetLatestMetricsQuery());

            if (result == null)
                return NotFound("No metrics available yet");

            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] int count = 50)
        {
            var result = await _mediator.Send(new GetMetricsHistoryQuery(count));
            return Ok(result);
        }
    }
}
