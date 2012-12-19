using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace GIFBuilder
{
    public partial class GIFBuilderUI : Form
    {
        private ImageList _imageList;
        private string[] _selectedImages;
        public GIFBuilderUI()
        {
            InitializeComponent();
            _imageList = new ImageList();
            _imageList.ImageSize = new Size(64, 64);
            listPreviewImages.LargeImageList = _imageList;
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDlg = new OpenFileDialog())
            {
                openFileDlg.Filter = "Supported Images|*.jpg;*.png;*.jpeg;*.bmp";
                openFileDlg.ShowHelp = false;
                openFileDlg.ShowReadOnly = false;
                openFileDlg.CheckFileExists = true;
                openFileDlg.Multiselect = true;
                openFileDlg.SupportMultiDottedExtensions = true;
                if (openFileDlg.ShowDialog(this) == DialogResult.OK)
                {
                    _selectedImages = openFileDlg.FileNames;
                    FillListView(openFileDlg.FileNames);
                }
            }
        }

        private void FillListView(string[] images)
        {
            listPreviewImages.Items.Clear();
            _imageList.Images.Clear();

            foreach (string image in images)
            {
                string key = Path.GetFileNameWithoutExtension(image);
                _imageList.Images.Add(key, Image.FromFile(image));
                listPreviewImages.Items.Add(key, key, key);
            }
        }

        private void cmdBuild_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "GIF Files|*.gif";
                saveDialog.CheckPathExists = true;
                saveDialog.OverwritePrompt = true;
                saveDialog.AddExtension = true;
                saveDialog.FileName = "generated.gif";
                saveDialog.Title = "Save the Generated GIF file - GIF Builder";
                saveDialog.DefaultExt = "GIF";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveDialog.FileName;
                }
            }

            GenerateGIFImage(fileName);
            MessageBox.Show("Generated GIF file.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GenerateGIFImage(string fileName)
        {
            int delay = Convert.ToInt32(numSeconds.Value) * 100;

            Byte[] buf1;
            Byte[] buf2;
            Byte[] buf3;


            buf2 = new Byte[19];
            buf3 = new Byte[8];
            buf2[0] = 33;  //extension introducer
            buf2[1] = 255; //application extension
            buf2[2] = 11;  //size of block
            buf2[3] = 78;  //N
            buf2[4] = 69;  //E
            buf2[5] = 84;  //T
            buf2[6] = 83;  //S
            buf2[7] = 67;  //C
            buf2[8] = 65;  //A
            buf2[9] = 80;  //P
            buf2[10] = 69; //E
            buf2[11] = 50; //2
            buf2[12] = 46; //.
            buf2[13] = 48; //0
            buf2[14] = 3;  //Size of block
            buf2[15] = 1;  //
            buf2[16] = 0;  //
            buf2[17] = 0;  //
            buf2[18] = 0;  //Block terminator
            buf3[0] = 33;  //Extension introducer
            buf3[1] = 249; //Graphic control extension
            buf3[2] = 4;   //Size of block
            buf3[3] = 9;   //Flags: reserved, disposal method, user input, transparent color
            buf3[4] = Convert.ToByte((delay & 0x00FF) % 256);  //Delay time low byte
            buf3[5] = Convert.ToByte((delay & 0xFF00) / 256);   //Delay time high byte
            buf3[6] = 255; //Transparent color index
            buf3[7] = 0;   //Block terminator

            StreamWriter sw = new StreamWriter(fileName);
            using (BinaryWriter binaryWriter = new BinaryWriter(sw.BaseStream))
            {
                for (int picCount = 0; picCount < _selectedImages.Length; picCount++)
                {
                    using (Image image = Bitmap.FromFile(_selectedImages[picCount]))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            image.Save(memoryStream, ImageFormat.Gif);
                            buf1 = memoryStream.ToArray();
                            if (picCount == 0)
                            {
                                //only write these the first time....
                                binaryWriter.Write(buf1, 0, 781); //Header & global color table
                                binaryWriter.Write(buf2, 0, 19); //Application extension
                            }

                            binaryWriter.Write(buf3, 0, 8); //Graphic extension
                            binaryWriter.Write(buf1, 789, buf1.Length - 790); //Image data
                            if (picCount == _selectedImages.Length - 1)
                            {
                                //only write this one the last time....
                                binaryWriter.Write(";"); //Image terminator
                            }

                            memoryStream.SetLength(0);
                        }
                    }
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
