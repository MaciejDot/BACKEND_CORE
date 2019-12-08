using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class Subject
    {
        public Subject()
        {
            Thread = new HashSet<Thread>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriprion { get; set; }
        public int ThumbnailId { get; set; }

        public virtual Thumbnails1 Thumbnail { get; set; }
        public virtual ICollection<Thread> Thread { get; set; }
    }
}
