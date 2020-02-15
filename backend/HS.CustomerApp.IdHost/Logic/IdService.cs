using System;

namespace HS.CustomerApp.IdHost.Logic
{
    public class IdService : IIdService
    {
        private Random _random = new Random();

        public long Generate()
        {
            return DateTime.Now.TimeOfDay.Milliseconds % 2 == 0
                ? _random.Next()
                : throw new ApplicationException("I'm in a meeting man!");
        }
    }
}