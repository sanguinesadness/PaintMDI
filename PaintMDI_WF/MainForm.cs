using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PluginSupporter;
using System.Configuration;

namespace PaintMDI_WF
{
    public enum Tools
    {
        Grabber,
        Eraser,
        LineShape,
        RectangleShape,
        EllipseShape,
        StarShape
    }

    public partial class MainForm : Form
    {
        private static int canvasCount = 0;
        private Color _brushColor;
        private Color _selectionColor = Color.LightBlue;

        private List<ToolStripItem> _selectableElements;

        public bool RectangleFilled = false;
        public bool EllipseFilled = false;
        public bool StarFilled = false;

        public Color BrushColor
        {
            get => _brushColor;
            private set
            {
                _brushColor = value;

                if (value == Color.Red)
                {
                    redStripItem.BackColor = _selectionColor;
                    blueStripItem.ResetBackColor();
                    greenStripItem.ResetBackColor();
                    customStripItem.ResetBackColor();
                }
                else if (value == Color.Blue)
                {
                    redStripItem.ResetBackColor();
                    blueStripItem.BackColor = _selectionColor;
                    greenStripItem.ResetBackColor();
                    customStripItem.ResetBackColor();
                }
                else if (value == Color.Green)
                {
                    redStripItem.ResetBackColor();
                    blueStripItem.ResetBackColor();
                    greenStripItem.BackColor = _selectionColor;
                    customStripItem.ResetBackColor();
                }
                else
                {
                    redStripItem.ResetBackColor();
                    blueStripItem.ResetBackColor();
                    greenStripItem.ResetBackColor();
                    customStripItem.BackColor = _selectionColor;
                }
            }
        }

        private int _brushWidth;
        public int BrushWidth
        {
            get => _brushWidth;
            private set
            {
                _brushWidth = brushWidthBar.Value = value;

                switch (value)
                {
                    case 1:
                        onePxStripItem.BackColor = _selectionColor;
                        threePxStripItem.ResetBackColor();
                        fivePxStripItem.ResetBackColor();
                        eightPxStripItem.ResetBackColor();
                        break;
                    case 3:
                        onePxStripItem.ResetBackColor();
                        threePxStripItem.BackColor = _selectionColor;
                        fivePxStripItem.ResetBackColor();
                        eightPxStripItem.ResetBackColor();
                        break;
                    case 5:
                        onePxStripItem.ResetBackColor();
                        threePxStripItem.ResetBackColor();
                        fivePxStripItem.BackColor = _selectionColor;
                        eightPxStripItem.ResetBackColor();
                        break;
                    case 8:
                        onePxStripItem.ResetBackColor();
                        threePxStripItem.ResetBackColor();
                        fivePxStripItem.ResetBackColor();
                        eightPxStripItem.BackColor = _selectionColor;
                        break;
                    default:
                        onePxStripItem.ResetBackColor();
                        threePxStripItem.ResetBackColor();
                        fivePxStripItem.ResetBackColor();
                        eightPxStripItem.ResetBackColor();
                        break;
                }
            }
        }

        private Dictionary<string, IPlugin> _plugins = new Dictionary<string, IPlugin>();

        public MainForm()
        {
            InitializeComponent();

            BrushColor = Color.Blue;
            BrushWidth = 3;

            _selectableElements = new List<ToolStripItem>() { grabSelector, eraserSelector, lineSelector,
                                                             rectangleSelector, ellipseSelector, starSelector};

            FindPlugins();
            FillPluginMenu();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Canvas canvas = new Canvas();

            canvas.Text += $" ({++canvasCount})";
            canvas.MdiParent = this;
            canvas.Show();

            SetCanvasSize(canvas.Width, canvas.Height);

            EnableDisableTools();
        }

        public void SetPositions(int x, int y)
        {
            positionStatus.Text = $"{x}, {y}px";

            positionStatus.Visible = true;
            toolStripStatusLabel1.Visible = true;
        }

        public void HidePositions()
        {
            positionStatus.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        public void ClearPositions()
        {
            positionStatus.Text = string.Empty;
        }

        public void SetCanvasSize(int width, int height)
        {
            canvasSizeStatus.Text = $"{width} × {height}px";
        }

        public void ClearCanvasSize()
        {
            canvasSizeStatus.Text = string.Empty;
        }

        private void onePxStripItem_Click(object sender, EventArgs e)
        {
            BrushWidth = 1;
        }

        private void threePxStripItem_Click(object sender, EventArgs e)
        {
            BrushWidth = 3;
        }

        private void fivePxStripItem_Click(object sender, EventArgs e)
        {
            BrushWidth = 5;
        }

        private void eightPxStripItem_Click(object sender, EventArgs e)
        {
            BrushWidth = 8;
        }

        private void redStripItem_Click(object sender, EventArgs e)
        {
            BrushColor = Color.Red;
        }

        private void blueStripItem_Click(object sender, EventArgs e)
        {
            BrushColor = Color.Blue;
        }

        private void greenStripItem_Click(object sender, EventArgs e)
        {
            BrushColor = Color.Green;
        }

        private void eraserSelector_Click(object sender, EventArgs e)
        {
            SelectTool(eraserSelector);
        }

        private void customStripItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.BrushColor = colorDialog.Color;
            }
        }

