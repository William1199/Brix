using System.Text.Json.Serialization;

namespace Data.Entities
{
    public class Quiz
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public Guid? TypeID { get; set; }
        [JsonIgnore]
        public Type? Types { get; set; }

        public int PostID { get; set; }

        [JsonIgnore]
        public ContractorPost ContractorPost { get; set; }

        public DateTime LastModifiedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public List<Question> Questions { get; set; }
        [JsonIgnore]
        public List<AppliedPost>? AppliedPosts { get; set; }
    }
}
