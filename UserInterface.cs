using Syping.RagePhoto;
namespace RagePhoto.NET;

public partial class UserInterface : Form {

    public UserInterface() {
        InitializeComponent();
    }

    private void OpenButton_Click(object sender, EventArgs e) {
        OpenFileDialog openFileDialog = new() {
            Title = "Open Photo...",
            Filter = "RagePhoto compatible|PGTA5*;PRDR3*",
            CheckFileExists = true,
            CheckPathExists = true
        };

        if (openFileDialog.ShowDialog(this) != DialogResult.OK)
            return;

        using Photo ragePhoto = new();
        ragePhoto.LoadFile(openFileDialog.FileName);

        using MemoryStream jpegStream = new(ragePhoto.Jpeg);
        Image jpeg = Image.FromStream(jpegStream);
        ImageBox.Image = jpeg;

        string title = ragePhoto.Title;
        Text = string.IsNullOrEmpty(title) ?
           "RagePhoto.NET Photo Viewer" : $"RagePhoto.NET Photo Viewer - {title}";
    }

    private void CloseButton_Click(object sender, EventArgs e) {
        Close();
    }
}