        private void backColorSelector_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                (ActiveMdiChild as Canvas).SetCanvasBackColor(colorDialog.Color);
            }
        }

        private void grabSelector_Click(object sender, EventArgs e)
        {
            SelectTool(grabSelector);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as Canvas).ClearCanvas();
        }

        private void minusZoom_Click(object sender, EventArgs e)
        {
            Canvas activeCanvas = (ActiveMdiChild as Canvas);
            activeCanvas.ResizeWindow(activeCanvas.Bitmap.Width * 0.8f, activeCanvas.Bitmap.Height * 0.8f);
        }

        private void plusZoom_Click(object sender, EventArgs e)
        {
            Canvas activeCanvas = (ActiveMdiChild as Canvas);
            activeCanvas.ResizeWindow(activeCanvas.Bitmap.Width * 1.2f, activeCanvas.Bitmap.Height * 1.2f);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as Canvas).SaveAs();
        }

        private void файлToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem.Enabled = (ActiveMdiChild != null);
            сохранитьКакToolStripMenuItem.Enabled = (ActiveMdiChild != null);
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is null)
            {
                return;
            }

            AskUserForSaveFile(ActiveMdiChild as Canvas);

            EnableDisableTools();
        }

        private void AskUserForSaveFile(Canvas canvas)
        {
            DialogResult userResponse = MessageBox.Show($"Сохранить файл {canvas.Text} перед закрытием?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (userResponse == DialogResult.Yes)
            {
                canvas.Save();
                canvas.Close();
            }
            else if (userResponse == DialogResult.No)
            {
                canvas.Close();
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файлы JPEG (*.jpg)|*.jpg|Windows Bitmap (*.bmp)|*.bmp";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                Canvas newCanvas = new Canvas(openDialog.FileName);
                newCanvas.MdiParent = this;
                newCanvas.Show();
            }

            EnableDisableTools();
        }

        private void EnableDisableTools()
        {
            extensionsMenuItem.Enabled = windowMenuItem.Enabled = toolBar.Enabled = ActiveMdiChild != null;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is null)
            {
                return;
            }

            (ActiveMdiChild as Canvas).Save();
        }

        private void каскадомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void слеваНаправоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void сверхуВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void упорядочитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void изменитьРазмерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasSizeForm sizeForm = new CanvasSizeForm(this);

            if (sizeForm.ShowDialog() == DialogResult.OK)
            {
                int newWidth = sizeForm.UserWidth;
                int newHeight = sizeForm.UserHeight;

                (ActiveMdiChild as Canvas).ResizeWindow(newWidth, newHeight);
            }
        }

        private void brushWidthBar_ValueChanged(object sender, EventArgs e)
        {
            BrushWidth = (sender as TrackBar).Value;
        }

        private void UnselectTool(ToolStripItem element)
        {
            element.ResetBackColor();
        }

        private void SelectTool(ToolStripItem element)
        {
            if (element.BackColor == _selectionColor)
            {
                if (element == rectangleSelector)
                {
                    if (!RectangleFilled)
                    {
                        RectangleFilled = true;
                        rectangleSelector.Image = Properties.Resources.rectangle_fill;
                        return;
                    }
                    else
                    {
                        RectangleFilled = false;
                        rectangleSelector.Image = Properties.Resources.rectangle;
                    }
                }

                if (element == ellipseSelector)
                {
                    if (!EllipseFilled)
                    {
                        EllipseFilled = true;
                        ellipseSelector.Image = Properties.Resources.ellipse_fill;
                        return;
                    }
                    else
                    {
                        EllipseFilled = false;
                        ellipseSelector.Image = Properties.Resources.ellipse;
                    }
                }

                if (element == starSelector)
                {
                    if (!StarFilled)
                    {
                        StarFilled = true;
                        starSelector.Image = Properties.Resources.star_fill;
                        return;
                    }
                    else
                    {
                        StarFilled = false;
                        starSelector.Image = Properties.Resources.star;
                    }
                }

                UnselectTool(element);
                return;
            }

            foreach (var item in _selectableElements)
            {
                item.ResetBackColor();
            }

            element.BackColor = _selectionColor;
        }

        public bool IsToolSelected(Tools tool)
        {
            ToolStripItem toolItem;

            switch (tool)
            {
                case Tools.Grabber:
                    toolItem = grabSelector;
                    break;
                case Tools.Eraser:
                    toolItem = eraserSelector;
                    break;
                case Tools.LineShape:
                    toolItem = lineSelector;
                    break;
                case Tools.RectangleShape:
                    toolItem = rectangleSelector;
                    break;
                case Tools.EllipseShape:
                    toolItem = ellipseSelector;
                    break;
                case Tools.StarShape:
                    toolItem = starSelector;
                    break;
                default:
                    return false;
            }

            foreach (var item in _selectableElements)
            {
                if (item == toolItem && item.BackColor == _selectionColor)
                    return true;
            }

            return false;
        }

        private void lineSelector_Click(object sender, EventArgs e)
        {
            SelectTool(lineSelector);
        }

        private void rectangleSelector_Click(object sender, EventArgs e)
        {
            SelectTool(rectangleSelector);
        }

        private void ellipseSelector_Click(object sender, EventArgs e)
        {
            SelectTool(ellipseSelector);
        }

        private void starSelector_Click(object sender, EventArgs e)
        {
            SelectTool(starSelector);
        }

        private void развернутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int newWidth = this.Width - 20;
            int newHeight = this.Height - toolBar.Height - menuStrip1.Height - statusStrip1.Height - 45;

            Canvas activeCanvas = ActiveMdiChild as Canvas;

            activeCanvas.ResizeWindow(newWidth, newHeight);
            activeCanvas.Location = new Point(0, 0);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = MdiChildren.Length - 1; i >= 0; i--)
            {
                Canvas canvas = MdiChildren[i] as Canvas;

                DialogResult userResponse = MessageBox.Show($"Сохранить файл {canvas.Text} перед закрытием?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (userResponse == DialogResult.Yes)
                {
                    canvas.Save();
                    canvas.Close();
                }
                else if (userResponse == DialogResult.No)
                {
                    canvas.Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void FindPlugins()
        {
            // папка с плагинами
            string folder = AppDomain.CurrentDomain.BaseDirectory;

            // список названий плагинов для загрузки
            ICollection<string> pluginsToLoad;
            try
            {
                pluginsToLoad = ConfigurationSettings.AppSettings["pluginsToLoad"].Split(',').Select(p => p.Trim()).ToList();
            }
            catch (Exception)
            {
                pluginsToLoad = null;
            }

            // dll-файлы в этой папке
            string[] files = System.IO.Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginSupporter.IPlugin");

                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);

                            // если в конфигурационном файле стоит блокировка списка плагинов, то загрузятся все из сборки;
                            // иначе будет производиться отбор в соответствии со списком
                            if (pluginsToLoad is null || pluginsToLoad.Contains(plugin.Name))
                                _plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка загрузки плагина", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string[,] GetExtendedPluginList()
        {
            string[,] result = new string[_plugins.Count, 3];

            int pluginIndex = 0;
            foreach (var plugin in _plugins.Values)
            {
                result[pluginIndex, 0] = plugin.Name;
                result[pluginIndex, 1] = plugin.Author;

                var versionAttribute = plugin.GetType().GetCustomAttribute(typeof(VersionAttribute));

                if (versionAttribute is VersionAttribute)
                {
                    var a = versionAttribute as VersionAttribute;
                    result[pluginIndex, 2] = $"{a.Major}.{a.Minor}";
                }
                else
                {
                    result[pluginIndex, 2] = "{unknown}";
                }

                pluginIndex++;
            }

            return result;
        }

        private void FillPluginMenu()
        {
            foreach (var plugin in _plugins.Values)
            {
                var newItem = new ToolStripMenuItem(plugin.Name);
                newItem.Click += Plugin_Click;

                extensionsMenuItem.DropDownItems.Add(newItem);
            }
        }

        private void Plugin_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is null)
            {
                return;
            }

            Canvas child = ActiveMdiChild as Canvas;

            IPlugin plugin = _plugins[(sender as ToolStripMenuItem).Text];

            if (string.IsNullOrEmpty(child.FilePath))
            {
                plugin.Transform(child.Bitmap); 
            }
            else
            {
                plugin.Transform(child.Bitmap, child.FilePath);
            }

            child.Refresh();
            child.ResizeWindow(child.Bitmap.Width, child.Bitmap.Height);
        }

        private void списокПлагиновToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[,] plugins = GetExtendedPluginList();

            PluginListForm pluginListForm = new PluginListForm(plugins);
            pluginListForm.ShowDialog();
        }
    }
}
