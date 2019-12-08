using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Models.Forum
{
    public class CreateSubject
    {
        [MaxLength(300)]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
