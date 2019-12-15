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
    public class GetForumPostsQueryHandler : IRequestHandler<GetForumPostsQuery, PostPageDTO>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetForumPostsQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<PostPageDTO> Handle(GetForumPostsQuery request, CancellationToken token)
        {
            var posts = await _context
                .Post
                .Where(p => p.ThreadId == request.ThreadId)
                .OrderBy(post => post.Created)
                .Skip(request.SkipPosts)
                .Take(request.TakePosts)
                .Select(post => new GetForumPostsDTO
                {
                    Author = post.Author.UserName,
                    Content = post.Answear,
                    Created = post.Created,
                    Id = post.Id
                }
                )
                .ToListAsync(token)
                ;
            var allPostsCount = await _context.Post.CountAsync(x => x.Thread.Id == request.ThreadId);
            var thread = await _context.Thread
                .Select(x => 
                    new GetForumThreadDTO { Id = x.Id, Author = x.Author.UserName, Content = x.Question, Title = x.Title, Created = x.Created, SubjectName = x.Subject.Title })
                .SingleAsync(x => x.Id == request.ThreadId);
            return new PostPageDTO
            {
                Posts = posts,
                AllPostsCount = allPostsCount,
                Thread = thread
            };
        }
    }
}
