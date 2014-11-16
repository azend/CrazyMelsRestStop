using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CrazyMelsClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Screen1 form = new Screen1();
            form.TopLevel = false;
            panel1.Controls.Add(form);
            form.Show();
        }
    }
}
