using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Models.Wiki
{
    public class ExercisePost
    {
        public string Name { get; set; }
        public IEnumerable<SectionPost> Sections { get; set; }
    }
}
