using Data.Enum;

namespace ViewModels.BuilderPosts
{
    public class BuilderPostDTO
    {
        public int Id { get; set; }

        public PostCategories PostCategories { get; set; }
        public Place Place { get; set; }
        public string? AuthorName { get; set; }
        public string? Title { get; set; }
        public bool? IsSaved { get; set; }
        public string? Description { get; set; }
        public int BuilderID { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public string? Avatar { get; set; }
    }
}
