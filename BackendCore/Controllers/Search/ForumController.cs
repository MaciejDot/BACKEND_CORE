
using System.Threading;
using System.Threading.Tasks;
using BackendCore.Domain.Forum.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BackendCore.Controllers.Search
{
    [Route("Search/Forum")]
    [ApiController]
    [EnableCors]
    public class ForumController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ForumController(IMediator mediator) {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> Get(string phrase, int page, CancellationToken token)
        {
            return new JsonResult(new { Threads = await _mediator.Send(new SearchForumThreadQuery { Phrase = phrase, Skip = (page * 20) - 20, Take = 20 },token) });
        }
    }
}