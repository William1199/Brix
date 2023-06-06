using Data.Enum;

namespace Data.Entities
{
    public class Verify : BaseEntity
    {
        public int Id { get; set; }

        public string? FaceImage { get; set; }

        public string? FrontID { get; set; }

        public string? BackID { get; set; }

        public string? BusinessLicense { get; set; }

        public User? User { get; set; }

        public Guid? UserID { get; set; }

        public IdentificateType IdentificateType { get; set; }

        public string? PreCodition { get; set; }

        public Status Status { get; set; }

    }
}
