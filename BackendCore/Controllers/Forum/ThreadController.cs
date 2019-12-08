
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackendCore.Domain.Forum.Command;
using BackendCore.Domain.Forum.Query;
using BackendCore.Helpers;
using BackendCore.Models.Forum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BackendCore.Controllers.Forum
{
    [EnableCors]
    [Route("Thread")]
    [ApiController]
    public class ThreadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringToHtmlHelper _stringToHtmlHelper;
        public ThreadController(IMediator mediator, IStringToHtmlHelper stringToHtmlHelper)
        {
            _mediator = mediator;
            _stringToHtmlHelper = stringToHtmlHelper;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody]CreateThread command, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("not found");
            }

            return new JsonResult(new
            {
                Thread = await _mediator.Send(new CreateThreadCommand
                {
                    Content = _stringToHtmlHelper.GetHtml(command.Content),
                    SubjectName = command.SubjectName,
                    Title = command.Title,
                    UserId = User.Claims.Single(x => x.Type == "Id").Value
                }, token)
            });
        }

        [HttpGet("{subjectName}/{page}")]
        [AllowAnonymous]
        public async Task<ActionResult> Get(string subjectName, int page,CancellationToken token)
        {
            if (page < 1)
            {
                return BadRequest($"parameter page can't be less than 1");
            }
            try
            {
                var threads = _mediator.Send(new GetForumThreadsQuery { SkipThreads = page * 20 - 20, SubjectName = subjectName, TakeThreads = 20 },token);
                var threadsCount = _mediator.Send(new GetThreadsCountQuery { SubjectName = subjectName },token);
                var subjectNameDatabase = _mediator.Send(new SubjectNameExistsInDatabaseQuery { SubjectName = subjectName },token);
                await Task.WhenAll(threads, threadsCount, subjectNameDatabase);
                if ((threadsCount.Result > (page - 1) * 20 || (threadsCount.Result == 0 && page == 1))&&subjectName== subjectNameDatabase.Result && subjectName != null)
                {
                    return new JsonResult(new
                    {
                        Title = subjectNameDatabase.Result,
                        Threads = threads.Result,
                        allThreadsCount = threadsCount.Result
                    });
                }
            }
            catch
            {
                return NotFound();
            }
            return NotFound();

        }
    }
}