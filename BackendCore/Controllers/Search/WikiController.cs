
using System.Threading;
using System.Threading.Tasks;
using BackendCore.Domain.Wiki.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendCore.Controllers.Wiki
{
    [Route("Search/Wiki")]
    [EnableCors]
    [ApiController]
    public class WikiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WikiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> Get(string phrase, int page, CancellationToken token)
        {
            return new JsonResult(new { Articles = await _mediator.Send(new SearchForWikiArticleQuery
            {
                Phrase = phrase,
                Skip = (page*20)-20,
                Take = 20
            },token)});
        }
    }
}
