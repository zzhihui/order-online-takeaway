using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace Takeway.Mobile.Core.Services.DataServices
{
    public enum DbConfigurationEnum
    {
        LastOfferId,
        AllowNotification,
        Name,
        Address,
        Phone,
        LastOrderId,
        UserId,
        Email
    }


    public class DbConfigurationItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string ConfigKey { get; set; }

        public string ConfigValue { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }
    }
}