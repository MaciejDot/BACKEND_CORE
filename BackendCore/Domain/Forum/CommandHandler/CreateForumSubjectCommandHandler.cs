using BackendCore.Domain.Forum.Command;
using BackendCore.Domain.Forum.DTO;
using BackendCore.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.CommandHandler
{
    public class CreateForumSubjectCommandHandler : IRequestHandler<CreateForumSubjectCommand , CreateForumSubjectResponseDTO>
    {
        private readonly ApplicationDatabaseContext _context;
        public CreateForumSubjectCommandHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<CreateForumSubjectResponseDTO> Handle(CreateForumSubjectCommand command, CancellationToken token)
        {
            var thumbnail = new Thumbnails1 { Content = command.Content };
            await _context.Thumbnails1.AddAsync(thumbnail, token);
            var subject = new Subject { Thumbnail = thumbnail, Descriprion = command.Description, Title = command.Title };
            await _context.Subject.AddAsync(subject, token);
            await _context.SaveChangesAsync(token);
            return new CreateForumSubjectResponseDTO
            {
                Id = subject.Id,
                Title = subject.Title
            };
        }
    }
}
