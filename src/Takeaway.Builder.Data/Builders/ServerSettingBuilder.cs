using System;
using Takeaway.Data.Entities.Settings;

namespace Takeaway.Builder.Data.Builders
{
    public class ServerSettingBuilder
    {

        private readonly ServerSetting _entity = new ServerSetting();

        public ServerSettingBuilder()
        {
            _entity.Created = DateTime.Now;
            _entity.SettingKey = "SettingKey";
            _entity.SettingValue = "SettingValue";
        }

        public ServerSetting Build()
        {
            return _entity;
        }
    }
}