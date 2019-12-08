using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Wiki.DTO
{
    public class WikiArticleDTO
    {
        public string Name { get; set; }
        public IEnumerable<WikiSection> Sections { get; set; }
    }
}
