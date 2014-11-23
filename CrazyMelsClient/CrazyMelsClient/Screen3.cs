using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CrazyMelsClient
{
    public partial class Screen3 : Form
    {
        private Screen1 screen1 = new Screen1();
        private PrintDocument printDocument1 = new PrintDocument();
        private Bitmap memoryImage;

        public Screen3()
        {
            InitializeComponent();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
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
            CaptureScreen();
            printDocument1.Print();
        }
        /* ----- END BUTTON CLICKS ----- */

        /* ----- PRINTING METHODS ----- */
        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void printDocument1_PrintPage(System.Object sender,
               System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        /* ----- FIELD POPULATION ----- */
        private async void populateCustomerInformation()
        {
            Customer table = new Customer();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1973/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;

                response = await client.PostAsJsonAsync("api/Customer/", table);
            }
        }

        private void populateTableFields()
        {

        }

        private void populateTotals()
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
