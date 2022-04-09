using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StaffWebApp.Context;
using StaffWebApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StaffWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly StaffDbContext _db;
        public List<Employee> Employees { get; set; }
        public readonly List<SelectListItem> DepartmentSelectLists = new();

        public IndexModel(StaffDbContext db)
        {
            _db = db;
            DepartmentSelectLists.Add(new SelectListItem
            {
                Text = "Все",
                Value = "0"
            });
            DepartmentSelectLists.AddRange(_db.Departments.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList());
        }

        public void OnGet()
        {
            Employees = _db.Employees.Include(e => e.Department).OrderBy(e => e.Id).ToList();
        }

        public void OnGetFilter(string name, string last_name, int? department, string adress, SortState sort = SortState.Default)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            IQueryable<Employee> employees = _db.Employees.Include(e => e.Department);
            if (department != null && department != 0)
            {
                employees = employees.Where(e => e.DepartmentId == department);
            }
            if (!String.IsNullOrEmpty(name))
            {
                employees = employees.Where(p => p.Name.Contains(ti.ToTitleCase(name)));
            }
            if (!String.IsNullOrEmpty(last_name))
            {
                employees = employees.Where(p => p.LastName.Contains(ti.ToTitleCase(last_name)));
            }
            if (!String.IsNullOrEmpty(adress))
            {
                employees = employees.Where(p => p.Location.Contains(ti.ToTitleCase(adress)));
            }

            employees = sort switch
            {
                SortState.NameAsc => employees.OrderBy(e => e.Name),
                SortState.LastNameAsc => employees.OrderBy(e => e.LastName),
                SortState.DepartmentAsc => employees.OrderBy(e => e.Department.Name),
                SortState.Default => employees.OrderBy(e=> e.Id)
            };


            Employees = employees.ToList();
        }
        public IActionResult OnPostDelete(int? id)
        {
            var obj = _db.Employees.Find(id);
            if (obj != null)
            {
                _db.Employees.Remove(obj);
                _db.SaveChanges();
            }
            return RedirectToPage("Index");
        }
    }
}
