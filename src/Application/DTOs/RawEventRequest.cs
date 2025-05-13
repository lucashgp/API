namespace src.Application.DTOs
{
    public class RawEventRequest
    {
        public string Type { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public int Amount { get; set; }

        public EventRequest ToEventRequest()
        {
            return new EventRequest
            {
                Type = this.Type,
                Origin = Origin != null ? new AccountDTO { Id = Origin } : null,
                Destination = Destination != null ? new AccountDTO { Id = Destination } : null,
                Amount = this.Amount
            };
        }
    }
    public class EventRequest
    {
        public string Type { get; set; }
        public AccountDTO? Origin { get; set; }
        public AccountDTO? Destination { get; set; }
        public int Amount { get; set; }
    }
}
