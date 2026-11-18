using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeway.Web.ViewModels.Admin
{
    public class ServerSettingViewModel: ViewModelBase
    {
        public List<WsSetting> Settings { get; set; }
        public WsSetting SelectedSetting { get; set; }
    }
}