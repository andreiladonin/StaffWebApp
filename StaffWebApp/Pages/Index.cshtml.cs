using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StaffWebApp.Context;
using StaffWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StaffWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly StaffDbContext _db;
        public List<Employee> Employees { get; set; }
        public readonly List<SelectListItem> DepartmentSelectLists;

        public IndexModel(StaffDbContext db)
        {
            _db = db;
            DepartmentSelectLists = _db.Departments.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList();
            DepartmentSelectLists.Add(new SelectListItem
            {
                Text = "Все",
                Value = 0.ToString()
            }) ;
        }

        public void OnGet()
        {
            Employees = _db.Employees.Include(e => e.Department).ToList();
        }

        public void OnGetFilter(string name, string last_name, int? department, SortState sort = SortState.Default)
        {
            IQueryable<Employee> employees = _db.Employees.Include(e => e.Department);
            if (department != null && department != 0)
            {
                employees = employees.Where(e => e.DepartmentId == department);
            }
            if (!String.IsNullOrEmpty(name))
            {
                employees = employees.Where(p => p.Name.Contains(name));
            }
            if (!String.IsNullOrEmpty(last_name))
            {
                employees = employees.Where(p => p.LastName.Contains(last_name));
            }

            employees = sort switch
            {
                SortState.NameAsc => employees.OrderBy(e => e.Name),
                SortState.LastNameAsc => employees.OrderBy(e => e.LastName),
                SortState.DepartmentAsc => employees.OrderBy(e => e.Department.Name),
                SortState.Default => employees
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
