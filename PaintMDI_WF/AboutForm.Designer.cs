namespace PaintMDI_WF
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.productNameLabel = new System.Windows.Forms.Label();
            this.productVersionLabel = new System.Windows.Forms.Label();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // productNameLabel
            // 
            this.productNameLabel.Location = new System.Drawing.Point(0, 16);
            this.productNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.productNameLabel.Name = "productNameLabel";
            this.productNameLabel.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.productNameLabel.Size = new System.Drawing.Size(228, 27);
            this.productNameLabel.TabIndex = 25;
            this.productNameLabel.Text = "Product Name";
            // 
            // productVersionLabel
            // 
            this.productVersionLabel.Location = new System.Drawing.Point(0, 48);
            this.productVersionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.productVersionLabel.Name = "productVersionLabel";
            this.productVersionLabel.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.productVersionLabel.Size = new System.Drawing.Size(228, 27);
            this.productVersionLabel.TabIndex = 26;
            this.productVersionLabel.Text = "Version";
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.Location = new System.Drawing.Point(0, 80);
            this.copyrightLabel.Margin = new System.Windows.Forms.Padding(0);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.copyrightLabel.Size = new System.Drawing.Size(228, 27);
            this.copyrightLabel.TabIndex = 27;
            this.copyrightLabel.Text = "Copyright";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Location = new System.Drawing.Point(0, 111);
            this.descriptionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.descriptionLabel.Size = new System.Drawing.Size(228, 27);
            this.descriptionLabel.TabIndex = 28;
            this.descriptionLabel.Text = "Description";
            // 
            // okButton
            // 
            this.okButton.BackgroundImage = global::PaintMDI_WF.Properties.Resources.Green_and_Blue;
            this.okButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Location = new System.Drawing.Point(0, 218);
            this.okButton.Margin = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(393, 35);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            this.okButton.MouseEnter += new System.EventHandler(this.okButton_MouseEnter);
            this.okButton.MouseLeave += new System.EventHandler(this.okButton_MouseLeave);
            // 
            // logoPicture
            // 
            this.logoPicture.BackgroundImage = global::PaintMDI_WF.Properties.Resources.art_and_design1;
            this.logoPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoPicture.Location = new System.Drawing.Point(257, 16);
            this.logoPicture.Name = "logoPicture";
            this.logoPicture.Size = new System.Drawing.Size(120, 120);
            this.logoPicture.TabIndex = 29;
            this.logoPicture.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.logoPicture);
            this.panel1.Controls.Add(this.productNameLabel);
            this.panel1.Controls.Add(this.descriptionLabel);
            this.panel1.Controls.Add(this.productVersionLabel);
            this.panel1.Controls.Add(this.copyrightLabel);
            this.panel1.Location = new System.Drawing.Point(4, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 152);
            this.panel1.TabIndex = 30;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::PaintMDI_WF.Properties.Resources.Green_and_Blue;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(393, 35);
            this.panel2.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(393, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "◢ Информация о приложении ◣";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(393, 253);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("Consolas", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label productNameLabel;
        private System.Windows.Forms.Label productVersionLabel;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.PictureBox logoPicture;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
    }
}
