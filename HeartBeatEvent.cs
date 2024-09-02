public record HeartbeatEvent
    {
        public int Id { get; init; }
        public DateTime Timestamp { get; init; }

        public HeartbeatEvent(int id)
        {
            Id = id;
            Timestamp = DateTime.UtcNow;
        }

        public bool IsValid(int waitTimeInSeconds)
        {
            return (DateTime.UtcNow - Timestamp).TotalSeconds <= waitTimeInSeconds;
        }
    }