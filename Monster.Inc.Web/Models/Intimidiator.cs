using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Monster.Inc.Web.Models
{
    public class Intimidiator
    {
        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Tentacles number must be numeric")]
        public string TentaclesNumber { get; set; }

        [Required]
        public DateTime StartedDate { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(450)]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser {get;set;}
    }
}
