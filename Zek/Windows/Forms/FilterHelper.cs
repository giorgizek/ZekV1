using System.Windows.Forms;
using Zek.Data;

namespace Zek.Windows.Forms
{
    public class FilterHelper : SqlFilterHelper
    {
        public static void ShowFilterFields(string filterType, Control ctrl1, Control ctrl2 = null)
        {
            if (string.IsNullOrWhiteSpace(filterType))
            {
                ctrl1.Enabled = false;
                if (ctrl2 != null)
                    ctrl2.Enabled = false;
                return;
            }

            switch (filterType.ToLowerInvariant())
            {
                case "between":
                case "notbetween":
                case "[...]":
                case "[..]":
                case "[.]":
                case "[]":
                case "[ ]":
                case "[  ]":
                case "[   ]":
                case "![...]":
                case "![..]":
                case "![.]":
                case "![]":
                case "![ ]":
                case "![  ]":
                case "![   ]":
                    ctrl1.Enabled = true;
                    if (ctrl2 != null)
                        ctrl2.Enabled = true;
                    break;

                default:
                    ctrl1.Enabled = true;
                    if (ctrl2 != null)
                        ctrl2.Enabled = false;
                    break;
            }
        }
    }
}
