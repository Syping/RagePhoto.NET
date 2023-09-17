using System.Diagnostics;
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

            RagePhoto ragePhoto = new();
            bool isLoaded = ragePhoto.LoadFile(openFileDialog.FileName);
            if (!isLoaded)
                return;

            Image? jpeg = ragePhoto.GetJPEG();
            if (jpeg == null)
                return;
            ImageBox.Image = jpeg;

            String title = ragePhoto.GetTitle();
            if (title == "")
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