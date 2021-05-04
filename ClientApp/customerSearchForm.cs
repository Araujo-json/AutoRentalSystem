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
    public partial class customerSearchForm : Form
    {
        public customerSearchForm()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void search_Click(object sender, EventArgs e)
        {
            try
            {
                //Step1 - Create a Customer Object
                CreditCard c = new CreditCard();
                //Call Customer Object Load(ID) method to execute SELECT query
                //to the database and populate itself with the record returned
                //from the query
                bool success = c.Load(textBox1.Text.Trim());
                //Step 2-If validate customer is found
                if (success)
                {
                    //Step 3-Then Data is extracted from customer object & placed on textboxes
                    cardNumberField.Text = c.CardNumber;
                    cardOwnerField.Text = c.CardOwnerName;
                    cardcompanyField.Text = c.MerchantName;
                    expDateField.Text = c.ExpDate.ToString();
                    address1Field.Text = c.AddressLines1; //convert to a string
                    cityField.Text = c.City;
                    stateField.Text = c.State;
                    zipField.Text = c.ZipCode;
                    countryField.Text = c.Country;
                    //zipField.Text = c.CreditLimit.ToString();
                    //countryField.Text = c.ActivationStatus.ToString();

                    /*
                     CardNumber
                    OwnerName 
                    MerchantName
                    ExpDate
                    HouseStreetAddress
                    City
                    State
                    Zipcode
                    Country
                    CreditLimit
                    ActivationStatus
                     */
                }
                else
                {
                    //Step 4-prompt user customer not found
                    MessageBox.Show("Customer Not Found");
                    //Step 5-Clear all controls
                    cardNumberField.Text = "";
                    cardOwnerField.Text = "";
                    cardcompanyField.Text = "";
                    expDateField.Text = "";
                    address1Field.Text = "";
                    cityField.Text = "";
                    stateField.Text = "";
                    zipField.Text = "";
                    countryField.Text = "";

                }
            }//End of try
             //Step B-Trap for BO, App & General Exceptions
            catch (System.Exception )
            {
                //Step C- throw system exception since run time error has occured;
                MessageBox.Show("Error in search!");
            }

        }
        private void apply_Click(object sender, EventArgs e)
        {
            CreditCard c = new CreditCard();
            //string t1 = cardNumberSearch.Text;
            //c.Load(t1);
        }
    }
}
        /*private void apply_Click(object sender, EventArgs e)
        {
            CreditCard c = new CreditCard();
            string t1 = cardNumberSearch.Text;
            c.Load(t1);
        }
    }
}
        */