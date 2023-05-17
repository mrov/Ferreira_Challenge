using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Input
{
    public class CreateUserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string MotherName { get; set; }
    }
}
