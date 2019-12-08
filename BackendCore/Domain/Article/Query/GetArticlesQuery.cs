using BackendCore.Domain.Article.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Article.Query
{
    public class GetArticlesQuery :IRequest<IEnumerable<GetArticlesDTO>>
    {
        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
