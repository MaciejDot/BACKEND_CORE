using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class MuscleGroup
    {
        public MuscleGroup()
        {
            ExerciseJoinMuscleGroup = new HashSet<ExerciseJoinMuscleGroup>();
        }

        public string Name { get; set; }

        public virtual ICollection<ExerciseJoinMuscleGroup> ExerciseJoinMuscleGroup { get; set; }
    }
}
