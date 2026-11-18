namespace Takeaway.Data.Entities.Settings
{
    public partial class OfflineSetting : BaseDataEntity
    {

        public string Name { get; set; }
        public string SettingValue { get; set; }

    }
}