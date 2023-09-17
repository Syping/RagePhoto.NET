namespace RagePhoto.NET
{
    partial class UserInterface
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ImageBox = new PictureBox();
            Panel = new Panel();
            CloseButton = new Button();
            OpenButton = new Button();
            ((System.ComponentModel.ISupportInitialize)ImageBox).BeginInit();
            Panel.SuspendLayout();
            SuspendLayout();
            // 
            // ImageBox
            // 
            ImageBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ImageBox.Location = new Point(0, 0);
            ImageBox.Name = "ImageBox";
            ImageBox.Size = new Size(814, 388);
            ImageBox.SizeMode = PictureBoxSizeMode.Zoom;
            ImageBox.TabIndex = 0;
            ImageBox.TabStop = false;
            // 
            // Panel
            // 
            Panel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel.Controls.Add(CloseButton);
            Panel.Controls.Add(OpenButton);
            Panel.Location = new Point(12, 394);
            Panel.Name = "Panel";
            Panel.Size = new Size(790, 46);
            Panel.TabIndex = 1;
            // 
            // CloseButton
            // 
            CloseButton.AutoSize = true;
            CloseButton.Dock = DockStyle.Right;
            CloseButton.FlatStyle = FlatStyle.System;
            CloseButton.Location = new Point(398, 0);
            CloseButton.Margin = new Padding(3, 0, 0, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(392, 46);
            CloseButton.TabIndex = 1;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // OpenButton
            // 
            OpenButton.AutoSize = true;
            OpenButton.Dock = DockStyle.Left;
            OpenButton.FlatStyle = FlatStyle.System;
            OpenButton.Location = new Point(0, 0);
            OpenButton.Margin = new Padding(0, 0, 3, 0);
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(392, 46);
            OpenButton.TabIndex = 0;
            OpenButton.Text = "Open";
            OpenButton.UseVisualStyleBackColor = true;
            OpenButton.Click += OpenButton_Click;
            // 
            // UserInterface
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 449);
            Controls.Add(Panel);
            Controls.Add(ImageBox);
            MinimumSize = new Size(840, 520);
            Name = "UserInterface";
            Text = "RagePhoto.NET Photo Viewer";
            Load += UserInterface_Load;
            ((System.ComponentModel.ISupportInitialize)ImageBox).EndInit();
            Panel.ResumeLayout(false);
            Panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox ImageBox;
        private Panel Panel;
        private Button OpenButton;
        private Button CloseButton;
    }
}