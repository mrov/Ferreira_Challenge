using Utils.Enums;

namespace Models.DTOs.Input
{
    public class UserFilterDTO
    {
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? CPF { get; set; }
        public DateTime? StartDateBirth { get; set; }
        public DateTime? EndDateBirth { get; set; }
        public DateTime? StartInsertedAt { get; set; }
        public DateTime? EndInsertedAt { get; set; }
        public DateTime? StartUpdatedAt { get; set; }
        public DateTime? EndUpdatedAt { get; set; }
        public int? StartAge { get; set; }
        public int? EndAge { get; set; }
        public Status? Status { get; set; }
        public int? PageNumber { get; set; }
    }
}