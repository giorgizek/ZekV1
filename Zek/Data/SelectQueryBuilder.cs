using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zek.Data
{
    public class SelectQueryBuilder
    {
        private string _tableName;

        public void SelectFromTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException(@"tableName argument is required", "tableName");


        }
    }
}
