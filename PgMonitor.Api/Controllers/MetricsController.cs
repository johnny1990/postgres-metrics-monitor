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

        //endpoint to get the latest metrics
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var result = await _mediator.Send(new GetLatestMetricsQuery());

            if (result == null)
                return NotFound("No metrics available yet");

            return Ok(result);
        }

        //endpoint to get historical metrics, with an optional count parameter (default 50)

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] int count = 50)
        {
            var result = await _mediator.Send(new GetMetricsHistoryQuery(count));
            return Ok(result);
        }
    }
}
