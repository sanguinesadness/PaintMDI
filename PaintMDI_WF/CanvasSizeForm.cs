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
    public partial class CanvasSizeForm : Form
    {
        public int UserWidth { get; private set; }
        public int UserHeight { get; private set; }

        public CanvasSizeForm(MainForm parent)
        {
            InitializeComponent();

            applyButton.DialogResult = DialogResult.OK;

            widthBox.Value = parent.ActiveMdiChild.Width;
            heightBox.Value = parent.ActiveMdiChild.Height;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            UserWidth = (int)widthBox.Value;
            UserHeight = (int)heightBox.Value;
            this.Close();
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            (sender as Button).ForeColor = Color.White;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).ForeColor = Color.Black;
        }
    }
}
