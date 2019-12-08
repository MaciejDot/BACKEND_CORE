using BackendCore.Domain.Article.DTO;
using BackendCore.Domain.Article.Query;
using BackendCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
namespace BackendCore.Domain.Article.QueryHandler
{
    public class GetArticleThumbnailQueryHandler : IRequestHandler<GetArticleThumbnailQuery, GetArticleThumbnailDTO>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetArticleThumbnailQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<GetArticleThumbnailDTO> Handle(GetArticleThumbnailQuery request, CancellationToken token)
        {
            var thumbnail = await _context.Thumbnails
                .FindAsync(request.Id);
            return new GetArticleThumbnailDTO { Content = thumbnail.Content };
        }
    }
}
