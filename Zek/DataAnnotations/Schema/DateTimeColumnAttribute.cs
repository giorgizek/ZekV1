using System.ComponentModel.DataAnnotations.Schema;

namespace Zek.DataAnnotations.Schema
{
    public class DateTimeColumnAttribute : ColumnAttribute
    {
        public DateTimeColumnAttribute()
        {
            TypeName = "datetime2";
        }
    }
}
