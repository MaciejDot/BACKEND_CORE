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
    public class GetArticleCountQueryHandler :IRequestHandler<GetArticleCountQuery,int>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetArticleCountQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<int> Handle(GetArticleCountQuery request, CancellationToken token)
        {
            return await _context.Article.CountAsync(token);
        }
    }
}
