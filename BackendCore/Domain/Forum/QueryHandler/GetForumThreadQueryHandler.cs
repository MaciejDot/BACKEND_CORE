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
    public class GetForumThreadQueryHandler :IRequestHandler<GetForumThreadQuery, GetForumThreadDTO>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetForumThreadQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public Task<GetForumThreadDTO> Handle(GetForumThreadQuery request, CancellationToken token)
        {
            return _context.Thread.Select(thread => new GetForumThreadDTO
            {
                Author = thread.Author.UserName,
                Id = thread.Id,
                Content = thread.Question,
                Title = thread.Title,
                Created = thread.Created,
                SubjectName = thread.Subject.Title
            }).FirstAsync(t=> t.Id == request.ThreadId, token);
        }
    }
}
