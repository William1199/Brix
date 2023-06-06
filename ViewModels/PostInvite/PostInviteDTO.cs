using Data.Enum;

namespace ViewModels.PostInvite
{
    public class PostInviteDTO
    {
        public int Id { get; set; }
        public int? ContractorId { get; set; }
        public int? BuilderId { get; set; }
        public string? BuilderName { get; set; }
        public string? ContractorName { get; set; }
        public string? CompanyName { get; set; }
        public string? ContractorPostName { get; set; }
        public int? ContractorPostId { get; set; }
        public Place Places { get; set; }
        public string? Salaries { get; set; }

        public Status Status { get; set; }

        public bool? IsSave { get; set; }

        public DateTime LastModifiedAt { get; set; } = DateTime.Now;

        public bool IsRead { get; set; }
    }
}
