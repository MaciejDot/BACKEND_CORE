using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class Exercise
    {
        public Exercise()
        {
            ExerciseJoinExerciseGroup = new HashSet<ExerciseJoinExerciseGroup>();
            ExerciseJoinMuscleGroup = new HashSet<ExerciseJoinMuscleGroup>();
            Section = new HashSet<Section>();
        }

        public string Name { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<ExerciseJoinExerciseGroup> ExerciseJoinExerciseGroup { get; set; }
        public virtual ICollection<ExerciseJoinMuscleGroup> ExerciseJoinMuscleGroup { get; set; }
        public virtual ICollection<Section> Section { get; set; }
    }
}
