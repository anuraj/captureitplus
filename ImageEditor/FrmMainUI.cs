using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class FrmMainUI : Form
    {
        private string _fileName;
        private readonly List<CustomShape> _shapes;
        private GraphicsPath _graphicsPath;
        private Shape _selectedShape = Shape.Unknown;
        private Color _selectedColor = Color.Red;
        private PenSize _selectedSize = PenSize.Fine;
        private Pen _selectedPen;
        private readonly Pen _highlighterPen = new Pen(Color.FromArgb(92, Color.LightYellow), (int)PenSize.Thick);
        public FrmMainUI()
        {
            InitializeComponent();
            _shapes = new List<CustomShape>();
            UpdatePen();
        }

        public FrmMainUI(string fileName)
            : this()
        {
            _fileName = fileName;
            LoadImage(_fileName);
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PNG File|*.png|JPEG File|*.jpeg|BMP File|*.bmp|TIFF File|*.tiff|GIF File|*.gif|WMF File|*.wmf"; ;
                openFileDialog.CheckFileExists = true;
                openFileDialog.Multiselect = false;
                openFileDialog.ReadOnlyChecked = false;
                openFileDialog.Title = "Open Image File - Screenshot Editor";
                openFileDialog.ShowReadOnly = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadImage(openFileDialog.FileName);
                }
            }
        }

        private void LoadImage(string fileName)
        {
            picPreview.Image = Image.FromFile(fileName);
            _fileName = fileName;
        }

        private void picPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (_selectedShape == Shape.Eraser)
            {

            }
            else if (_selectedShape == Shape.Pen || _selectedShape == Shape.Highlighter)
            {
                _graphicsPath = new GraphicsPath();
            }
        }

        private void picPreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (_graphicsPath != null)
            {
                _graphicsPath.AddLine(e.X, e.Y, e.X, e.Y);
            }
        }

        private void picPreview_MouseUp(object sender, MouseEventArgs e)
        {
            if (_graphicsPath != null)
            {
                var customShape = new CustomShape();
                customShape.Path = _graphicsPath;
                customShape.Pen = _selectedShape == Shape.Highlighter ? _highlighterPen : _selectedPen;
                customShape.Shape = _selectedShape;
                _shapes.Add(customShape);
                _graphicsPath = null;
                picPreview.Invalidate();
            }

            _selectedShape = Shape.Unknown;
        }

        private void picPreview_Paint(object sender, PaintEventArgs e)
        {
            if (_shapes.Count >= 1)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                foreach (var customShape in _shapes)
                {
                    e.Graphics.DrawPath(customShape.Pen, customShape.Path);
                }
            }
        }

        private void penToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedShape = Shape.Pen;
        }

        private void highlighterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedShape = Shape.Highlighter;
        }

        private void eraserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedShape = Shape.Eraser;
        }

        private void thinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedSize = PenSize.Fine;
            UpdatePen();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedSize = PenSize.Medium;
            UpdatePen();
        }

        private void thickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedSize = PenSize.Thick;
            UpdatePen();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedColor = Color.Red;
            UpdatePen();
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedColor = Color.Blue;
            UpdatePen();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedColor = Color.Green;
            UpdatePen();
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.AnyColor = true;
                colorDialog.AllowFullOpen = true;
                colorDialog.ShowHelp = false;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedColor = colorDialog.Color;
                    UpdatePen();
                }
            }
        }

        private void UpdatePen()
        {
            _selectedPen = new Pen(_selectedColor, (float)_selectedSize);
        }
    }
}
