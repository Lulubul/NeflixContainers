using MediatR;
using System;
using History.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using History.Infrastructure.Entities;
using History.API.Application.IntegrationEvents;

namespace History.API.Application.Commands
{
    public class CreateHistoryItemCommandHandler : IRequestHandler<CreateHistoryItemCommand, bool>
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IHistoryIntegrationEventService _historyIntegrationEventService;
        private readonly IMapper _mapper;

        public CreateHistoryItemCommandHandler(IHistoryRepository historyRepository, IHistoryIntegrationEventService historyIntegrationEventService, IMapper mapper)
        {
            _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
            _historyIntegrationEventService = historyIntegrationEventService ?? throw new ArgumentNullException(nameof(historyIntegrationEventService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(CreateHistoryItemCommand request, CancellationToken cancellationToken)
        {
            await _historyIntegrationEventService.PublishThroughEventBusAsync(_mapper.Map<CreateHistoryItemCommand, HistoryUpdatedIntegrationEvent>(request));
            return await _historyRepository.AddAsync(_mapper.Map<CreateHistoryItemCommand, HistoryEntity>(request));
        }
    }
}
