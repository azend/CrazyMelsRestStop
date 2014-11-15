using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CrazyMelsClient
{
    public partial class Screen1 : Form
    {
        Screen2 screen2;
        public Panel panel = new Panel();

        public Screen1()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            searchButton.Location = new Point((int)(this.Width * 0.2) - searchButton.Width / 2, (int)(this.Height * 0.7));
            insertButton.Location = new Point((int)(this.Width * 0.4) - insertButton.Width / 2, (int)(this.Height * 0.7));
            updateButton.Location = new Point((int)(this.Width * 0.6) - updateButton.Width / 2, (int)(this.Height * 0.7));
            deleteButton.Location = new Point((int)(this.Width * 0.8) - deleteButton.Width / 2, (int)(this.Height * 0.7));
            exitButton.Location = new Point((int)(this.Width * 0.5) - exitButton.Width / 2, (int)(this.Height * 0.8));
            this.Controls.Add(panel);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            createScreen2(1);
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            createScreen2(2);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            createScreen2(3);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            createScreen2(4);
        }

        private void createScreen2(int action)
        {
            screen2 = new Screen2(action);
            screen2.Owner = this;
            screen2.TopLevel = false;
            panel.Dock = DockStyle.Fill;
            panel.Controls.Add(screen2);
            screen2.Show();
            HideButtons();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void ShowButtons()
        {
            searchButton.Show();
            insertButton.Show();
            updateButton.Show();
            deleteButton.Show();
            exitButton.Show();
            titleLabel.Show();
            pictureBox1.Show();
            pictureBox2.Show();
        }

        public void HideButtons()
        {
            searchButton.Hide();
            insertButton.Hide();
            updateButton.Hide();
            deleteButton.Hide();
            exitButton.Hide();
            titleLabel.Hide();
            pictureBox1.Hide();
            pictureBox2.Hide();
        }
    }
}
