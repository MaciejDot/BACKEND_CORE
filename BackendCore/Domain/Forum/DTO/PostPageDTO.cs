using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.DTO
{
    public class PostPageDTO
    {
        public GetForumThreadDTO Thread { get; set; }
        public IEnumerable<GetForumPostsDTO> Posts { get; set; }
        public int AllPostsCount { get; set; }
    }
}
