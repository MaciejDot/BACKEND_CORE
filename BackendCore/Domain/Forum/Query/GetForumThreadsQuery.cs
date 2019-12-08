using BackendCore.Domain.Forum.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.Query
{
    public class GetForumThreadsQuery :IRequest<List<GetForumThreadsDTO>>
    {
        public int TakeThreads { get; set; }
        public int SkipThreads { get; set; }
        public string SubjectName { get; set; }
    }
}
