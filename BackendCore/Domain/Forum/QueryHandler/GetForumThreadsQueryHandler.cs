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
    public class GetForumThreadsQueryHandler :IRequestHandler<GetForumThreadsQuery,List<GetForumThreadsDTO>>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetForumThreadsQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public Task<List<GetForumThreadsDTO>> Handle(GetForumThreadsQuery request, CancellationToken token)
        {
            return Task.FromResult(_context.Thread.Where(t => t.Subject.Title == request.SubjectName)
                .OrderByDescending(thread => thread.Created)
                .Skip(request.SkipThreads)
                .Take(request.TakeThreads)
                .Select(thread=>
                    new GetForumThreadsDTO
                    {
                        Id = thread.Id,
                        Author = thread.Author.UserName,
                        Created = thread.Created,
                        Title = thread.Title,
                        Replies = thread.Post.Count,
                        LastActivity = thread.Post.Any() ? thread.Post.Max(post=>post.Created) : thread.Created
                    }
                )
                .ToList());
        }
    }
}
