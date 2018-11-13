namespace NesScripts.Controls.PathFind
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.ShowMap = new System.Windows.Forms.PictureBox();
            this.DrawMapWhenReady = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ShowMap)).BeginInit();
            this.SuspendLayout();
            // 
            // ShowMap
            // 
            this.ShowMap.Location = new System.Drawing.Point(12, 12);
            this.ShowMap.Name = "ShowMap";
            this.ShowMap.Size = new System.Drawing.Size(400, 400);
            this.ShowMap.TabIndex = 0;
            this.ShowMap.TabStop = false;
            // 
            // DrawMapWhenReady
            // 
            this.DrawMapWhenReady.Enabled = true;
            this.DrawMapWhenReady.Tick += new System.EventHandler(this.DrawMapWhenReady_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 423);
            this.Controls.Add(this.ShowMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Path Find";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ShowMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ShowMap;
        private System.Windows.Forms.Timer DrawMapWhenReady;
    }
}