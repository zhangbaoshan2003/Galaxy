using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL.Exceptions
{
    public class NotFundException : Exception
    {
        private String errorMsg;
        public NotFundException(String productId,DateTime asOfDate)
        {
            String msg = String.Format("Can't find data for product : {0} @ {1}" , productId,asOfDate.ToShortDateString());
            errorMsg = msg;
        }

        public override String Message
        {
            get { return errorMsg; }
        }
    }
}
