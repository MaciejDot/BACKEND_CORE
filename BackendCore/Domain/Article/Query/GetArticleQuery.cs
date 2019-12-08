using BackendCore.Domain.Article.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Article.Query
{
    public class GetArticleQuery : IRequest<GetArticleDTO>
    {
        public int Id { get; set; }
    }
}
