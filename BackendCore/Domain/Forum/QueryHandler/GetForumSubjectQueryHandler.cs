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
    public class GetForumSubjectQueryHandler : IRequestHandler<GetForumSubjectQuery, List<GetForumSubjectDTO>>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetForumSubjectQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public Task<List<GetForumSubjectDTO>> Handle(GetForumSubjectQuery request, CancellationToken token)
        {
            return _context.Subject
                .Select(subject => new GetForumSubjectDTO
                {
                    Id = subject.Id,
                    ThumbnailId = subject.ThumbnailId,
                    Description = subject.Descriprion,
                    LastActivity =DateTime.MinValue,/* subject.Thread.Any() ? 
                        subject.Thread.Max(thread => thread.Post.Any() ? thread.Post.Max(post => post.Created) : thread.Created) 
                        : DateTime.MinValue*/
                    PostCount = 1,//subject.Thread.Sum(x => x.Post.Count() + 1),
                    Title = subject.Title
                }).ToListAsync(token);
        }
    }
}
