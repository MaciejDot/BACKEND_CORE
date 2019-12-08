using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Article.Query
{
    public class GetArticleCountQuery :IRequest<int>
    {
    }
}
