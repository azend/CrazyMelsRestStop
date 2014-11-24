using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CrazyMelsWeb.Models;
using System.Net.Http.Formatting;

namespace CrazyMelsClient
{
    public partial class Screen2 : Form
    {
        /* ----- ATTRIBUTES ----- */
        private Screen1 screen1 = new Screen1();
        private Screen3 screen3 = new Screen3();
        private int screen1Option = 0;
        private enum CRUD { SEARCH, INSERT, UPDATE, DELETE };
        private Regex rgx;
        private string tempPhoneNumber;
        private string tempProdID;
        private string tempProdName;
        private string tempPrice;
        private string tempProdWeight;
        private string tempCartOrderID;
        private string tempCartProdID;
        private string tempQuantity;



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
            if (crud == (int)CRUD.INSERT)
            {
                customerCustID_textbox.Enabled = false;
                productProdID_textbox.Enabled = false;
                orderOrderID_textbox.Enabled = false;
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
                cartOrderID_textbox.ReadOnly = true;
                cartProdID_textbox.ReadOnly = true;
                quantity_textbox.ReadOnly = true;
                tempPhoneNumber = phoneNumber_textbox.Text;
                tempProdID = productProdID_textbox.Text;
                tempProdName = prodName_textbox.Text;
                tempPrice = price_textbox.Text;
                tempProdWeight = prodWeight_textbox.Text;
                tempCartOrderID = cartOrderID_textbox.Text;
                tempCartProdID = cartProdID_textbox.Text;
                tempQuantity = quantity_textbox.Text;
                phoneNumber_textbox.Text = "";
                productProdID_textbox.Text = "";
                prodName_textbox.Text = "";
                price_textbox.Text = "";
                prodWeight_textbox.Text = "";
                cartOrderID_textbox.Text = "";
                cartProdID_textbox.Text = "";
                quantity_textbox.Text = "";
                soldOut_checkbox.Enabled = false;
            }
            else if (productOrder_checkbox.Checked == false)
            {
                phoneNumber_textbox.Text = tempPhoneNumber;
                productProdID_textbox.Text = tempProdID;
                prodName_textbox.Text = tempProdName;
                price_textbox.Text = tempPrice;
                prodWeight_textbox.Text = tempProdWeight;
                cartOrderID_textbox.Text = tempCartOrderID;
                cartProdID_textbox.Text = tempCartProdID;
                quantity_textbox.Text = tempQuantity;
                phoneNumber_textbox.ReadOnly = false;
                productProdID_textbox.ReadOnly = false;
                prodName_textbox.ReadOnly = false;
                price_textbox.ReadOnly = false;
                prodWeight_textbox.ReadOnly = false;
                cartOrderID_textbox.ReadOnly = false;
                cartProdID_textbox.ReadOnly = false;
                quantity_textbox.ReadOnly = false;
                soldOut_checkbox.Enabled = true;
            }
        }

        /* ----- TOGGLING/EDITING CUSTOMER FIELDS ----- */
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
            if (screen1Option != (int)CRUD.SEARCH)
            {
                if (orderOrderID_textbox.Text != "")
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
        }

