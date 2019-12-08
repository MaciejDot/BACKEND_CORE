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
    public class GetForumThumbnailPictureQueryHandler : IRequestHandler<GetForumThumbnailPictureQuery,GetForumThumbnailPictureQueryDTO>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetForumThumbnailPictureQueryHandler(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task<GetForumThumbnailPictureQueryDTO> Handle(GetForumThumbnailPictureQuery request, CancellationToken token)
        {
            return new GetForumThumbnailPictureQueryDTO
            {
                Content = (await _context.Thumbnails1.FirstOrDefaultAsync(x=>x.Id == request.Id, token))?.Content
            };
        }
    }
}
