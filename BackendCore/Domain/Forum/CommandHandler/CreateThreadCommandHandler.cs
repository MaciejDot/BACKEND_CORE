using BackendCore.Domain.Forum.Command;
using BackendCore.Domain.Forum.DTO;
using BackendCore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BackendCore.Domain.Forum.CommandHandler
{
    public class CreateThreadCommandHandler : IRequestHandler<CreateThreadCommand, CreateThreadResponseDTO>
    {
        private readonly ApplicationDatabaseContext _context;
        public CreateThreadCommandHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<CreateThreadResponseDTO> Handle(CreateThreadCommand command, CancellationToken token)
        {
            var thread = new Data.Thread
            {
                Author = _context.AspNetUsers.Find(command.UserId),
                Title = command.Title,
                Created = DateTime.Now.ToUniversalTime(),
                Question = command.Content,
                Subject = _context.Subject
                .First(s => s.Title == command.SubjectName)

            };
            await _context.Thread.AddAsync(thread, token);
            await _context.SaveChangesAsync(token);
            return new CreateThreadResponseDTO { ThreadId = thread.Id, SubjectName = command.SubjectName };
        }
    }
}
