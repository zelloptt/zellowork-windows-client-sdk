namespace Sample6
{
    partial class AudioDevices
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPlaybackDevices = new System.Windows.Forms.ComboBox();
            this.buttonClearPlaybackDevice = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRecordingDevices = new System.Windows.Forms.ComboBox();
            this.buttonClearRecordingDevice = new System.Windows.Forms.Button();
            this.buttonChange = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 20);
            this.label1.Text = "Playback devices";
            // 
            // cbPlaybackDevices
            // 
            this.cbPlaybackDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPlaybackDevices.Location = new System.Drawing.Point(4, 28);
            this.cbPlaybackDevices.Name = "cbPlaybackDevices";
            this.cbPlaybackDevices.Size = new System.Drawing.Size(204, 22);
            this.cbPlaybackDevices.TabIndex = 1;
            // 
            // buttonClearPlaybackDevice
            // 
            this.buttonClearPlaybackDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearPlaybackDevice.Location = new System.Drawing.Point(214, 29);
            this.buttonClearPlaybackDevice.Name = "buttonClearPlaybackDevice";
            this.buttonClearPlaybackDevice.Size = new System.Drawing.Size(23, 20);
            this.buttonClearPlaybackDevice.TabIndex = 2;
            this.buttonClearPlaybackDevice.Text = "X";
            this.buttonClearPlaybackDevice.Click += new System.EventHandler(this.buttonClearPlaybackDevice_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 20);
            this.label2.Text = "Recording devices";
            // 
            // cbRecordingDevices
            // 
            this.cbRecordingDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRecordingDevices.Location = new System.Drawing.Point(4, 109);
            this.cbRecordingDevices.Name = "cbRecordingDevices";
            this.cbRecordingDevices.Size = new System.Drawing.Size(204, 22);
            this.cbRecordingDevices.TabIndex = 4;
            // 
            // buttonClearRecordingDevice
            // 
            this.buttonClearRecordingDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearRecordingDevice.Location = new System.Drawing.Point(214, 109);
            this.buttonClearRecordingDevice.Name = "buttonClearRecordingDevice";
            this.buttonClearRecordingDevice.Size = new System.Drawing.Size(23, 20);
            this.buttonClearRecordingDevice.TabIndex = 5;
            this.buttonClearRecordingDevice.Text = "X";
            this.buttonClearRecordingDevice.Click += new System.EventHandler(this.buttonClearRecordingDevice_Click);
            // 
            // buttonChange
            // 
            this.buttonChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChange.Location = new System.Drawing.Point(162, 247);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(74, 20);
            this.buttonChange.TabIndex = 6;
            this.buttonChange.Text = "change";
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // AudioDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.buttonClearRecordingDevice);
            this.Controls.Add(this.cbRecordingDevices);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonClearPlaybackDevice);
            this.Controls.Add(this.cbPlaybackDevices);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "AudioDevices";
            this.Text = "Audio devices";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPlaybackDevices;
        private System.Windows.Forms.Button buttonClearPlaybackDevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbRecordingDevices;
        private System.Windows.Forms.Button buttonClearRecordingDevice;
        private System.Windows.Forms.Button buttonChange;
    }
}