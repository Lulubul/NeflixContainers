using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using History.Infrastructure;
using MediatR;
using History.API.Application.Model;
using History.Infrastructure.Entities;

namespace History.API.Application.Queries
{
    public class PopularItemsByWatchingTypeQueryHandler : IRequestHandler<PopularItemsByWatchingTypeQuery, HistoryItemByUserIAndProfileResult>
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IMapper _mapper;

        public PopularItemsByWatchingTypeQueryHandler(IHistoryRepository historyRepository, IMapper mapper)
        {
            _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<HistoryItemByUserIAndProfileResult> Handle(PopularItemsByWatchingTypeQuery request, CancellationToken cancellationToken)
        {
            List<HistoryEntity> historyItems = await _historyRepository.GetPopularItemsAsync(request.WatchingType);
            return new HistoryItemByUserIAndProfileResult(historyItems.Select(_mapper.Map<HistoryEntity, HistoryItem>));
        }
    }
}