using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrazyMelsClient
{
    public partial class Screen3 : Form
    {
        private Screen1 screen1 = new Screen1();

        public Screen3()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        /* ----- BUTTON CLICKS ----- */
        private void back_button_Click(object sender, EventArgs e)
        {
            screen1 = this.Owner as Screen1;
            screen1.ShowButtons();
            this.Close();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void print_button_Click(object sender, EventArgs e)
        {

        }
        /* ----- END BUTTON CLICKS ----- */

        /* ----- FIELD POPULATION ----- */
        void populateCustomerInformation()
        {

        }

        void populateTableFields()
        {

        }

        void populateTotals()
        {
            int subtotal = 0;
            double tax = 0;
            double taxPercent = 0.13;
            Int32.TryParse(price_label.Text, out subtotal);
            if (subtotal != 0)
            {
                subtotal_label.Text = subtotal.ToString();
            }
            else
            {
                subtotal_label.Text = "derp";
            }

            tax = subtotal*taxPercent;
            tax_label.Text = tax.ToString();

            total_label.Text = (subtotal + tax).ToString();
        }
        /* ----- END FIELD POPULATION ----- */

    }
}
