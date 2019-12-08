using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class Thumbnails1
    {
        public Thumbnails1()
        {
            Subject = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public byte[] Content { get; set; }

        public virtual ICollection<Subject> Subject { get; set; }
    }
}
