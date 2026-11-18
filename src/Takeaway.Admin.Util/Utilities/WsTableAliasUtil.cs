using System.Collections.Generic;
using System.Linq;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Util.Utilities
{
    public static class WsTableAliasUtil
    {
        private static  Dictionary<int,string> _tableAliasList= new Dictionary<int, string>()
        {
            //{1,"New Table"},
            //{2,"New Some Table"}
        };


        public static void SetTableAlias(List<WsTableAlias> tables)
        {
            _tableAliasList= new Dictionary<int, string>();


            foreach (var alias in tables)
            {
                _tableAliasList.Add(alias.Id, alias.ItemName );
            }
        }


        public static void LoadTableAlias(List<WsTables> tables)
        {
            foreach (var table in tables)
            {
                var findAlias = _tableAliasList.FirstOrDefault(r => r.Key == table.Id);
                table.Alias = findAlias.Equals(default(KeyValuePair<int, string>)) ? table.Name : findAlias.Value;
            }
        }

        public static string GetTableNameById(int tableNumber)
        {
            var findAlias = _tableAliasList.FirstOrDefault(r => r.Key == tableNumber);
            return findAlias.Equals(default(KeyValuePair<int, string>)) ? string.Format("Table {0}", tableNumber) : findAlias.Value;
        }
    }
}