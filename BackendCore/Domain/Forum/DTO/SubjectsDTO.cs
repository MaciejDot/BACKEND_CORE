using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.DTO
{
    public class SubjectsDTO
    {
        public IEnumerable<GetForumSubjectDTO> Subjects { get; set; }
    }
}
