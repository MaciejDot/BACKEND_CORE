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
    public class GetForumSubjectQueryHandler : IRequestHandler<GetForumSubjectQuery, SubjectsDTO>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetForumSubjectQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<SubjectsDTO> Handle(GetForumSubjectQuery request, CancellationToken token)
        {
            var subjects = _context.Subject
                .Select(subject => new GetForumSubjectDTO
                {
                    Id = subject.Id,
                    ThumbnailId = subject.ThumbnailId,
                    Description = subject.Descriprion,
                    Title = subject.Title
                }).ToList();
            subjects.ForEach(subject => {
                subject.PostCount = _context.Thread.Count(x => x.SubjectId == subject.Id) + _context.Post.Count(x => x.Thread.SubjectId == subject.Id);
                subject.LastActivity = new DateTime(Math.Max(
                    _context.Thread.Any(x => x.SubjectId == subject.Id)?
                    _context.Thread.Where(x => x.SubjectId == subject.Id).Max(x => x.Created).Ticks:
                    DateTime.MinValue.Ticks,
                    _context.Post.Any(x => x.Thread.SubjectId == subject.Id)?
                    _context.Post.Where(x => x.Thread.SubjectId == subject.Id).Max(x => x.Created).Ticks:
                    DateTime.MinValue.Ticks));
             });
            return new SubjectsDTO
            {
                Subjects = subjects
            };
        }
    }
}
