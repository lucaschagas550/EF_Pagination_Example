namespace EF_Pagination_Example.Model
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        protected Entity() =>
            Id = Guid.NewGuid();
    }
}