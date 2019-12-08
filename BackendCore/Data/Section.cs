using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class Section
    {
        public int Id { get; set; }
        public string Exercise { get; set; }
        public string Name { get; set; }
        public int PositionPriority { get; set; }
        public string Content { get; set; }

        public virtual Exercise ExerciseNavigation { get; set; }
    }
}
