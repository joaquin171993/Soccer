using Soccer.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Common.Helpers
{
    public class TransformHelper : ITransformHelper
    {
        public List<Group> ToGroups(List<GroupResponse> groupResponse)
        {
            List<Group> list = new List<Group>();

            foreach (GroupResponse g in groupResponse)
            {
                Group group = new Group();
                foreach (GroupDetailResponse groupDetail in g.GroupDetails
                    .OrderByDescending(gd => gd.Points).ThenByDescending(gd => gd.GoalDifference).ThenByDescending(gd => gd.GoalsFor))
                {
                    group.Add(groupDetail);
                }

                group.Name = g.Name;
                list.Add(group);
            }

            return list;
        }
    }
}
