using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;


namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface ISelectDriverLocalViewModel : IBaseSelectDriverViewModel
    {
       
    }

    [OfflineViewModel]
    public class SelectDriverLocalViewModel : BaseSelectDriverViewModel, ISelectDriverLocalViewModel
    {

        

        #region Contructor
        

        public SelectDriverLocalViewModel(IOfflineShopService shopService)
        {
            _shopService = shopService;
            IsCompleted = false;
        }

        #endregion

        #region Methods

        public override void DoSelectDriver()
        {
            if (SelectedDriver == null)
                return;

            SelectedOrder.DriverName = SelectedDriver.Name;
            _shopService.SaveOrder(SelectedOrder);

            IsCompleted = true;
            this.TryClose();
        }

        #endregion
    }
}