using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Interfaces
{
    public interface IDomainEventPublisher
    {
        void Publish<TEvent>(TEvent @event);
    }
}
