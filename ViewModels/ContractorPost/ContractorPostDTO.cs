using Data.Enum;

namespace ViewModels.ContractorPost
{
    public class ContractorPostDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? ProjectName { get; set; }

        public string? Description { get; set; }

        public string? AuthorName { get; set; }

        public DateTime StarDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool? IsSave { get; set; }

        public Place Place { get; set; }

        public PostCategories PostCategories { get; set; }

        public string? Salaries { get; set; }

        public int ViewCount { get; set; }

        public int NumberPeople { get; set; }

        public int ContractorID { get; set; }

        public string? Avatar { get; set; }

        public Guid? _createdBy { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public Status Status { get; set; }

    }
}
