using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BOLayer;

namespace ClientApp
{
    public partial class editCard : Form
    {
        
        private CreditCard c;
        
        public editCard()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void apply_Click(object sender, EventArgs e)
        {
            c = new CreditCard();
            try
            {
               
                c.CardNumber = cardNumber.Text.Trim();
                c.CardOwnerName = cardOwner.Text.Trim();
                c.MerchantName = cardCompany.Text.Trim();
                c.ExpDate = Convert.ToDateTime(expDate.Text.Trim());
                c.AddressLines1 = city.Text.Trim();
                c.City = state.Text.Trim();
                c.State = zip.Text.Trim();
                c.ZipCode = country.Text.Trim();
                c.Country = textBox11.Text.Trim();
                //c.CreditLimit = cardNumberSearch.Text.Trim();
                bool success = c.Update();
                //Step 2-If validate customer was added
                if (success)
                {
                    //Prompt user customer was added
                    MessageBox.Show("Credit Card was Updated");

                    //Step 5-Clear all controls
                    cardNumber.Text = "";
                    cardOwner.Text = "";
                    cardCompany.Text = "";
                    expDate.Text = "";
                    city.Text = "";
                    state.Text = "";
                    zip.Text = "";
                    country.Text = "";
                    textBox11.Text = "";

                }
                else
                {
                    //prompt user customer was not added
                    MessageBox.Show("Error! Credit Card was not updated!");
                }

            }
            catch
            {
                MessageBox.Show("Error! Credit Card was not updated!");
            }
            
            //c.Update();
        }

        private void search_Click(object sender, EventArgs e)
        {
            //Step A - start Excption handling
            try
            {
                //**** PART 1 - SEARCH-GET CUSTOMER RECORD ****
                //Step1 - Create a Customer Object by assiging pointer to new object
                c = new CreditCard();
                //Call Customer Object Load(ID) method to execute SELECT query
                //to the database and populate itself with the record returned
                //from the query
                bool success = c.Load(cardNumberSearch.Text.Trim());
                //Step 2-If validate customer is found
                if (success)
                {
                    //Step 3-Then Data is extracted from customer object & placed on textboxes
                    cardNumber.Text = c.CardNumber;
                    cardOwner.Text = c.CardOwnerName;
                    cardCompany.Text = c.MerchantName;
                    expDate.Text = c.ExpDate.ToString(); //convert to a string
                    city.Text = c.AddressLines1;
                    state.Text = c.City;
                    zip.Text = c.State;
                    country.Text = c.ZipCode;
                    textBox11.Text = c.Country;
                    
                }
                else
                {
                    //Step 4-prompt user customer not found
                    MessageBox.Show("Customer Not Found");

                    //Step 5-Clear all controls
                    cardNumberSearch.Text = "";
                    cardNumber.Text = "";
                    cardOwner.Text = "";
                    cardCompany.Text = "";
                    expDate.Text = "";
                    address1.Text = "";
                    city.Text = "";
                    state.Text = "";
                    zip.Text = "";
                    country.Text = "";

                }
            }//End of try
             //Step B-Trap for BO, App & General Exceptions
            catch (System.Exception)
            {
                //Step C- throw system exception since run time error has occured;
                MessageBox.Show("Error in search!");
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
