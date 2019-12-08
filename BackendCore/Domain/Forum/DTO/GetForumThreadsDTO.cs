using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.DTO
{
    public class GetForumThreadsDTO
    {
        public int Id { get; set; }
        public int Replies { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActivity { get; set; }
        public string Title { get; set; }
    }
}
