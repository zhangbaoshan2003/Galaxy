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
    public class ProductNetValue
    {
        [Column(Order = 0), Key]
        public int ProductID { get; set; }

        [Column(Order = 1),DataType(DataType.Date), Key]
        public DateTime UpdatedDate { get; set; }

        public double NetValue { get; set; }
    }
}
