using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StaffWebApp.Context;
using StaffWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApp.Pages
{
    public class CreateOrEditModel : PageModel
    {
        [BindProperty]
        public Employee Employee { get; set; }

        private readonly StaffDbContext _db;
        public readonly List<SelectListItem> DepartmentSelectLists;
        [BindProperty]
        public int Id { get; set; }

        public CreateOrEditModel(StaffDbContext db)
        {
            _db = db;
            DepartmentSelectLists = _db.Departments.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList();
        }

        public void OnGet()
        {

        }
        public void OnGetById(int id)
        {
            Id = id;
            Employee = _db.Employees.Find(id);
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Id == 0)
            {
                await _db.Employees.AddAsync(Employee);

            }
            else
            {
                _db.Employees.Update(Employee);
            }

            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
