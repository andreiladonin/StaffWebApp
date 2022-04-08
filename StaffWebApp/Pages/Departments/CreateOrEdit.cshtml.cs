using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StaffWebApp.Context;
using StaffWebApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApp.Pages.Departments
{
    public class CreateOrEditModel : PageModel
    {
        public int name  { get; set; }
        [BindProperty]
        public Department Department { get; set; }

        private readonly StaffDbContext _db;

        [BindProperty]
        public int Id { get; set; }

        public CreateOrEditModel(StaffDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {

        }
        public void OnGetById(int id)
        {
            Id = id;
            Department = _db.Departments.Find(id);
        }

        public async Task<ActionResult> OnPost()
        {
            var departament = await _db.Departments.Where(d => d.Name == Department.Name).FirstOrDefaultAsync();
            if (departament != null)
            {
                ModelState.AddModelError("Department.Name", "Название уже есть");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Id == 0)
            {
                await _db.Departments.AddAsync(Department);

            }
            else
            {
                _db.Departments.Update(Department);
            }

            await _db.SaveChangesAsync();

            return RedirectToPage("Departments");
        }
    }
}
