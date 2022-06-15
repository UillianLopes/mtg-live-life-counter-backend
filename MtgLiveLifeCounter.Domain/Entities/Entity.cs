namespace MtgLiveLifeCounter.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTime Creation { get; private set; }
        public DateTime? Update { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            Creation = DateTime.Now;
        }
    }
}
