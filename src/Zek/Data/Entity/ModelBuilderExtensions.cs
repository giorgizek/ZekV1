using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Zek.Data.Entity
{
    public static class ModelBuilderExtensions
    {
        public static void InitConventions(this ModelBuilder builder)
        {
            foreach (var type in builder.Model.GetEntityTypes())
            {
                var tableName = type.SqlServer().TableName;

                foreach (var key in type.GetKeys())
                {
                    key.Relational().Name = "PK_" + tableName;
                }

                foreach (var index in type.GetIndexes())
                {
                    //if (type.ClrType.Name == "HotelBuilding")
                    //{

                    //}
                    index.Relational().Name = (index.IsUnique ? "AK_" : "IX_") + tableName + "_" + string.Join("_", index.Properties.Select(p => p.Name));
                }

                foreach (var fk in type.GetForeignKeys())
                {
                    var masterTable = fk.PrincipalEntityType.SqlServer().TableName;
                    var name = "FK_" + tableName + "_" + masterTable + "_" + string.Join("_", fk.Properties.Select(p => p.Name));
                    fk.Relational().Name = name;
                }
            }
        }
    }
}
