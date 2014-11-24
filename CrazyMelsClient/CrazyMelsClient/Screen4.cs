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

namespace CrazyMelsClient
{
    public partial class Screen4 : Form
    {
        private Screen1 screen1 = new Screen1();
        private int tablesToShow = 0;
        private int locationX = 373;
        private int[] locationY = new int[4];

        /* ------ Constructor -------- */
        public Screen4()
        {
            InitializeComponent();

            this.Location = new Point(120, 0);
            label1.Visible = false;
            
            locationY[0] = 94;
            locationY[1] = 269;
            locationY[2] = 444;
            locationY[3] = 619;
        }
        /* ----------- End Constructor -------------- */

        private void Screen4_Load(object sender, EventArgs e)
        {

        }

        /* ----------- Button Clicks ---------------- */
        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /* ----------- End Button Clicks ------------ */

        public void customerList(List<Customer> customer)
        {
            DataGridView customerGridView = new DataGridView();
            int x = 0;

            string[] names = { "Customer ID", "First Name", "Last Name", "Phone Number" };

            if (customer.Count == 0)
            {
                return;
            }

            generateTable("customerGridView", 4, names, customerGridView);

            foreach (Customer cust in customer)
            {
                customerGridView.Rows.Add(1);
                customerGridView.Rows[x].Cells[0].Value = cust.custID.ToString();
                customerGridView.Rows[x].Cells[1].Value = cust.firstName.ToString();
                customerGridView.Rows[x].Cells[2].Value = cust.lastName.ToString();
                customerGridView.Rows[x].Cells[3].Value = cust.phoneNumber.ToString();
                x++;
            }
        }
        public void productList(List<Product> product)
        {
            DataGridView productGridView = new DataGridView();
            int x = 0;

            string[] names = { "Product ID", "Product Name", "Price", "Product Weight", "In Stock?" };

            if (product.Count == 0)
            {
                return;
            }

            generateTable("productGridView", 5, names, productGridView);

            foreach (Product prod in product)
            {
                productGridView.Rows.Add(1);
                productGridView.Rows[x].Cells[0].Value = prod.prodID.ToString();
                productGridView.Rows[x].Cells[1].Value = prod.prodName.ToString();
                productGridView.Rows[x].Cells[2].Value = prod.price.ToString();
                productGridView.Rows[x].Cells[3].Value = prod.prodWeight.ToString();
                productGridView.Rows[x].Cells[4].Value = prod.inStock.ToString();
                x++;
            }
        }
        public void orderList(List<Order> order)
        {
            DataGridView orderGridView = new DataGridView();
            int x = 0;

            string[] names = { "Order ID", "Customer ID", "PO Number", "Order Date" };

            if (order.Count == 0)
            {
                return;
            }

            generateTable("orderGridView", 4, names, orderGridView);

            foreach (Order ord in order)
            {
                orderGridView.Rows.Add(1);
                orderGridView.Rows[x].Cells[0].Value = ord.orderID.ToString();
                orderGridView.Rows[x].Cells[1].Value = ord.custID.ToString();
                orderGridView.Rows[x].Cells[2].Value = ord.poNumber.ToString();
                orderGridView.Rows[x].Cells[3].Value = ord.orderDate.ToString();
                x++;
            }
        }
        public void cartList(List<Cart> cart)
        {
            DataGridView cartGridView = new DataGridView();
            int x = 0;

            string[] names = { "Order ID", "Product ID", "Quantity" };

            if (cart.Count == 0)
            {
                return;
            }

            generateTable("cartGridView", 3, names, cartGridView);

            foreach (Cart car in cart)
            {
                cartGridView.Rows.Add(1);
                cartGridView.Rows[x].Cells[0].Value = car.orderID.ToString();
                cartGridView.Rows[x].Cells[1].Value = car.prodID.ToString();
                cartGridView.Rows[x].Cells[2].Value = car.quantity.ToString();
                x++;
            }
        }

        private void generateTable(string name, int columnNumber, string[] columnNames, DataGridView dataGridView1)
        {
           // DataGridView dataGridView1 = new DataGridView();
            this.Controls.Add(dataGridView1);

            dataGridView1.Name = name;
            dataGridView1.Location = new Point(locationX, locationY[tablesToShow]);
            dataGridView1.Size = new Size(589, 169);
            dataGridView1.EditingControl.Enabled = false;

            dataGridView1.ColumnCount = columnNumber;
            dataGridView1.ColumnHeadersVisible = true;

            for (int x = 0; x < columnNumber; x++)
            {
                dataGridView1.Columns[x].Name = columnNames[x];
            }
            tablesToShow++;
        }

        public void generateMessage(string message)
        {
            label1.Text = message;
            label1.Visible = true;
        }

        public void positionButtons()
        {
            if (tablesToShow == 0)
            {
                back_button.Location = new Point(locationX, (locationY[tablesToShow] + 175));
            }
            else
            {
                back_button.Location = new Point(locationX, (locationY[--tablesToShow] + 175));
            }
            exit_button.Location = new Point(781, (locationY[tablesToShow] + 175));

            this.Size = new Size(this.Size.Width, exit_button.Location.Y + 100);
        }
    }
}
