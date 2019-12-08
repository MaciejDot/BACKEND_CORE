using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class Thumbnails
    {
        public Thumbnails()
        {
            Article = new HashSet<Article>();
        }

        public int Id { get; set; }
        public byte[] Content { get; set; }

        public virtual ICollection<Article> Article { get; set; }
    }
}
