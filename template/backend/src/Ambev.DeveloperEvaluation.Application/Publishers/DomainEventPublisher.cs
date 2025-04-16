using Ambev.DeveloperEvaluation.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Publishers
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly ILogger<DomainEventPublisher> _logger;

        public DomainEventPublisher(ILogger<DomainEventPublisher> logger)
        {
            _logger = logger;
        }

        public void Publish<TEvent>(TEvent @event) 
        {
            _logger.LogInformation("Publishing event: {@DomainEvent}", @event);
        }
    }
}
