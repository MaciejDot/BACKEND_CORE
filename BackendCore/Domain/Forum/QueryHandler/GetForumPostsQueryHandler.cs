using BackendCore.Domain.Forum.DTO;
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
    public class GetForumPostsQueryHandler :IRequestHandler<GetForumPostsQuery, List<GetForumPostsDTO>>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetForumPostsQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public Task<List<GetForumPostsDTO>> Handle(GetForumPostsQuery request, CancellationToken token)
        {
            return _context
                .Post
                .Where(p => p.ThreadId == request.ThreadId)
                .OrderBy(post => post.Created)
                .Skip(request.SkipPosts)
                .Take(request.TakePosts)
                .Select(post => new GetForumPostsDTO
                {
                    Author=post.Author.UserName,
                    Content=post.Answear,
                    Created= post.Created,
                    Id = post.Id
                }
                )
                .ToListAsync(token)
                ;
        }
    }
}
