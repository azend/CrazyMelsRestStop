using CrazyMelsWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrazyMelsWeb.Models;

namespace CrazyMelsClient
{
    public partial class Screen4 : Form
    {
        private Screen1 screen1 = new Screen1();
        private int tablesToShow = 0;
        private int locationX = 373;
        private int[] locationY;

        /* ------ Constructor -------- */
        public Screen4()
        {
            InitializeComponent();

            locationY[0] = 94;
            locationY[0] = 269;
            locationY[0] = 444;
            locationY[0] = 619;
        }
        /* ----------- End Constructor -------------- */

        private void Screen4_Load(object sender, EventArgs e)
        {

        }

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

        public void customerList(List<Customer> customer)
        {
            string[] names = { "Customer ID", "First Name", "Last Name", "Phone Number" };

            if (customer.Count == 0)
            {
                return;
            }

            generateTable("customerGridView", 4, names);
        }
        public void productList(List<Product> product)
        {
            string[] names = { "Product ID", "Product Name", "Price", "Product Weight", "In Stock?" };

            if (product.Count == 0)
            {
                return;
            }

            generateTable("productGridView", 5, names);
        }
        public void orderList(List<Order> order)
        {
            string[] names = { "Order ID", "Customer ID", "PO Number", "Order Date" };

            if (order.Count == 0)
            {
                return;
            }

            generateTable("orderGridView", 4, names);
        }
        public void cartList(List<Cart> cart)
        {
            string[] names = { "Order ID", "Product ID", "Quantity" };

            if (cart.Count == 0)
            {
                return;
            }
            
            generateTable("cartGridView", 3, names);
        }

        private void generateTable(string name, int columnNumber, string[] columnNames)
        {
            DataGridView DataGridView1 = new DataGridView();
            this.Controls.Add(dataGridView1);

            dataGridView1.Name = name;
            dataGridView1.Location = new Point(locationX, locationY[tablesToShow]);
            dataGridView1.Size = new Size(589, 169);

            dataGridView1.ColumnCount = columnNumber;
            dataGridView1.ColumnHeadersVisible = true;

            for (int x = 0; x < columnNumber; x++)
            {
                dataGridView1.Columns[x].Name = columnNames[x];
            }
            tablesToShow++;
        }

        public void positionButtons()
        {
            back_button.Location = new Point(locationX,(locationY[--tablesToShow] + 175));
            exit_button.Location = new Point(781, (locationY[--tablesToShow] + 175));
        }
    }
}
