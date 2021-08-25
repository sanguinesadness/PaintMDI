using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintMDI_WF
{
    public partial class PluginListForm : Form
    {
        public PluginListForm(string[,] plugins)
        {
            InitializeComponent();

            if (plugins.GetLength(1) != 3)
            {
                dataTable.Rows.Add("n", "n", "n");
                return;
            }

            for (int i = 0; i < plugins.GetLength(0); i++)
            {
                dataTable.Rows.Add(plugins[i, 0], plugins[i, 1], plugins[i, 2]);
            }

            FixRowHeight();
        }

        private void FixRowHeight()
        {
            foreach (DataGridViewRow row in dataTable.Rows)
            {
                row.MinimumHeight = 25;
                row.Height = 25;
            }
        }

        private void backButton_MouseEnter(object sender, EventArgs e)
        {
            backButton.BackColor = Color.Transparent;
            backButton.ForeColor = Color.White;
        }

        private void backButton_MouseLeave(object sender, EventArgs e)
        {
            backButton.BackColor = Color.Transparent;
            backButton.ForeColor = Color.Black;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
