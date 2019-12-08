using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackendCore.Domain.Wiki.Command;
using BackendCore.Domain.Wiki.Models;
using BackendCore.Domain.Wiki.Query;
using BackendCore.Helpers;
using BackendCore.Models.Wiki;
using BackendCore.Security.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BackendCore.Controllers.Search
{
    [EnableCors]
    [Route("WikiExercise")]
    [ApiController]
    public class WikiExerciseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringToHtmlHelper _stringToHtmlHelper;
        public WikiExerciseController(IMediator mediator, IStringToHtmlHelper stringToHtmlHelper)
        {
            _mediator = mediator;
            _stringToHtmlHelper = stringToHtmlHelper;
        }
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<JsonResult> Post(ExercisePost exercise,CancellationToken token)
        {
            return new JsonResult(await _mediator.Send(new CreateWikiArticleCommand {Name=exercise.Name, Sections=exercise.Sections.Select(x=>new Section {
                Name = x.Name,
                Content = _stringToHtmlHelper.GetHtml(x.Content)
            }) },token));
        }

        [AllowAnonymous]
        [HttpGet("{name}")]
        public async Task<ActionResult> Get(string name, CancellationToken token)
        {
            var result = await _mediator.Send(new GetWikiArticleQuery { Name = name }, token);
            if(result == null)
            {
                return NotFound("not found");
            }
            return new JsonResult(new { Article = result });
        }
    }
}