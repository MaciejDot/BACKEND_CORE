using BackendCore.Domain.Wiki.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Wiki.Query
{
    public class SearchForWikiArticleQuery : IRequest<IEnumerable<SearchWikiArticleDTO>>
    {
        public string Phrase { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
