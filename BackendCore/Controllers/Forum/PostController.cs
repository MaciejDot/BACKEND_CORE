using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackendCore.Domain.Forum.Command;
using BackendCore.Domain.Forum.DTO;
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
    [Route("Post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringToHtmlHelper _stringToHtmlHelper;
        public PostController(IMediator mediator, IStringToHtmlHelper stringToHtmlHelper)
        {
            _mediator = mediator;
            _stringToHtmlHelper = stringToHtmlHelper;
        }

        [HttpGet("{subjectName}/{threadId}/{page}")]
        [AllowAnonymous]
        public async Task<ActionResult<PostPageDTO>> GetPosts(string subjectName, int threadId, int page, CancellationToken token)
        {
            if (page < 1 || subjectName == null)
            {
                return BadRequest($"parameter page can't be less than 1");
            }
            try
            {
                var posts = await _mediator.Send(new GetForumPostsQuery { SkipPosts = page * 20 - 20, TakePosts = 20, ThreadId = threadId },token);
                if ((posts.AllPostsCount > (page - 1) * 20 || (posts.AllPostsCount == 0 && page == 1)) && subjectName == posts.Thread.SubjectName)
                {
                    return posts;
                }
            }
            catch
            {
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody]CreatePost command,CancellationToken token)
        {
            return new JsonResult(new
            {
                Post = await _mediator.Send(new CreatePostCommand
                {
                    Content = _stringToHtmlHelper.GetHtml(command.Content),
                    ThreadId = command.ThreadId,
                    UserId = User.Claims.Single(x => x.Type == "Id").Value
                }, token)
            }) ;
        }
    }
}
