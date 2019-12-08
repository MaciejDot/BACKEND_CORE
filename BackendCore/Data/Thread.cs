using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class Thread
    {
        public Thread()
        {
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string AuthorId { get; set; }
        public DateTime Created { get; set; }
        public int SubjectId { get; set; }

        public virtual AspNetUsers Author { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
