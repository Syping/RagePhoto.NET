using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace RagePhoto.NET
{
    public partial class UserInterface : Form
    {
        public UserInterface()
        {
            InitializeComponent();
        }

        private void UserInterface_Load(object sender, EventArgs e)
        {
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Open Photo...",
                Filter = "RagePhoto compatible|PGTA5*;PRDR3*",
                CheckFileExists = true,
                CheckPathExists = true
            };
            openFileDialog.ShowDialog(this);

            if (openFileDialog.FileName == "")
                return;

            RagePhoto ragePhoto = new();
            bool isLoaded = ragePhoto.LoadFile(openFileDialog.FileName);
            if (!isLoaded)
            {
                Int32 error = ragePhoto.GetError();
                if (error <= (Int32)RagePhoto.Error.PhotoReadError)
                {
                    MessageBox.Show("Failed to read photo: " + openFileDialog.FileName, "Open Photo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            Image jpeg = ragePhoto.GetJPEGImage();
            ImageBox.Image = jpeg;

            String title = ragePhoto.Title;
            if (String.IsNullOrEmpty(title))
                Text = "RagePhoto.NET Photo Viewer";
            else
                Text = "RagePhoto.NET Photo Viewer - " + title;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}