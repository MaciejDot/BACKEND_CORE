using BackendCore.Domain.Forum.Query;
using BackendCore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.QueryHandler
{
    public class GetForumPostsCountQueryHandler : IRequestHandler<GetForumPostsCountQuery,int>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetForumPostsCountQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public Task<int> Handle(GetForumPostsCountQuery request, CancellationToken token)
        {
            return _context.Post.CountAsync(p=> p.ThreadId == request.ThreadId, token);
        }
    }
}
