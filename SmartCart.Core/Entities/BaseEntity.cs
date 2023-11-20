namespace SmartCart.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedDate { get; set; }
    }
}
