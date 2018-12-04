using System.Collections.Generic;

namespace Day04
{
    public class Guard
    {
        public int Id { get; set; }
        public List<int> MinutesAsleep { get; set; }

        public Guard(int id)
        {
            Id = id;
            MinutesAsleep = new List<int>();
        }
    }
}
