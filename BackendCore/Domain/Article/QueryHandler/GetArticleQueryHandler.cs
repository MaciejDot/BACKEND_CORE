using BackendCore.Domain.Article.Query;
using BackendCore.Domain.Article.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCore.Data;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace BackendCore.Domain.Article.QueryHandler
{
    public class GetArticleQueryHandler :IRequestHandler<GetArticleQuery, GetArticleDTO >
    {
        private readonly ApplicationDatabaseContext _context;
        public GetArticleQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }

        public async Task<GetArticleDTO> Handle(GetArticleQuery request, CancellationToken token)
        {
            var _article = await _context
                .Article
                .Include(article => article.Author)
                .SingleAsync(article => article.Id == request.Id, token);

            return new GetArticleDTO
            {
                Author = _article.Author.UserName,
                Created = _article.Created,
                Content = _article.Content,
                Title = _article.Title
            };
        }
    }
}
