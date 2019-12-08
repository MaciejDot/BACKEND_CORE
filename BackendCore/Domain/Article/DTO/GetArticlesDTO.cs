using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Article.DTO
{
    public class GetArticlesDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public DateTime Created { get; set; }

        public int ThumbnailId { get; set; }
    }
}
