using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.DAL.Model
{
      [Serializable]
    public class SecurityInfo
    {
        [Column(Order = 0),Key]
        public String SecurityID { get; set; }

        [Column(Order = 1), Key]
        public DateTime UpdatedDate { get; set; }

        public String SecurityName { get; set; }

        [ForeignKey("InducstryInfo")]
        public int IndustryID { get; set; }
        public virtual InducstryInfo InducstryInfo { get; set; }
    }
}
