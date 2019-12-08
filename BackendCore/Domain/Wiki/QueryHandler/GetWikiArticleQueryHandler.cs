using BackendCore.Domain.Wiki.DTO;
using BackendCore.Domain.Wiki.Query;
using BackendCore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCore.Domain.Wiki.QueryHandler
{
    public class GetWikiArticleQueryHandler :IRequestHandler<GetWikiArticleQuery, WikiArticleDTO>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetWikiArticleQueryHandler(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task<WikiArticleDTO> Handle(GetWikiArticleQuery request, CancellationToken token)
        {
            return await _context.Exercise
                .Where(exercise => exercise.Name == request.Name)
                .Select(
                    exercise =>
                    new WikiArticleDTO
                    {
                        Name = exercise.Name,
                        Sections = exercise.Section.OrderBy(section => section.PositionPriority).Select(section => new WikiSection {
                            Title = section.Name,
                            Content = section.Content
                        })
                    }
                ).FirstOrDefaultAsync(token);
        }
    }
}
