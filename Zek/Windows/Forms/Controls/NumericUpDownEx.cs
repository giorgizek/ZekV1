using System.Windows.Forms;

namespace Zek.Windows.Forms
{
    /// <summary>
    /// NumericUpDown - გასწორებულია ReadOnly.
    /// </summary>
    public class NumericUpDownEx : NumericUpDown
    {
        public override void DownButton()
        {
            if (ReadOnly) return;
            base.DownButton();
        }

        public override void UpButton()
        {
            if (ReadOnly) return;
            base.UpButton();
        }
    }
}
