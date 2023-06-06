using Data.Enum;

namespace ViewModels.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }

        public string? IdNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? Avatar { get; set; }
        public string? Role { get; set; }
        public Status? Status { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public int? StoreID { get; set; }
        public int? BuilderID { get; set; }
        public int? ContractorID { get; set; }
        public bool Premium { get; set; }


        public DetailBuilder? Builder { get; set; }
        public DetailContractor? Contractor { get; set; }
        public DetailMaterialStore? DetailMaterialStore { get; set; }
    }
}
