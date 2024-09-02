using System.Diagnostics;

    public static class Heartbeat
    {
        public static string GetStatus()
        {
            return "OK";
        }

        public static string GetUptime()
        {
            return (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        }

        public static object GetHeartbeat()
        {
            return new 
            {
                Status = GetStatus(),
                Uptime = GetUptime()
            };
        }
    }
 