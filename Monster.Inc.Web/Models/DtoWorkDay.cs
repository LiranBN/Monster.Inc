using System;

namespace Monster.Inc.Web.Models
{
    public class DtoWorkDay
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime? EndTime { get; set; }

        public double? EnergyCollected { get; set; }
        public int IntimidiatorId { get; set; }
        public string IntimidiatorName { get; set; }
        public int DoorId { get; set; }

        public string DoorName { get; set; }
    }
}