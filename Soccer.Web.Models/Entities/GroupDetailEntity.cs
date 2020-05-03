﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Soccer.Web.Models.Entities
{
    public class GroupDetailEntity
    {
        public int Id { get; set; }

        public TeamEntity Team { get; set; }

        [Display(Name = "Matches Played")]
        public int MatchesPlayed { get; set; }

        [Display(Name = "Matches Won")]
        public int MatchesWon { get; set; }

        [Display(Name = "Matches Tied")]
        public int MatchesTied { get; set; }

        [Display(Name = "Matches Lost")]
        public int MatchesLost { get; set; }

        public int Points => MatchesWon * 3 + MatchesTied;

        [Display(Name = "Goals For")]
        public int GoalsFor { get; set; }

        [Display(Name = "Goals Against")]
        public int GoalsAgainst { get; set; }

        public int GoalDifference => GoalsFor - GoalsAgainst;

        public GroupEntity Group { get; set; }
    }
}
