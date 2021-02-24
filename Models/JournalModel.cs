using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rurouni_v2.Models
{
    public class JournalModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Enter the date of workout")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Enter the description of your workout")]
        public String Description { get; set; }
    }
}
