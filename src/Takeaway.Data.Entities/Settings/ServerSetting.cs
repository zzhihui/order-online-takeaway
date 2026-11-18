namespace Takeaway.Data.Entities.Settings
{
    public partial class ServerSetting : BaseDataEntity
    {
        
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }

        public bool? IsServer { get; set; }
        
    }
}
