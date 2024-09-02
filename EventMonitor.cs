public class EventMonitor
    {
        private readonly Dictionary<int, HeartbeatEvent> _events = new();

        public void AddOrUpdateEvent(int id)
        {
            _events[id] = new HeartbeatEvent(id);
        }

        public bool IsEventStillAlive(int id, int waitTimeInSeconds)
        {
            if (_events.ContainsKey(id) && _events[id].IsValid(waitTimeInSeconds))
            {
                return true;
            }
            return false;
        }
    }
