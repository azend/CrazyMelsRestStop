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
using CrazyMelsWeb.Models;

namespace CrazyMelsClient
{
    public partial class Screen3 : Form
    {
        private Screen1 screen1 = new Screen1();
        private PrintDocument printDocument1 = new PrintDocument();
        private Bitmap memoryImage;

        public Screen3(List<Customer> cust, List<Product> prod, List<Order> ord, List<Cart> cart)
        {
            InitializeComponent();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("ID");
            dt1.Columns.Add("Product Name");
            dt1.Columns.Add("Quantity");
            dt1.Columns.Add("Unit Price");
            dt1.Columns.Add("Unit Weight");

            string custID = "", cFirst = "", cLast = "", cPhone = "";
            string prodID = "", price = "", weight = "", prodName = "";
            string ordDate = "", poNum = "";
            string cartQuantity = "";

            if (cust.Count != 0)
            {
                custID = cust.ElementAt(0).custID.ToString();
                cFirst = cust.ElementAt(0).firstName.ToString();
                cLast = cust.ElementAt(0).lastName.ToString();
                cPhone = cust.ElementAt(0).phoneNumber.ToString();
            }
            if (ord.Count != 0)
            {
                ordDate = ord.ElementAt(0).orderDate.ToString();
                poNum = ord.ElementAt(0).poNumber.ToString();
            }
            for (int i = 0; i < cust.Count; i++)
            {
                if (prod.Count != 0)
                {
                    prodID = prod.ElementAt(i).prodID.ToString();
                    price = prod.ElementAt(i).price.ToString();
                    weight = prod.ElementAt(i).prodWeight.ToString();
                    prodName = prod.ElementAt(i).prodName.ToString();
                }
                if (cart.Count != 0)
                {
                    cartQuantity = cart.ElementAt(i).quantity.ToString();
                }
                dt1.Rows.Add(prodID, prodName, cartQuantity, price, weight);
            }

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("ID");
            dt2.Columns.Add("First Name");
            dt2.Columns.Add("Last Name");
            dt2.Columns.Add("Phone");
            dt2.Columns.Add("Purchase Date");
            dt2.Columns.Add("PO Number");
            dt2.Rows.Add(custID, cFirst, cLast, cPhone, ordDate, poNum);

            populateTableFields(dt1);
            populateCustomerInformation(dt2);
            populateTotals();
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

        /* ----- FIELD POPULATION ----- */
        private void populateCustomerInformation(DataTable table)
        {
            //Assumes there is a DataTable of customer/PO information
            string[] rows = new string[6]; // 6 = number of fields in the top of PO
            int count = 0;

            foreach (DataColumn column in table.Columns)
            {
                foreach (DataRow row in table.Rows)
                {
                    rows[count] = row[column].ToString();
                    count++;
                }
            }

            custID_label.Text = rows[0];
            firstLastName_label.Text = (rows[2] + ", " + rows[1]);
            phoneNumber_label.Text = rows[3];
            orderDate_label.Text = rows[4];
            poNumber_label.Text = rows[5];
        }

        private void populateTableFields(DataTable table)
        {
            //Assumes there is a DataTable with the table information
            string[] columnName = new string[table.Columns.Count];
            string[,] columnData = new string[table.Columns.Count,table.Rows.Count];
            int rowCount = 0, colCount = 0;

            column1_listbox.Items.Clear();
            column2_listbox.Items.Clear();
            column3_listbox.Items.Clear();
            column4_listbox.Items.Clear();
            column5_listbox.Items.Clear();
            
            foreach (DataColumn column in table.Columns)
            {
                foreach (DataRow row in table.Rows)
                {
                    columnName[colCount] = column.ColumnName;
                    columnData[colCount, rowCount] = row[column].ToString();

                    if (colCount == 0){
                        column1_listbox.Items.Add(columnData[colCount, rowCount]);
                    } else if (colCount == 1){
                        column2_listbox.Items.Add(columnData[colCount, rowCount]);
                    } else if (colCount == 2){
                        column3_listbox.Items.Add(columnData[colCount, rowCount]);
                    } else if (colCount == 3){
                        column4_listbox.Items.Add(columnData[colCount, rowCount]);
                    } else if (colCount == 4){
                        column5_listbox.Items.Add(columnData[colCount, rowCount]);
                    }

                    rowCount++;
                    if (rowCount >= table.Rows.Count)
                    {
                        rowCount = 0;
                        break;
                    }
                }
                colCount++;
                if (colCount >= table.Columns.Count)
                {
                    break;
                }
            }

            column1_label.Text = columnName[0];
            column2_label.Text = columnName[1];
            column3_label.Text = columnName[2];
            column4_label.Text = columnName[3];
            column5_label.Text = columnName[4];
        }

        private void populateTotals()
        {
            int subtotal = 0;
            int subtotalBuffer = 0;
            int weightTotal = 0;
            int weightBuffer = 0;
            double tax = 0;
            double taxPercent = 0.13;

            // Calculate subtotal
            foreach (object item in column4_listbox.Items)  
            {  
                Int32.TryParse(column4_listbox.GetItemText(item), out subtotalBuffer);
                subtotal += subtotalBuffer;
            }
            if (subtotal != 0)
            {
                subtotal_label.Text = subtotal.ToString();
            }
            else
            {
                subtotal = 0;
                subtotal_label.Text = subtotal.ToString();
            }
            tax = subtotal*taxPercent;
            tax_label.Text = tax.ToString();
            total_label.Text = (subtotal + tax).ToString();

            // Calculate total weight
            foreach (object item in column5_listbox.Items)
            {
                Int32.TryParse(column5_listbox.GetItemText(item), out weightBuffer);
                weightTotal += weightBuffer;
            }
            totalOrderWeight_label.Text = weightTotal.ToString();

            // Count of pieces in order
            totalNumOrderPieces_label.Text = column1_listbox.Items.Count.ToString();
        }
        /* ----- END FIELD POPULATION ----- */

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
    }
}
