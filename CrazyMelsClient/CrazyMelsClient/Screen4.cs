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
    public partial class Screen4 : Form
    {
        private Screen1 screen1 = new Screen1();
        private enum table { CUSTOMER, PRODUCT, ORDER, CART };
        private int screen2option = 0;

        /* ------ Constructor -------- */
        public Screen4(int option)
        {
            InitializeComponent();

            screen2option = option;

            if (option == (int)table.CUSTOMER)
            {
 
            }
            else if (option == (int)table.PRODUCT)
            {

            }
            else if (option == (int)table.ORDER)
            {

            }
            else
            {
                dataGridView1.ColumnCount = 3;
                dataGridView1.ColumnHeadersVisible = true;

                dataGridView1.Columns[0].Name = "Order ID";
                dataGridView1.Columns[1].Name = "Product ID";
                dataGridView1.Columns[2].Name = "Quantity";
            }
        }
        /* ----------- End Constructor -------------- */

        /* ----------- Button Clicks ---------------- */
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
        /* ----------- End Button Clicks ------------ */

        private void customerList(List<Customer> customer)
        {
            if (customer.Count == 0)
            {
                return;
            }

            dataGridView1.ColumnCount = 4;
            dataGridView1.ColumnHeadersVisible = true;

            dataGridView1.Columns[0].Name = "Customer ID";
            dataGridView1.Columns[1].Name = "First Name";
            dataGridView1.Columns[2].Name = "Last Name";
            dataGridView1.Columns[3].Name = "Phone Number";
        }
        private void customerList(List<Product> product)
        {
            if (product.Count == 0)
            {
                return;
            }
            dataGridView2.ColumnCount = 5;
            dataGridView2.ColumnHeadersVisible = true;

            dataGridView2.Columns[0].Name = "Product ID";
            dataGridView2.Columns[1].Name = "Product Name";
            dataGridView2.Columns[2].Name = "Price";
            dataGridView2.Columns[3].Name = "Product Weight";
            dataGridView2.Columns[4].Name = "In Stock?";
        }
        private void customerList(List<Order> order)
        {
            if (order.Count == 0)
            {
                return;
            }

            dataGridView3.ColumnCount = 4;
            dataGridView3.ColumnHeadersVisible = true;

            dataGridView3.Columns[0].Name = "Order ID";
            dataGridView3.Columns[1].Name = "Customer ID";
            dataGridView3.Columns[2].Name = "PO Number";
            dataGridView3.Columns[3].Name = "Order Date";
        }
        private void customerList(List<Cart> customer)
        {
            if (customer.Count == 0)
            {
                return;
            }
        }
    }
}
