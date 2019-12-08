using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackendCore.Domain.Forum.Command;
using BackendCore.Domain.Forum.Query;
using BackendCore.Models.Forum;
using BackendCore.Security.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BackendCore.Controllers.Forum
{
    [EnableCors]
    [Route("Subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> Get(CancellationToken token)
        {
            return new JsonResult(new { Subjects = await _mediator.Send(new GetForumSubjectQuery(),token) });
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Post(CreateSubject command, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid model" });
            }
            var fileStreamResult = Request.Form.Files[0].OpenReadStream();
            var fileBytes = new byte[fileStreamResult.Length];
            fileStreamResult.Read(fileBytes, 0, (int)fileStreamResult.Length);
            if (!fileBytes.Any())
            {

                return BadRequest(new { message = "Photo was not provided" });
            }
            return new JsonResult(new
            {
                Forum = await _mediator.Send(new CreateForumSubjectCommand
                {
                    Content = fileBytes,
                    Description = command.Description,
                    Title = command.Title
                },token)
            });
        }
    }
}