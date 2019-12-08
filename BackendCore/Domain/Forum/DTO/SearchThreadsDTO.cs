using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.DTO
{
    public class SearchThreadsDTO
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActivity { get; set; }
        public string Author { get; set; }
    }
}
