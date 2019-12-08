using BackendCore.Domain.Wiki.Command;
using BackendCore.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCore.Domain.Wiki.CommandHandler
{
    public class CreateWikiArticleCommandHandler :IRequestHandler<CreateWikiArticleCommand, string>
    {
        private readonly ApplicationDatabaseContext _context;
        public CreateWikiArticleCommandHandler(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(CreateWikiArticleCommand request, CancellationToken token)
        {
            int index = 0;
            var sections = request.Sections.Select(x => new Section { Name = x.Name, Content = x.Content, PositionPriority = index++ }).ToList();
            await _context.Exercise.AddAsync(new Exercise {Name =request.Name,Created=DateTime.Now,Section=sections }, token);
            await _context.SaveChangesAsync(token);
            return request.Name;
        }
    }
}
