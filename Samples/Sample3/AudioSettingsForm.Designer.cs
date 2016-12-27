namespace Sample3
{
    partial class AudioSettingsForm
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
            this.nudPlayback = new System.Windows.Forms.NumericUpDown();
            this.nudRecording = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPlayback = new System.Windows.Forms.TrackBar();
            this.lblPlayback = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRecording = new System.Windows.Forms.TrackBar();
            this.lblRecording = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkNoiseSupp = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecording)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPlayback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRecording)).BeginInit();
            this.SuspendLayout();
            // 
            // nudPlayback
            // 
            this.nudPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPlayback.Location = new System.Drawing.Point(255, 68);
            this.nudPlayback.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudPlayback.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.nudPlayback.Name = "nudPlayback";
            this.nudPlayback.Size = new System.Drawing.Size(48, 20);
            this.nudPlayback.TabIndex = 0;
            this.nudPlayback.ValueChanged += new System.EventHandler(this.nudPlayback_ValueChanged);
            // 
            // nudRecording
            // 
            this.nudRecording.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRecording.Location = new System.Drawing.Point(255, 180);
            this.nudRecording.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudRecording.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.nudRecording.Name = "nudRecording";
            this.nudRecording.Size = new System.Drawing.Size(48, 20);
            this.nudRecording.TabIndex = 1;
            this.nudRecording.ValueChanged += new System.EventHandler(this.nudRecording_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(309, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "dB";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(309, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "dB";
            // 
            // tbPlayback
            // 
            this.tbPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPlayback.LargeChange = 10;
            this.tbPlayback.Location = new System.Drawing.Point(255, 13);
            this.tbPlayback.Maximum = 100;
            this.tbPlayback.Name = "tbPlayback";
            this.tbPlayback.Size = new System.Drawing.Size(301, 45);
            this.tbPlayback.TabIndex = 4;
            this.tbPlayback.TickFrequency = 5;
            this.tbPlayback.Scroll += new System.EventHandler(this.tbPlayback_Scroll);
            // 
            // lblPlayback
            // 
            this.lblPlayback.AutoEllipsis = true;
            this.lblPlayback.Location = new System.Drawing.Point(13, 13);
            this.lblPlayback.Name = "lblPlayback";
            this.lblPlayback.Size = new System.Drawing.Size(236, 13);
            this.lblPlayback.TabIndex = 5;
            this.lblPlayback.Text = "Playback device volume";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Extra playback amplification";
            // 
            // tbRecording
            // 
            this.tbRecording.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRecording.LargeChange = 10;
            this.tbRecording.Location = new System.Drawing.Point(255, 129);
            this.tbRecording.Maximum = 100;
            this.tbRecording.Name = "tbRecording";
            this.tbRecording.Size = new System.Drawing.Size(301, 45);
            this.tbRecording.TabIndex = 7;
            this.tbRecording.TickFrequency = 5;
            this.tbRecording.Scroll += new System.EventHandler(this.tbRecording_Scroll);
            // 
            // lblRecording
            // 
            this.lblRecording.AutoEllipsis = true;
            this.lblRecording.Location = new System.Drawing.Point(13, 129);
            this.lblRecording.Name = "lblRecording";
            this.lblRecording.Size = new System.Drawing.Size(236, 13);
            this.lblRecording.TabIndex = 8;
            this.lblRecording.Text = "Recording device volume";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Extra recording amplification";
            // 
            // chkNoiseSupp
            // 
            this.chkNoiseSupp.AutoSize = true;
            this.chkNoiseSupp.Location = new System.Drawing.Point(255, 225);
            this.chkNoiseSupp.Name = "chkNoiseSupp";
            this.chkNoiseSupp.Size = new System.Drawing.Size(15, 14);
            this.chkNoiseSupp.TabIndex = 10;
            this.chkNoiseSupp.UseVisualStyleBackColor = true;
            this.chkNoiseSupp.CheckedChanged += new System.EventHandler(this.chkNoiseSupp_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Use noise suppression";
            // 
            // AudioSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 259);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkNoiseSupp);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblRecording);
            this.Controls.Add(this.tbRecording);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblPlayback);
            this.Controls.Add(this.tbPlayback);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudRecording);
            this.Controls.Add(this.nudPlayback);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AudioSettingsForm";
            this.Text = "Audio Settings";
            this.Load += new System.EventHandler(this.AudioSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecording)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPlayback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRecording)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudPlayback;
        private System.Windows.Forms.NumericUpDown nudRecording;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbPlayback;
        private System.Windows.Forms.Label lblPlayback;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tbRecording;
        private System.Windows.Forms.Label lblRecording;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkNoiseSupp;
        private System.Windows.Forms.Label label7;
    }
}