using BackendCore.Domain.Article.DTO;
using BackendCore.Domain.Article.Query;
using BackendCore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCore.Domain.Article.QueryHandler
{
    public class GetArticlesQueryHandler :IRequestHandler<GetArticlesQuery, IEnumerable<GetArticlesDTO>>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetArticlesQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<IEnumerable<GetArticlesDTO>> Handle(GetArticlesQuery request, CancellationToken token)
        {
            return await _context.Article
                .OrderByDescending(article => article.Created)
                .Include(article => article.Author)
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(article => new GetArticlesDTO
                {
                    Id = article.Id,
                    Author = article.Author.UserName,
                    Created = article.Created,
                    Description = article.Description,
                    Title = article.Title,
                    ThumbnailId = article.ThumbnailId
                }).ToListAsync(token) ;
        }
    }
}
