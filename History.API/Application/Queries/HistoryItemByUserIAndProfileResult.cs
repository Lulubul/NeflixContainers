using System;
using System.Collections.Generic;
using History.API.Application.Model;

namespace History.API.Application.Queries
{
    public class HistoryItemByUserIAndProfileResult
    {
        public readonly IEnumerable<HistoryItem> HistoryItems;

        public HistoryItemByUserIAndProfileResult(IEnumerable<HistoryItem> historyItems)
        {
            HistoryItems = historyItems;
        }
    }
}