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
    public class SearchForumThreadQueryHandler : IRequestHandler<SearchForumThreadQuery,IEnumerable<SearchThreadsDTO>>
    {
        private readonly ApplicationDatabaseContext _context;
        public SearchForumThreadQueryHandler(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SearchThreadsDTO>> Handle(SearchForumThreadQuery request, CancellationToken token)
        {
            return await _context.Thread.Where(t => t.Title.Contains(request.Phrase) || t.Question.Contains(request.Phrase) || t.Author.UserName.Contains(request.Phrase) || t.Post.Any(p => p.Author.UserName.Contains(request.Phrase) || p.Answear.Contains(request.Phrase)))
                .Select(x => new SearchThreadsDTO { Name = x.Title, Author = x.Author.UserName, Created = x.Created, LastActivity = x.Post.Any() ? x.Post.Max(y => y.Created) : x.Created })
                .OrderByDescending(x => x.LastActivity)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(token);

        }
    }
}
