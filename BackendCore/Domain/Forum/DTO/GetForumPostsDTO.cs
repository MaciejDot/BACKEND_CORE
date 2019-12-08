using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.DTO
{
    public class GetForumPostsDTO
    {
        public string Author { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Content {get; set;}
    }
}
