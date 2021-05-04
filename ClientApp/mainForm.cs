using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }


        private void search_Click(object sender, EventArgs e)
        {
            //this.Hide();
            customerSearchForm f2 = new customerSearchForm();
            f2.Show();
        }
        
        private void registration_Click(object sender, EventArgs e)
        {
            //this.Hide();
            addCardForm f3 = new addCardForm();
            f3.Show();
        }

        private void update_Click(object sender, EventArgs e)
        {
            //this.Hide();
            editCard f4 = new editCard();
            f4.Show();
        }
        
        private void delete_Click(object sender, EventArgs e)
        {
            //this.Hide();
            deleteForm f5 = new deleteForm();
            f5.Show();
        }

      private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbl_Click(object sender, EventArgs e)
        {

        }

    }
}
