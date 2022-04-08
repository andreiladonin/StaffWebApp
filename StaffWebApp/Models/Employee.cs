using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffWebApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }
        [Required]
        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirthday { get; set; }
        [Required]
        [DisplayName("Адрес Проживания")]
        public string Location { get; set; }
        [Required]
        [DisplayName("Отдел")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
