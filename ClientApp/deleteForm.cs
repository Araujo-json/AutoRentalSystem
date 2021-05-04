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
    public partial class deleteForm : Form
    {
        public deleteForm()
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
                    //Call Customer Object Load(ID) method to execute SELECT query
                    //to the database and populate itself with the record returned
                    //from the query
                    bool success = c.Delete(cardToDelete.Text.Trim());
                    //Step 2-If validate customer is found
                    if (success)
                    {
                        //Step4-Prompt user customer was deleted
                        MessageBox.Show("Customer Deleted");

                    }
                    else
                    {
                        //Step 4-prompt user customer not found
                        MessageBox.Show("Customer Not Found");

                        //Step 5-Clear all controls
                        cardToDelete.Text = "";

                    }
                }//End of try
                 //Step B-Trap for BO, App & General Exceptions
                catch (System.Exception)
                {
                    //Step C- throw system exception since run time error has occured;
                    MessageBox.Show("Error Deleting customer!");
                }
            }
            /*CreditCard c = new CreditCard();
            string card = textBox.Text;
            c.Delete(card);*/
    }
}
