using BackendCore.Domain.Wiki.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Wiki.Query
{
    public class GetWikiArticleQuery : IRequest<WikiArticleDTO>
    {
        public string Name { get; set; }
    }
}
