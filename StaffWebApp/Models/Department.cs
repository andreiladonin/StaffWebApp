using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StaffWebApp.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Наименование отдела")]
        public string Name { get; set; }
    }
}
