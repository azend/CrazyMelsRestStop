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
        /* ----- ATTRIBUTES ----- */
        private Screen1 screen1 = new Screen1();
        private int screen1Option = 0;
        private enum CRUD { INSERT, SEARCH, UPDATE, DELETE };

        // Constructor
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

        // PO Checkbox - Enables/Disables appropriate fields
        private void productOrder_checkbox_CheckedChanged_1(object sender, EventArgs e)
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

        /* ----- EDITING CUSTOMER FIELDS ----- */
        //The Customer and Product tables are independent of each other
        //The UI is not to allow data from the Customer and the Product areas to be input for searching, inserting, updating and deleting
        //Behind the scenes (on the web-service side) it very well may be the case that joins of customer and product are required, but they are not to be allowed through the UI!!
        //Do not allow the user to do this – tell them they are in error.
        private void customerCustID_textbox_TextChanged(object sender, EventArgs e)
        {
            if (customerCustID_textbox.Text != "")
            {
                toggleProductFields(true);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(true);
                    toggleCartFields(true);
                }
            }
            else
            {
                toggleProductFields(false);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(false);
                    toggleCartFields(false);
                }
            }
        }

        private void firstName_textbox_TextChanged(object sender, EventArgs e)
        {
            if (firstName_textbox.Text != "")
            {
                toggleProductFields(true);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(true);
                    toggleCartFields(true);
                }
            }
            else
            {
                toggleProductFields(false);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(false);
                    toggleCartFields(false);
                }
            }
        }

        private void lastName_textbox_TextChanged(object sender, EventArgs e)
        {
            if (lastName_textbox.Text != "")
            {
                toggleProductFields(true);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(true);
                    toggleCartFields(true);
                }
            }
            else
            {
                toggleProductFields(false);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(false);
                    toggleCartFields(false);
                }
            }
        }

        private void phoneNumber_textbox_TextChanged(object sender, EventArgs e)
        {
            if (phoneNumber_textbox.Text != "")
            {
                toggleProductFields(true);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(true);
                    toggleCartFields(true);
                }
            }
            else
            {
                toggleProductFields(false);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(false);
                    toggleCartFields(false);
                }
            }
        }

        /* ----- EDITING PRODUCT FIELDS ----- */
        private void productProdID_textbox_TextChanged(object sender, EventArgs e)
        {
            if (productProdID_textbox.Text != "")
            {
                toggleCustomerFields(true);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(true);
                    toggleCartFields(true);
                }
            }
            else
            {
                toggleCustomerFields(false);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(false);
                    toggleCartFields(false);
                }
            }
        }

        private void prodName_textbox_TextChanged(object sender, EventArgs e)
        {
            if (prodName_textbox.Text != "")
            {
                toggleCustomerFields(true);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(true);
                    toggleCartFields(true);
                }
            }
            else
            {
                toggleCustomerFields(false);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(false);
                    toggleCartFields(false);
                }
            }
        }

        private void price_textbox_TextChanged(object sender, EventArgs e)
        {
            if (price_textbox.Text != "")
            {
                toggleCustomerFields(true);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(true);
                    toggleCartFields(true);
                }
            }
            else
            {
                toggleCustomerFields(false);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(false);
                    toggleCartFields(false);
                }
            }
        }

        private void prodWeight_textbox_TextChanged(object sender, EventArgs e)
        {
            if (prodWeight_textbox.Text != "")
            {
                toggleCustomerFields(true);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(true);
                    toggleCartFields(true);
                }
            }
            else
            {
                toggleCustomerFields(false);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(false);
                    toggleCartFields(false);
                }
            }
        }

        private void soldOut_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (soldOut_checkbox.Checked == true)
            {
                toggleCustomerFields(true);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(true);
                    toggleCartFields(true);
                }
            }
            else
            {
                toggleCustomerFields(false);
                if (screen1Option != (int)CRUD.SEARCH)
                {
                    toggleOrderFields(false);
                    toggleCartFields(false);
                }
            }
        }

        /* ----- EDITING ORDER FIELDS ----- */
        private void orderOrderID_textbox_TextChanged(object sender, EventArgs e)
        {
            if (orderOrderID_textbox.Text != "" && screen1Option != (int)CRUD.SEARCH)
            {
                toggleCustomerFields(true);
                toggleProductFields(true);
                toggleCartFields(true);
            }
            else
            {
                toggleCustomerFields(false);
                toggleProductFields(false);
                toggleCartFields(false);
            }
        }

        private void custID_textbox_TextChanged(object sender, EventArgs e)
        {
            if (custID_textbox.Text != "" && screen1Option != (int)CRUD.SEARCH)
            {
                toggleCustomerFields(true);
                toggleProductFields(true);
                toggleCartFields(true);
            }
            else
            {
                toggleCustomerFields(false);
                toggleProductFields(false);
                toggleCartFields(false);
            }
        }

        private void poNumber_textbox_TextChanged(object sender, EventArgs e)
        {
            if (poNumber_textbox.Text != "" && screen1Option != (int)CRUD.SEARCH)
            {
                toggleCustomerFields(true);
                toggleProductFields(true);
                toggleCartFields(true);
            }
            else
            {
                toggleCustomerFields(false);
                toggleProductFields(false);
                toggleCartFields(false);
            }
        }

        private void orderDate_textbox_TextChanged(object sender, EventArgs e)
        {
            if (orderDate_textbox.Text != "" && screen1Option != (int)CRUD.SEARCH)
            {
                toggleCustomerFields(true);
                toggleProductFields(true);
                toggleCartFields(true);
            }
            else
            {
                toggleCustomerFields(false);
                toggleProductFields(false);
                toggleCartFields(false);
            }
        }

        /* ----- EDITING CART FIELDS ----- */
        private void cartOrderID_textbox_TextChanged(object sender, EventArgs e)
        {
            if (cartOrderID_textbox.Text != "" && screen1Option != (int)CRUD.SEARCH)
            {
                toggleCustomerFields(true);
                toggleProductFields(true);
                toggleOrderFields(true);
            }
            else
            {
                toggleCustomerFields(false);
                toggleProductFields(false);
                toggleOrderFields(false);
            }
        }

        private void cartProdID_textbox_TextChanged(object sender, EventArgs e)
        {
            if (cartProdID_textbox.Text != "" && screen1Option != (int)CRUD.SEARCH)
            {
                toggleCustomerFields(true);
                toggleProductFields(true);
                toggleOrderFields(true);
            }
            else
            {
                toggleCustomerFields(false);
                toggleProductFields(false);
                toggleOrderFields(false);
            }
        }

        private void quantity_textbox_TextChanged(object sender, EventArgs e)
        {
            if (quantity_textbox.Text != "" && screen1Option != (int)CRUD.SEARCH)
            {
                toggleCustomerFields(true);
                toggleProductFields(true);
                toggleOrderFields(true);
            }
            else
            {
                toggleCustomerFields(false);
                toggleProductFields(false);
                toggleOrderFields(false);
            }
        }

        // toggleCustomerFields method header (lazy version)
        private void toggleCustomerFields(bool disable)
        {
            if (disable)
            {
                customerCustID_textbox.ReadOnly = true;
                firstName_textbox.ReadOnly = true;
                lastName_textbox.ReadOnly = true;
                phoneNumber_textbox.ReadOnly = true;
            }
            else
            {
                customerCustID_textbox.ReadOnly = false;
                firstName_textbox.ReadOnly = false;
                lastName_textbox.ReadOnly = false;
                phoneNumber_textbox.ReadOnly = false;
            }
        }

        // toggleProductFields method header (lazy version)
        private void toggleProductFields(bool disable)
        {
            if (disable)
            {
                productProdID_textbox.ReadOnly = true;
                prodName_textbox.ReadOnly = true;
                price_textbox.ReadOnly = true;
                prodWeight_textbox.ReadOnly = true;
                soldOut_checkbox.Enabled = false;
            }
            else
            {
                productProdID_textbox.ReadOnly = false;
                prodName_textbox.ReadOnly = false;
                price_textbox.ReadOnly = false;
                prodWeight_textbox.ReadOnly = false;
                soldOut_checkbox.Enabled = true;
            }
        }

        // toggleOrderFields method header (lazy version)
        private void toggleOrderFields(bool disable)
        {
            if (disable)
            {
                orderOrderID_textbox.ReadOnly = true;
                custID_textbox.ReadOnly = true;
                poNumber_textbox.ReadOnly = true;
                orderDate_textbox.ReadOnly = true;
            }
            else
            {
                orderOrderID_textbox.ReadOnly = false;
                custID_textbox.ReadOnly = false;
                poNumber_textbox.ReadOnly = false;
                orderDate_textbox.ReadOnly = false;
            }
        }

        // toggleCartFields method header (lazy version)
        private void toggleCartFields(bool disable)
        {
            if (disable)
            {
                cartOrderID_textbox.ReadOnly = true;
                cartProdID_textbox.ReadOnly = true;
                quantity_textbox.ReadOnly = true;
            }
            else
            {
                cartOrderID_textbox.ReadOnly = false;
                cartProdID_textbox.ReadOnly = false;
                quantity_textbox.ReadOnly = false;
            }
        }


        /* ----- BUTTON CLICKS ----- */

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
