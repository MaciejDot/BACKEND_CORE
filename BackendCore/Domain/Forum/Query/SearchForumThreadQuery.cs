using BackendCore.Domain.Forum.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.Query
{
    public class SearchForumThreadQuery : IRequest<IEnumerable<SearchThreadsDTO>>
    {
        public string Phrase { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
