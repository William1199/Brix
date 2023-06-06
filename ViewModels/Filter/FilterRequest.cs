using Data.Enum;

namespace ViewModels.Filter
{
    public class FilterRequest
    {
        public string? Title { get; set; }

        public List<string>? Salary { get; set; }

        public List<Place>? Places { get; set; }

        public List<PostCategories>? Categories { get; set; }

        public List<Guid>? Types { get; set; }

        public int? Participant { get; set; }

        public bool? Accommodation { get; set; }

        public bool? Transport { get; set; }

        public Status? Status { get; set; }



    }
}
