using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.Query
{
    public class GetThreadsCountQuery :IRequest<int>
    {
        public string SubjectName { get; set; }
    }
}
