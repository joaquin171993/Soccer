﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Soccer.Web.DataAccess.Data;
using Soccer.Web.DataAccess.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Web.DataAccess
{
    public class ComboHelper : ICombosHelper
    {
        private readonly ApplicationDbContext _context;

        public ComboHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboTeams()
        {
            List<SelectListItem> list = _context.Teams.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a team...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboTeams(int id)
        {
            List<SelectListItem> list = _context.GroupDetails
                .Include(gd => gd.Team)
                .Where(gd => gd.Group.Id == id)
                .Select(gd => new SelectListItem
                {
                    Text = gd.Team.Name,
                    Value = $"{gd.Team.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a team...]",
                Value = "0"
            });

            return list;
        }
    }
}
