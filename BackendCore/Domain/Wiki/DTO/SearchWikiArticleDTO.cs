using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Wiki.DTO
{
    public class SearchWikiArticleDTO
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}
