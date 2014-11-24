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
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Unit Price");
            dt.Columns.Add("Unit Weight");
            dt.Rows.Add("1", "test1", "1", "5", "2");
            dt.Rows.Add("2", "test2", "2", "10", "4");
            dt.Rows.Add("3", "test3", "3", "15", "6");
            dt.Rows.Add("4", "test4", "4", "20", "8");
            dt.Rows.Add("5", "test5", "5", "25", "10");

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("ID");
            dt2.Columns.Add("First Name");
            dt2.Columns.Add("Last Name");
            dt2.Columns.Add("Phone");
            dt2.Rows.Add("1", "Test First", "Test Last", "123-456-7890");

            populateTableFields(dt);
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
            //Assumes there is a DataTable of customer information
            string[] rows = new string[4];
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
        }

        private void populateTableFields(DataTable table)
        {
            //Assumes there is a DataTable of table information
            string[] columnName = new string[5];
            string[,] columnData = new string[5,3];
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
                    if (rowCount >= 3)
                    {
                        rowCount = 0;
                        break;
                    }
                }
                colCount++;
                if (colCount >= 5)
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
            double tax = 0;
            double taxPercent = 0.13;

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
                subtotal_label.Text = "derp";
            }

            tax = subtotal*taxPercent;
            tax_label.Text = tax.ToString();

            total_label.Text = (subtotal + tax).ToString();
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
