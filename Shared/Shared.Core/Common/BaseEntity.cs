using Shared.Core.Common.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models.Core
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        public int  Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        private readonly List<BaseEvent> _domainEvents = new();
        private readonly List<DomainEvent> _backgroundDomainEvents = new();
        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();
        [NotMapped]
        public IReadOnlyCollection<DomainEvent> BackgroundDomainEvents => _backgroundDomainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public void AddBackgroundDomainEvent(DomainEvent domainEvent)
        {
            _backgroundDomainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
        public void RemoveBackgroundDomainEvent(DomainEvent domainEvent)
        {
            _backgroundDomainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        public void ClearBackgroundDomainEvents()
        {
            _backgroundDomainEvents.Clear();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 41;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj.GetType() != GetType()) return false;

            if (obj is not BaseEntity entity) return false;

            return entity.Id == Id;
        }

        public bool Equals(BaseEntity? other)
        {
            if (other is null) return false;

            if (other.GetType() != GetType()) return false;

            if (other is not BaseEntity entity) return false;

            return entity.Id == Id;
        }

        public static bool operator ==(BaseEntity? first, BaseEntity? second)
        {
            return first is not null && second is not null && first.Equals(second);
        }
        public static bool operator !=(BaseEntity? first, BaseEntity? second)
        {
            return !(first == second);
        }

    }
}
