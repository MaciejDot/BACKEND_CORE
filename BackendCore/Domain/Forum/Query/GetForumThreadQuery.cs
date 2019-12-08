using BackendCore.Domain.Forum.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.Query
{
    public class GetForumThreadQuery :IRequest<GetForumThreadDTO>
    {
        public int ThreadId { get; set; }
    }
}
