﻿namespace MovieRental
{
    partial class Like
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelinlike = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelinlike
            // 
            this.panelinlike.AutoScroll = true;
            this.panelinlike.BackColor = System.Drawing.SystemColors.Control;
            this.panelinlike.Location = new System.Drawing.Point(0, 0);
            this.panelinlike.Name = "panelinlike";
            this.panelinlike.Size = new System.Drawing.Size(1397, 584);
            this.panelinlike.TabIndex = 0;
            // 
            // Like
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelinlike);
            this.Name = "Like";
            this.Size = new System.Drawing.Size(1410, 641);
            this.Load += new System.EventHandler(this.Like_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelinlike;
    }
}
