using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Monster.Inc.Web.Models
{
    public class WorkDay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public DateTime? EndTime { get; set; }

        public double? EnergyCollected { get; set; }


        public Intimidiator Intimidiator { get; set; }

        
        public Door Door { get; set; }

        [Required]
        public int IntimidiatorId { get; set; }

        [Required]
        public int DoorId { get; set; }
    }
}
