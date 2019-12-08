using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BackendCore.Domain.Article.Query;
using BackendCore.Helpers;
using BackendCore.Models.Article;
using BackendCore.Domain.Article.Command;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using BackendCore.Security.Roles;
using System.Linq;

namespace BackendCore.Controllers.Article
{

    [EnableCors]
    [Route("Article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringToHtmlHelper _stringToHtmlHelper;

        public ArticleController(IMediator mediator, IStringToHtmlHelper stringToHtmlHelper)
        {
            _mediator = mediator;
            _stringToHtmlHelper = stringToHtmlHelper;
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<JsonResult> Post([FromForm] CreateArticle request, CancellationToken token)
        {
            var fileStreamResult = Request.Form.Files[0].OpenReadStream();
            var fileBytes = new byte[fileStreamResult.Length];
            fileStreamResult.Read(fileBytes, 0, (int)fileStreamResult.Length);
            var requestCommand = new CreateArticleCommand
            {
                AuthorId = User.Claims.Single(x => x.Type == "Id").Value,
                Created = DateTime.UtcNow.ToUniversalTime(),
                Content = _stringToHtmlHelper.GetHtml(request.Content),
                Description = request.Description,
                Title = request.Title,
                Thumbnail = fileBytes
            };
            return new JsonResult(new { Article = await _mediator.Send(requestCommand, token) });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> Get(CancellationToken token)
        {
            return await Get(1, token);
        }

        [HttpGet("{page}")]
        [AllowAnonymous]
        public async Task<JsonResult> Get(int page, CancellationToken token)
        {
            var taskGetArticles = _mediator.Send(new GetArticlesQuery { Skip = 20 * page - 20, Take = 20 }, token);
            var taskArticlesCount = _mediator.Send(new GetArticleCountQuery(), token);
            await Task.WhenAll(taskArticlesCount, taskGetArticles);
            return new JsonResult( new { articles = taskGetArticles.Result, allArticlesCount = taskArticlesCount.Result });
        }

        [HttpGet("{page}/{id}")]
        [AllowAnonymous]
        public async Task<JsonResult> Get(int page, int id, CancellationToken token)
        {
            return new JsonResult(
                await _mediator.Send(new GetArticleQuery { Id = id }, token)
            );
        }
    }
}