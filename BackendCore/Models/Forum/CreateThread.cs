using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BackendCore.Models.Forum
{
    public class CreateThread
    {
        [MinLength(1)]
        [MaxLength(4000)]
        public string SubjectName { get; set; }
        [MinLength(1)]
        [MaxLength(4000)]
        public string Content { get; set; }
        [MinLength(1)]
        [StringLength(300)]
        public string Title { get; set; }
    }
}
