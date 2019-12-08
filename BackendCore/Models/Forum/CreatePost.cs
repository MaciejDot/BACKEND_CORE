using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BackendCore.Models.Forum
{
    public class CreatePost
    {
        public int ThreadId { get; set; }
        [MaxLength(4000)]
        public string Content { get; set; }
    }
}
