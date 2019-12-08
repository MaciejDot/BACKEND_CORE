using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class Article
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ThumbnailId { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }

        public virtual AspNetUsers Author { get; set; }
        public virtual Thumbnails Thumbnail { get; set; }
    }
}
