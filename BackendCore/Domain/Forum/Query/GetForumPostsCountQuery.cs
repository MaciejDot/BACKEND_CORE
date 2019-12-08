using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.Query
{
    public class GetForumPostsCountQuery :IRequest<int>
    {
        public int ThreadId { get; set; }
    }
}
