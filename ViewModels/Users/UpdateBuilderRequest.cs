using Data.Enum;

namespace ViewModels.Users
{
    public class UpdateBuilderRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? Avatar { get; set; }
        public string? IdNumber { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }




        public string? ExperienceDetail { get; set; }
        public int? Experience { get; set; }

        public string? Certificate { get; set; }


        public Place? Place { get; set; }

        public Guid? TypeID { get; set; }

        public List<int>? Skills { get; set; }

        public List<int>? WorkerConstructionType { get; set; }



    }
}
