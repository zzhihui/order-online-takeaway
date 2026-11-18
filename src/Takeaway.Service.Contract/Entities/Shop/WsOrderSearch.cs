using System;

namespace Takeaway.Service.Contract.Entities.Shop
{
    public class WsOrderSearch:WsEntity
    {
        public DateTime? SearchDate { get; set; }
    }
}