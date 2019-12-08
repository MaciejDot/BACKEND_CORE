using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class ExerciseJoinExerciseGroup
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public string ExerciseGroupName { get; set; }

        public virtual ExerciseGroup ExerciseGroupNameNavigation { get; set; }
        public virtual Exercise ExerciseNameNavigation { get; set; }
    }
}
