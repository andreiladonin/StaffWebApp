using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StaffWebApp.Context;
using StaffWebApp.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StaffWebApp.Pages
{
    public class DepartamentModel : PageModel
    {
        private readonly StaffDbContext _db;

        public List<Department> departments;
        public DepartamentModel(StaffDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            departments = _db.Departments.ToList();
        } 

        public IActionResult OnPostDelete(int? id)
        {
            var obj = _db.Departments.Find(id);
            if (obj != null)
            {
                _db.Departments.Remove(obj);
                _db.SaveChanges();
            }
            return RedirectToPage("Departments");
        }
    }
}
