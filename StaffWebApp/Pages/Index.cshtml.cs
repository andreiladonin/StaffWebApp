using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StaffWebApp.Context;
using StaffWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace StaffWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly StaffDbContext _db;
        public List<Employee> Employees { get; set; }

        public IndexModel(StaffDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Employees = _db.Employees.Include(e => e.Department).ToList();
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
