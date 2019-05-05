using MediatR;
using System.ComponentModel.DataAnnotations;
using System;
using History.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace History.API.Application.Commands
{
    public class CreateHistoryItemCommandHandler : IRequestHandler<CreateHistoryItemCommand, bool>
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IMapper _mapper;

        public CreateHistoryItemCommandHandler(IHistoryRepository historyRepository, IMapper mapper)
        {
            _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(CreateHistoryItemCommand request, CancellationToken cancellationToken)
        {
            return await _historyRepository.AddAsync(_mapper.Map<CreateHistoryItemCommand, HistoryEntity>(request));
        }
    }
}
