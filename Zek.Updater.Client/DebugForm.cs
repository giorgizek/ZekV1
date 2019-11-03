using System.Windows.Forms;

namespace Zek.Updater.Client
{
    public partial class DebugForm : Form
    {
        public DebugForm(string text)
        {
            InitializeComponent();
            txtText.Text = text;
        }
    }
}
