using Data.Enum;

namespace ViewModels.Identification
{
    public class IdentificationDTO
    {
        public int Id { get; set; }

        public string? FaceImage { get; set; }

        public string? FrontID { get; set; }

        public string? BackID { get; set; }

        public string? BusinessLicense { get; set; }

        public Guid? UserID { get; set; }

        public string? Name { get; set; }

        public IdentificateType IdentificateType { get; set; }

        public string? PreCodition { get; set; }

        public Status Status { get; set; }

        public DateTime LastModifiedAt { get; set; }

    }
}
