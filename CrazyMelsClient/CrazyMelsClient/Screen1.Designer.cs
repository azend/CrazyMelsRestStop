namespace CrazyMelsClient
{
    partial class Screen1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Screen1));
            this.exitButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.insertButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.Blue;
            this.exitButton.BackgroundImage = global::CrazyMelsClient.Properties.Resources.Exit;
            this.exitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exitButton.ForeColor = System.Drawing.Color.White;
            this.exitButton.Location = new System.Drawing.Point(502, 599);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(167, 66);
            this.exitButton.TabIndex = 4;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.Blue;
            this.deleteButton.BackgroundImage = global::CrazyMelsClient.Properties.Resources.Delete;
            this.deleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(836, 513);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(167, 66);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.BackColor = System.Drawing.Color.Blue;
            this.updateButton.BackgroundImage = global::CrazyMelsClient.Properties.Resources.Update;
            this.updateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.updateButton.ForeColor = System.Drawing.Color.White;
            this.updateButton.Location = new System.Drawing.Point(610, 513);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(167, 66);
            this.updateButton.TabIndex = 2;
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // insertButton
            // 
            this.insertButton.BackColor = System.Drawing.Color.Blue;
            this.insertButton.BackgroundImage = global::CrazyMelsClient.Properties.Resources.Insert;
            this.insertButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.insertButton.ForeColor = System.Drawing.Color.White;
            this.insertButton.Location = new System.Drawing.Point(384, 513);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(167, 66);
            this.insertButton.TabIndex = 1;
            this.insertButton.UseVisualStyleBackColor = false;
            this.insertButton.Click += new System.EventHandler(this.insertButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.Transparent;
            this.searchButton.BackgroundImage = global::CrazyMelsClient.Properties.Resources.Search;
            this.searchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.searchButton.ForeColor = System.Drawing.SystemColors.Window;
            this.searchButton.Location = new System.Drawing.Point(145, 513);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(167, 66);
            this.searchButton.TabIndex = 0;
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(1033, 344);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 135);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Location = new System.Drawing.Point(1033, 40);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(167, 136);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.SystemColors.Control;
            this.titleLabel.Font = new System.Drawing.Font("Playbill", 46F);
            this.titleLabel.ForeColor = System.Drawing.Color.Red;
            this.titleLabel.Location = new System.Drawing.Point(329, 40);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(600, 62);
            this.titleLabel.TabIndex = 7;
            this.titleLabel.Text = "CRAZY MEL\'S SHOPPING EMPORIUM";
            // 
            // Screen1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.insertButton);
            this.Controls.Add(this.searchButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Screen1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button insertButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label titleLabel;
    }
}

