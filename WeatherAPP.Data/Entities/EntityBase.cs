namespace WeatherAPP.Data.Entities
{
    public class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
