using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BackendCore.Models.Article
{
    public class CreateArticle
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
