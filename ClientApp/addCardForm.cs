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
    public partial class addCardForm : Form
    {
        public addCardForm()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void apply_Click(object sender, EventArgs e)
        {
            //Step A - start Excption handling
            try
            {
                //Step1 - Create a Customer Object
                CreditCard c = new CreditCard();
                //Set Object with parameters values
                
                c.CardNumber = cardNumber.Text.Trim();
                c.CardOwnerName = cardOwnerName.Text.Trim();
                c.MerchantName = cardCompany.Text.Trim();
                c.ExpDate = Convert.ToDateTime(expDate.Text.Trim());
                c.AddressLines1 = address1.Text.Trim();
                c.City = city.Text.Trim();
                c.State = state.Text.Trim();
                c.ZipCode = zip.Text.Trim();
                c.Country = country.Text.Trim();

                //Call Customer Object Insert()) method to execute INSERT query
                //Using the populated object's data to create Inser query
                bool success = c.Insert();
                //Step 2-If validate customer was added
                if (success)
                {
                    //Prompt user customer was added
                    MessageBox.Show("Customer Added");
                    //Step 5-Clear all controls
                    
                    cardNumber.Text = "";
                    cardOwnerName.Text = "";
                    cardCompany.Text = "";
                    expDate.Text = "";
                    address1.Text = "";
                    city.Text = "";
                    state.Text = "";
                    zip.Text = "";
                    country.Text = "";

                }
                else
                {
                    //prompt user customer was not added
                    MessageBox.Show("Error! Customer was not added!");
                }
            }//End of try
             //Step B-Trap for BO, App & General Exceptions
            catch (System.Exception)
            {
                //Step C- throw system exception since run time error has occured;
                MessageBox.Show("Error! Customer was not added!");
            }
        }

    }
}
