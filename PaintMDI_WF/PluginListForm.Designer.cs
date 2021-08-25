namespace PaintMDI_WF
{
    partial class PluginListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataTable = new System.Windows.Forms.DataGridView();
            this.PluginName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PluginAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PluginVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataTable
            // 
            this.dataTable.AllowUserToAddRows = false;
            this.dataTable.AllowUserToDeleteRows = false;
            this.dataTable.AllowUserToResizeColumns = false;
            this.dataTable.AllowUserToResizeRows = false;
            this.dataTable.BackgroundColor = System.Drawing.Color.White;
            this.dataTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataTable.ColumnHeadersHeight = 30;
            this.dataTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PluginName,
            this.PluginAuthor,
            this.PluginVersion});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataTable.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataTable.Location = new System.Drawing.Point(13, 53);
            this.dataTable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataTable.Name = "dataTable";
            this.dataTable.ReadOnly = true;
            this.dataTable.RowHeadersVisible = false;
            this.dataTable.RowHeadersWidth = 35;
            this.dataTable.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataTable.Size = new System.Drawing.Size(900, 444);
            this.dataTable.TabIndex = 0;
            // 
            // PluginName
            // 
            this.PluginName.HeaderText = "Название";
            this.PluginName.Name = "PluginName";
            this.PluginName.ReadOnly = true;
            this.PluginName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PluginName.Width = 300;
            // 
            // PluginAuthor
            // 
            this.PluginAuthor.HeaderText = "Автор";
            this.PluginAuthor.Name = "PluginAuthor";
            this.PluginAuthor.ReadOnly = true;
            this.PluginAuthor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PluginAuthor.Width = 300;
            // 
            // PluginVersion
            // 
            this.PluginVersion.HeaderText = "Версия";
            this.PluginVersion.Name = "PluginVersion";
            this.PluginVersion.ReadOnly = true;
            this.PluginVersion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PluginVersion.Width = 299;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::PaintMDI_WF.Properties.Resources.Green_and_Blue;
            this.panel1.Controls.Add(this.backButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 35);
            this.panel1.TabIndex = 2;
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Consolas", 12F);
            this.backButton.Location = new System.Drawing.Point(0, 0);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(126, 35);
            this.backButton.TabIndex = 2;
            this.backButton.Text = "◀ Назад";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            this.backButton.MouseEnter += new System.EventHandler(this.backButton_MouseEnter);
            this.backButton.MouseLeave += new System.EventHandler(this.backButton_MouseLeave);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Consolas", 12F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(926, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "◢ Список установленных плагинов ◣";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PluginListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(926, 511);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataTable);
            this.Font = new System.Drawing.Font("Consolas", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PluginListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Плагины – My Little Paint";
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn PluginName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PluginAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn PluginVersion;
    }
}