        private void custID_textbox_TextChanged(object sender, EventArgs e)
        {
            if (screen1Option != (int)CRUD.SEARCH)
            {
                if (custID_textbox.Text != "")
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
        }

        private void poNumber_textbox_TextChanged(object sender, EventArgs e)
        {
            if (screen1Option != (int)CRUD.SEARCH)
            {
                if (poNumber_textbox.Text != "")
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
        }

        private void orderDate_textbox_TextChanged(object sender, EventArgs e)
        {
            if (screen1Option != (int)CRUD.SEARCH)
            {
                if (orderDate_textbox.Text != "")
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
        }

        /* ----- EDITING CART FIELDS ----- */
        private void cartOrderID_textbox_TextChanged(object sender, EventArgs e)
        {
            if (screen1Option != (int)CRUD.SEARCH)
            {
                if (cartOrderID_textbox.Text != "")
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
        }

        private void cartProdID_textbox_TextChanged(object sender, EventArgs e)
        {
            if (screen1Option != (int)CRUD.SEARCH)
            {
                if (cartProdID_textbox.Text != "")
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
        }

        private void quantity_textbox_TextChanged(object sender, EventArgs e)
        {
            if (screen1Option != (int)CRUD.SEARCH)
            {
                if (quantity_textbox.Text != "")
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
        }

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
        /* ----- END FIELD EDITING/TOGGLING ----- */


        /* ----- BUTTON CLICKS ----- */

        private void back_button_Click(object sender, EventArgs e)
        {
            screen1 = this.Owner as Screen1;
            screen1.ShowButtons();
            this.Close();
        }

        private void execute_button_Click(object sender, EventArgs e)
        {
            if (screen1Option != (int)CRUD.SEARCH)
            {
                if (firstName_textbox.Enabled)
                {
                    if (screen1Option == (int)CRUD.INSERT)
                    {
                        if (firstName_textbox.Text != "")
                        {
                            if (!ValidateCustomerName(firstName_textbox.Text, 1))
                            {
                                return;
                            }
                        }
                        if (!ValidateCustomerName(lastName_textbox.Text, 2))
                        {
                            return;
                        }
                        if (!ValidateCustomerPhoneNumber())
                        {
                            return;
                        }
                    }
                    if (screen1Option == (int)CRUD.UPDATE)
                    {
                        if (customerCustID_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(customerCustID_textbox.Text))
                            {
                                return;
                            }
                            if (firstName_textbox.Text != "")
                            {
                                if (!ValidateCustomerName(firstName_textbox.Text, 1))
                                {
                                    return;
                                }
                            }
                            if (lastName_textbox.Text != "")
                            {
                                if (!ValidateCustomerName(lastName_textbox.Text, 2))
                                {
                                    return;
                                }
                            }
                            if (phoneNumber_textbox.Text != "")
                            {
                                if (!ValidateCustomerPhoneNumber())
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (firstName_textbox.Text != "")
                            {
                                if (!ValidateCustomerName(firstName_textbox.Text, 1))
                                {
                                    return;
                                }
                            }
                            if (!ValidateCustomerName(lastName_textbox.Text, 2))
                            {
                                return;
                            }
                            if (!ValidateCustomerPhoneNumber())
                            {
                                return;
                            }
                        }
                    }
                    if (screen1Option == (int)CRUD.DELETE)
                    {
                        if (customerCustID_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(customerCustID_textbox.Text))
                            {
                                return;
                            }
                            else
                            {
                                firstName_textbox.Text = "";
                                lastName_textbox.Text = "";
                                phoneNumber_textbox.Text = "";
                            }
                        }
                        else
                        {
                            if (firstName_textbox.Text != "")
                            {
                                if (!ValidateCustomerName(firstName_textbox.Text, 1))
                                {
                                    return;
                                }
                            }
                            if (lastName_textbox.Text != "")
                            {
                                if (!ValidateCustomerName(lastName_textbox.Text, 2))
                                {
                                    return;
                                }
                            }
                            if (phoneNumber_textbox.Text != "")
                            {
                                if (!ValidateCustomerPhoneNumber())
                                {
                                    return;
                                }
                            }
                        }
                    }

                    Customer customer = new Customer();
                    if (customerCustID_textbox.Text != "")
                    {
                        customer.custID = Convert.ToInt32(customerCustID_textbox.Text);
                    }
                    customer.firstName = firstName_textbox.Text;
                    customer.lastName = lastName_textbox.Text;
                    customer.phoneNumber = phoneNumber_textbox.Text;

                    queryCustomer(customer);
                }
                else if (prodName_textbox.Enabled)
                {
                    if (screen1Option == (int)CRUD.INSERT)
                    {
                        if (prodName_textbox.Text == "")
                        {
                            MessageBox.Show("You did not enter a product name");
                            return;
                        }
                        if (!ValidateProductPriceOrWeight(price_textbox.Text, 1))
                        {
                            return;
                        }
                        if (!ValidateProductPriceOrWeight(prodWeight_textbox.Text, 2))
                        {
                            return;
                        }
                    }
                    if (screen1Option == (int)CRUD.UPDATE)
                    {
                        if (productProdID_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(productProdID_textbox.Text))
                            {
                                return;
                            }
                        }
                        if (price_textbox.Text != "")
                        {
                            if (!ValidateProductPriceOrWeight(price_textbox.Text, 1))
                            {
                                return;
                            }
                        }
                        if (prodWeight_textbox.Text != "")
                        {
                            if (!ValidateProductPriceOrWeight(prodWeight_textbox.Text, 2))
                            {
                                return;
                            }
                        }

                    }
                    if (screen1Option == (int)CRUD.DELETE)
                    {
                        if (productProdID_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(productProdID_textbox.Text))
                            {
                                return;
                            }
                            else
                            {
                                prodName_textbox.Text = "";
                                price_textbox.Text = "";
                                prodWeight_textbox.Text = "";
                            }
                        }
                        else
                        {
                            if (price_textbox.Text != "")
                            {
                                if (!ValidateProductPriceOrWeight(price_textbox.Text, 1))
                                {
                                    return;
                                }
                            }
                            if (prodWeight_textbox.Text != "")
                            {
                                if (!ValidateProductPriceOrWeight(prodWeight_textbox.Text, 2))
                                {
                                    return;
                                }
                            }
                        }
                    }
                    Product product = new Product();
                    if (productProdID_textbox.Text != "")
                    {
                        product.prodID = Convert.ToInt32(productProdID_textbox.Text);
                    }
                    product.prodName = prodName_textbox.Text;
                    if (price_textbox.Text != "")
                    {
                        product.price = Convert.ToDouble(price_textbox.Text);
                    }
                    if (prodWeight_textbox.Text != "")
                    {
                        product.prodWeight = Convert.ToDouble(prodWeight_textbox.Text);
                    }
                    product.inStock = soldOut_checkbox.Checked;
                    queryProduct(product);
                }
                else if (custID_textbox.Enabled)
                {
                    if (screen1Option == (int)CRUD.INSERT)
                    {
                        if (!ValidateIDOrQuantity(custID_textbox.Text))
                        {
                            return;
                        }
                        if (poNumber_textbox.Text != "")
                        {
                            if (!ValidateOrderPONumber())
                            {
                                return;
                            }
                        }
                        if (!ValidateOrderDate(orderDate_textbox.Text))
                        {
                            return;
                        }
                    }
                    if (screen1Option == (int)CRUD.UPDATE)
                    {
                        if (orderOrderID_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(orderOrderID_textbox.Text))
                            {
                                return;
                            }
                        }
                        if (custID_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(custID_textbox.Text))
                            {
                                return;
                            }
                        }
                        if (poNumber_textbox.Text != "")
                        {
                            if (!ValidateOrderPONumber())
                            {
                                return;
                            }
                        }
                        if (orderDate_textbox.Text != "")
                        {
                            if (!ValidateOrderDate(orderDate_textbox.Text))
                            {
                                return;
                            }
                        }
                    }
                    if (screen1Option == (int)CRUD.DELETE)
                    {
                        if (orderOrderID_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(orderOrderID_textbox.Text))
                            {
                                return;
                            }
                            else
                            {
                                custID_textbox.Text = "";
                                poNumber_textbox.Text = "";
                                orderDate_textbox.Text = "";
                            }
                        }
                        else
                        {
                            if (custID_textbox.Text != "")
                            {
                                if (!ValidateIDOrQuantity(custID_textbox.Text))
                                {
                                    return;
                                }
                            }
                            if (poNumber_textbox.Text != "")
                            {
                                if (!ValidateOrderPONumber())
                                {
                                    return;
                                }
                            }
                            if (orderDate_textbox.Text != "")
                            {
                                if (!ValidateOrderDate(orderDate_textbox.Text))
                                {
                                    return;
                                }
                            }
                        }
                    }

                    Order order = new Order();

                    if (orderOrderID_textbox.Text != "")
                    {
                        order.orderID = Convert.ToInt32(orderOrderID_textbox.Text);
                    }
                    if (custID_textbox.Text != "")
                    {
                        order.custID = Convert.ToInt32(custID_textbox.Text);
                    }
                    if (poNumber_textbox.Text != "")
                    {
                        order.poNumber = poNumber_textbox.Text;
                    }
                    if (orderDate_textbox.Text != "")
                    {
                        string expectedFormat = "MM-DD-YY";
                        DateTime outDate;
                        DateTime.TryParseExact(orderDate_textbox.Text, expectedFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out outDate);
                        order.orderDate = outDate;
                    }
                    queryOrder(order);
                }
                else
                {
                    if (screen1Option == (int)CRUD.INSERT)
                    {
                        if (!ValidateIDOrQuantity(cartOrderID_textbox.Text))
                        {
                            return;
                        }
                        if (!ValidateIDOrQuantity(cartProdID_textbox.Text))
                        {
                            return;
                        }
                        if (!ValidateIDOrQuantity(quantity_textbox.Text))
                        {
                            return;
                        }
                    }
                    else if (screen1Option == (int)CRUD.UPDATE)
                    {
                        if (cartOrderID_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(cartOrderID_textbox.Text))
                            {
                                return;
                            }
                        }
                        if (cartProdID_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(cartProdID_textbox.Text))
                            {
                                return;
                            }
                        }
                        if (quantity_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(quantity_textbox.Text))
                            {
                                return;
                            }
                        }
                    }
                    else if (screen1Option == (int)CRUD.DELETE)
                    {
                        if (!ValidateIDOrQuantity(cartOrderID_textbox.Text))
                        {
                            return;
                        }
                        if (cartProdID_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(cartProdID_textbox.Text))
                            {
                                return;
                            }
                        }
                        if (quantity_textbox.Text != "")
                        {
                            if (!ValidateIDOrQuantity(quantity_textbox.Text))
                            {
                                return;
                            }
                        }
                    }
                    Cart cart = new Cart();
                    if (cartOrderID_textbox.Text != "")
                    {
                        cart.orderID = Convert.ToInt32(cartOrderID_textbox.Text);
                    }
                    if (cartProdID_textbox.Text != "")
                    {
                        cart.prodID = Convert.ToInt32(cartProdID_textbox.Text);
                    }
                    if (quantity_textbox.Text != "")
                    {
                        cart.quantity = Convert.ToInt32(quantity_textbox.Text);
                    }

                    queryCart(cart);
                }
            }
            else
            {
                if (customerCustID_textbox.Text != "")
                {
                    if (!ValidateIDOrQuantity(customerCustID_textbox.Text))
                    {
                        return;
                    }
                }
                if (firstName_textbox.Text != "")
                {
                    if (!ValidateCustomerName(firstName_textbox.Text, 1))
                    {
                        return;
                    }
                }
                if (lastName_textbox.Text != "")
                {
                    if (!ValidateCustomerName(lastName_textbox.Text, 2))
                    {
                        return;
                    }
                }
                if (phoneNumber_textbox.Text != "")
                {
                    if (!ValidateCustomerPhoneNumber())
                    {
                        return;
                    }
                }
                if (productProdID_textbox.Text != "")
                {
                    if (!ValidateIDOrQuantity(productProdID_textbox.Text))
                    {
                        return;
                    }
                }
                if (price_textbox.Text != "")
                {
                    if (!ValidateProductPriceOrWeight(price_textbox.Text, 1))
                    {
                        return;
                    }
                }
                if (prodWeight_textbox.Text != "")
                {
                    if (!ValidateProductPriceOrWeight(prodWeight_textbox.Text, 2))
                    {
                        return;
                    }
                }
                if (orderOrderID_textbox.Text != "")
                {
                    if (!ValidateIDOrQuantity(orderOrderID_textbox.Text))
                    {
                        return;
                    }
                }
                if (custID_textbox.Text != "")
                {
                    if (!ValidateIDOrQuantity(custID_textbox.Text))
                    {
                        return;
                    }
                }
                if (poNumber_textbox.Text != "")
                {
                    if (!ValidateOrderPONumber())
                    {
                        return;
                    }
                }
                if (orderDate_textbox.Text != "")
                {
                    if (!ValidateOrderDate(orderDate_textbox.Text))
                    {
                        return;
                    }
                }
                if (cartOrderID_textbox.Text != "")
                {
                    if (!ValidateIDOrQuantity(cartOrderID_textbox.Text))
                    {
                        return;
                    }
                }
                if (cartProdID_textbox.Text != "")
                {
                    if (!ValidateIDOrQuantity(cartProdID_textbox.Text))
                    {
                        return;
                    }
                }
                if (quantity_textbox.Text != "")
                {
                    if (!ValidateIDOrQuantity(quantity_textbox.Text))
                    {
                        return;
                    }
                }
                if (productOrder_checkbox.Checked == true)
                {
                    screen1 = this.Owner as Screen1;
                    
                    screen1.createScreen3();
                    this.Close();
                }
                else
                {
                    string search = "";
                    if (customerCustID_textbox.Text != "")
                    {
                        search += "Customer.custID=" + customerCustID_textbox.Text + "/";
                    }
                    if (firstName_textbox.Text != "")
                    {
                        search += "Customer.firstName=" + firstName_textbox.Text + "/";
                    }
                    if (lastName_textbox.Text != "")
                    {
                        search += "Customer.lastName=" + lastName_textbox.Text + "/";
                    }
                    if (phoneNumber_textbox.Text != "")
                    {
                        search += "Customer.phoneNumber=" + phoneNumber_textbox + "/";
                    }
                    if (productProdID_textbox.Text != "")
                    {
                        search += "Product.prodID=" + productProdID_textbox.Text + "/";
                    }
                    if (prodName_textbox.Text != "")
                    {
                        search += "Product.prodName=" + prodName_textbox.Text + "/";
                    }
                    if (price_textbox.Text != "")
                    {
                        search += "Product.price=" + price_textbox.Text + "/";
                    }
                    if (prodWeight_textbox.Text != "")
                    {
                        search += "Product.prodWeight=" + prodWeight_textbox.Text + "/";
                    }
                    if (orderOrderID_textbox.Text != "")
                    {
                        search += "Order.orderID=" + orderOrderID_textbox.Text + "/";
                    }
                    if (custID_textbox.Text != "")
                    {
                        search += "Order.custID=" + custID_textbox.Text + "/";
                    }
                    if (poNumber_textbox.Text != "")
                    {
                        search += "Order.poNumber=" + poNumber_textbox.Text + "/";
                    }
                    if (orderDate_textbox.Text != "")
                    {
                        search += "Order.orderDate=" + orderDate_textbox.Text + "/";
                    }
                    if (cartOrderID_textbox.Text != "")
                    {
                        search += "Cart.orderID=" + cartOrderID_textbox.Text + "/";
                    }
                    if (cartProdID_textbox.Text != "")
                    {
                        search += "Cart.prodID=" + cartProdID_textbox.Text + "/";
                    }
                    if (quantity_textbox.Text != "")
                    {
                        search += "Cart.quantity=" + quantity_textbox.Text + "/";
                    }
                    if (search.Length == 0)
                    {
                        MessageBox.Show("You did not enter anything to search");
                        return;
                    }
                    search.Remove(search.Length - 1);

                    this.search(search);
                }
            }
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /* ----- END BUTTON CLICKS ----- */

        /* ----- QUERIES ----- */
        private async void search(string parameters)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlClass.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                JsonMediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                jsonFormatter.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;

                response = await client.GetAsync("api/Search/" + parameters);
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<CrazyMelDataModel> data;
                    data = await response.Content.ReadAsAsync<CrazyMelDataModel[]>(new MediaTypeFormatter[] { jsonFormatter });
                    List<Customer> customers = new List<Customer>();
                    List<Product> products = new List<Product>();
                    List<Order> orders = new List<Order>();
                    List<Cart> carts = new List<Cart>();

                    foreach (CrazyMelDataModel model in data)
                    {
                        if (model.GetType() == typeof(Customer))
                        {
                            customers.Add((Customer)model);
                        }
                        if (model.GetType() == typeof(Product))
                        {
                            products.Add((Product)model);
                        }
                        if (model.GetType() == typeof(Order))
                        {
                            orders.Add((Order)model);
                        }
                        if (model.GetType() == typeof(Cart))
                        {
                            carts.Add((Cart)model);
                        }
                    }

                    Screen4 screen4 = new Screen4();
                    screen4.customerList(customers);
                    screen4.productList(products);
                    screen4.orderList(orders);
                    screen4.cartList(carts);
                    screen4.positionButtons();
                    screen4.Show();
                }
            }
        }

        private async void queryCustomer(Customer table)
        {
            using (var client = new HttpClient())
            {
                string path;
                client.BaseAddress = new Uri(urlClass.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;

                if (screen1Option == (int)CRUD.INSERT)
                {
                    response = await client.PostAsJsonAsync("api/Customer/", table);
                }
                else if (screen1Option == (int)CRUD.UPDATE)
                {
                    response = await client.PutAsJsonAsync("api/Customer/", table);
                }
                else if (screen1Option == (int)CRUD.DELETE)
                {
                    path = "api/Customer/custID=" + table.custID + "/firstName=" + table.firstName + "/lastName=" + table.lastName + "/phoneNumber=" + table.phoneNumber;
                    response = await client.DeleteAsync(path);
                }
            }
        }

        private async void queryProduct(Product table)
        {
            using (var client = new HttpClient())
            {
                string path;
                client.BaseAddress = new Uri(urlClass.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;


                if (screen1Option == (int)CRUD.INSERT)
                {
                    response = await client.PostAsJsonAsync("api/Customer/", table);
                }
                else if (screen1Option == (int)CRUD.UPDATE)
                {
                    response = await client.PutAsJsonAsync("api/Customer/", table);
                }
                else if (screen1Option == (int)CRUD.DELETE)
                {
                    path = "api/Customer/prodID=" + table.prodID + "/prodName=" + table.prodName + "/price=" + table.price + "/prodWeight=" + table.prodWeight + "/inStock=" + table.inStock;
                    response = await client.DeleteAsync(path);
                }
            }
        }

        private async void queryOrder(Order table)
        {
            using (var client = new HttpClient())
            {
                string path = "api/Order/orderID=" + table.orderID + "/custID=" + table.custID + "/poNumber=" + table.poNumber + "/orderDate=" + table.orderDate;
                client.BaseAddress = new Uri(urlClass.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;


                if (screen1Option == (int)CRUD.INSERT)
                {
                    response = await client.PostAsJsonAsync("api/Order/", table);
                }
                else if (screen1Option == (int)CRUD.UPDATE)
                {
                    response = await client.PutAsJsonAsync("api/Order/", table);
                }
                else if (screen1Option == (int)CRUD.DELETE)
                {
                    response = await client.DeleteAsync(path);
                }
            }
        }

        private async void queryCart(Cart table)
        {
            using (var client = new HttpClient())
            {
                string path = "api/Cart/orderID=" + table.orderID + "/prodID=" + table.prodID + "/quantity=" + table.quantity;
                client.BaseAddress = new Uri(urlClass.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;

                
                if (screen1Option == (int)CRUD.INSERT)
                {
                    response = await client.PostAsJsonAsync("api/Cart/", table);
                }
                else if (screen1Option == (int)CRUD.UPDATE)
                {
                    response = await client.PutAsJsonAsync("api/Cart/", table);
                }
                else if (screen1Option == (int)CRUD.DELETE)
                {
                    response = await client.DeleteAsync(path);
                }
            }
        }
        /* ----- END QUERIES ----- */

        /* ----- VALIDATION ----- */
        public bool ValidateIDOrQuantity(string id)
        {
            if (screen1Option != (int)CRUD.INSERT)
            {
                try
                {
                    if (Convert.ToUInt32(id) > Int32.MaxValue)
                    {
                        MessageBox.Show("You entered a number number larger than the maximum allowed number of " + Int32.MaxValue);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("You did not enter a number or you entered an invalid number.");
                    return false;
                }
            }

            return true;
        }

        public bool ValidateCustomerName(string name, int type)
        {
            string namePrefix = "";
            if (type == 1)
            {
                namePrefix = "first";
            }
            else
            {
                namePrefix = "last";
            }

            rgx = new Regex("[a-zA-Z ,.'-]{2,50}");
            if (!rgx.IsMatch(name))
            {
                MessageBox.Show("You did not enter a valid " + namePrefix + " name. The accepted characters are 'a' through 'z' ',' '.' ' ' '-' and '. The name must also be between 2 and 50 characters long.");
                return false;
            }

            return true;
        }

        public bool ValidateCustomerPhoneNumber()
        {
            rgx = new Regex("[0-9]{3}-[0-9]{3}-[0-9]{4}");
            if (!rgx.IsMatch(phoneNumber_textbox.Text))
            {
                MessageBox.Show("You did not enter a valid phone number");
                return false;
            }

            return true;
        }


        public bool ValidateProductPriceOrWeight(string text, int type)
        {
            string priceOrWeight = "";
            if (type == 1)
            {
                priceOrWeight = "price";
            }
            else
            {
                priceOrWeight = "Weight";
            }

            try
            {
                if (Convert.ToDouble(text) < 0)
                {
                    MessageBox.Show("You may not enter a negative number for " + priceOrWeight);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You did not enter a number or you entered an invalid number for " + priceOrWeight + ".");
                return false;
            }


            return true;
        }

        public bool ValidateOrderPONumber()
        {
            rgx = new Regex("[a-zA-Z-]{2,50}");
            if (!rgx.IsMatch(poNumber_textbox.Text))
            {
                MessageBox.Show("You did not enter a valid last name. The accepted characters are 'a' through 'z' ',' '.' ' ' '-' and '. The name must also be between 2 and 50 characters long.");
                return false;
            }

            return true;
        }

        public bool ValidateOrderDate(string date)
        {
            string expectedFormat = "MM-DD-YY";
            DateTime outDate;
            if (DateTime.TryParseExact(date, expectedFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out outDate))
            {
                return true;
            }
            else
            {
                MessageBox.Show("You must enter the date in the format MM-DD-YY");
                return false;
            }
        }
        /* ----- END VALIDATION ----- */
    }
}
