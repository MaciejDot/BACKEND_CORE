using BackendCore.Domain.Forum.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.Query
{
    public class GetForumPostsQuery : IRequest<List<GetForumPostsDTO>>
    {
        public int ThreadId { get; set; }
        public int SkipPosts { get; set; }
        public int TakePosts { get; set; }
    }
}
