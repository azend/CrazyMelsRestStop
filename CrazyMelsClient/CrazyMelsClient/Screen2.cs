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
    public partial class Screen2 : Form
    {
        private Screen1 screen1 = new Screen1();
        private int screen1Option = 0;
        private enum CRUD { INSERT, SEARCH, UPDATE, DELETE };

        public Screen2(int crud)
        {
            InitializeComponent();
            screen1Option = crud;
            //Product Order generation checkbox should only be visible if the user selects the search option
            if (crud != (int)CRUD.SEARCH)
            {
                productOrder_checkbox.Visible = false;
                productOrder_label.Visible = false;
                productOrder_panel.Visible = false;
            }
        }
        
        private void productOrder_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            //When asking for a P.O to be generated, only the following fields can be used (in any combination) to build the query
            //orderID, custID, lastName, firstName, poNumber and/or orderDate
            if (productOrder_checkbox.Checked == true)
            {
                phoneNumber_textbox.ReadOnly = true;
                productProdID_textbox.ReadOnly = true;
                prodName_textbox.ReadOnly = true;
                price_textbox.ReadOnly = true;
                prodWeight_textbox.ReadOnly = true;
                orderDate_textbox.ReadOnly = true;
                cartOrderID_textbox.ReadOnly = true;
                cartProdID_textbox.ReadOnly = true;
                quantity_textbox.ReadOnly = true;
                soldOut_checkbox.Enabled = false;
            }
            else if (productOrder_checkbox.Checked == false)
            {
                phoneNumber_textbox.ReadOnly = false;
                productProdID_textbox.ReadOnly = false;
                prodName_textbox.ReadOnly = false;
                price_textbox.ReadOnly = false;
                prodWeight_textbox.ReadOnly = false;
                orderDate_textbox.ReadOnly = false;
                cartOrderID_textbox.ReadOnly = false;
                cartProdID_textbox.ReadOnly = false;
                quantity_textbox.ReadOnly = false;
                soldOut_checkbox.Enabled = true;
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            screen1 = this.Owner as Screen1;
            screen1.ShowButtons();
            this.Close();
        }

        private void execute_button_Click(object sender, EventArgs e)
        {

        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
