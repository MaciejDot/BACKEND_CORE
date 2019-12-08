using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Article.DTO
{
    public class GetArticleDTO
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime Created { get; set; }
    }
}
