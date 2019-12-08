using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class ExerciseGroup
    {
        public ExerciseGroup()
        {
            ExerciseJoinExerciseGroup = new HashSet<ExerciseJoinExerciseGroup>();
        }

        public string Name { get; set; }

        public virtual ICollection<ExerciseJoinExerciseGroup> ExerciseJoinExerciseGroup { get; set; }
    }
}
