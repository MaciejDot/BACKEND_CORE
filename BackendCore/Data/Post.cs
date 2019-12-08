using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Answear { get; set; }
        public string AuthorId { get; set; }
        public DateTime Created { get; set; }
        public int ThreadId { get; set; }

        public virtual AspNetUsers Author { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
