namespace Data.Entities
{
    public class BaseEntity
    {
        public DateTime LastModifiedAt { get; set; } = DateTime.Now;

        public Guid CreateBy { get; set; }
    }
}