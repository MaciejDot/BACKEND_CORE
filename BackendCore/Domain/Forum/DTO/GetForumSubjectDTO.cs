using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.DTO
{
    public class GetForumSubjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ThumbnailId { get; set; }
        public int PostCount { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
