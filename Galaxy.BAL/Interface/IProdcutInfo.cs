using Galaxy.BAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL.Interface
{
    public interface IProdcutInfo
    {
        MultipleTimeSeriesViewModel FetchProductNetValueDistViewModel(int productId);
        List<TimeSeriesDataViewModel> FetchPiggyBackDistViewModel(int productId);
        List<CategoryDataViewModel> FetchReturnDistViewModel(int productId,DateTime asOfDate);
        List<CategoryDataViewModel> FetchPnLDistViewModel(int productId,DateTime asOfDate);
        List<ProductBriefViewModel> FetchProducts();
        ProductBriefViewModel FetchProduct(int productId,DateTime asOfDate);

    }
}
