using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ContractorPost;

namespace ViewModels.BuilderPosts
{
    public class BuilderPostRequestDTO
    {
        public PostCategories PostCategories { get; set; }
        public List<TypeModels>? type { get; set; }

        public Place Place { get; set; }
        public Status Status { get; set; }
        public string Salaries { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Field { get; set; }
    }
}
