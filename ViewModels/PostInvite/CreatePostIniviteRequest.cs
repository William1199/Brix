using System.ComponentModel.DataAnnotations;

namespace ViewModels.PostInvite
{
    public class CreatePostIniviteRequest
    {
        public int? ContractorId { get; set; }

        [Required]
        public int BuilderId { get; set; }
        [Required]
        public int ContractorPostId { get; set; }
    }
}
