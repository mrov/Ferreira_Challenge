using Utils.Enums;

namespace Models.DTOs.Output
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MotherName { get; set; }
        public Status Status { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
