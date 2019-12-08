
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
    public class SearchForWikiArticleQueryHandler : IRequestHandler<SearchForWikiArticleQuery, IEnumerable<SearchWikiArticleDTO>>
    {
        private readonly ApplicationDatabaseContext _context;
        public SearchForWikiArticleQueryHandler(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SearchWikiArticleDTO>> Handle(SearchForWikiArticleQuery request, CancellationToken token)
        {
            return await _context.Exercise
                .Where(ex => ex.Name.Contains(request.Phrase) || 
                    ex.Section.Any(sec => sec.Name.Contains(request.Phrase) || 
                        sec.Content.Contains(request.Phrase)))

                .OrderByDescending(
                    x => x.Created
                )
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(x => new SearchWikiArticleDTO {
                    Name=x.Name,
                    Created=x.Created
                })
                .ToListAsync(token);
        }
    }
}
