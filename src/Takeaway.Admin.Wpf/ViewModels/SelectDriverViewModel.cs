using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface ISelectDriverViewModel:IBaseSelectDriverViewModel
    {
      
    }

    [OfflineViewModel]
    public class SelectDriverViewModel : BaseSelectDriverViewModel,ISelectDriverViewModel
    {

        

        #region Constructors
        
        

        public SelectDriverViewModel(IOfflineShopService shopService)
        {
            _shopService = shopService;
            IsCompleted = false;
        }

        #endregion

        #region Methods
        

        #endregion
    }
}