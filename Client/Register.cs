using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MySession.Username != null && MySession.Username.Length != 0) MySession.UsersService.Logout(MySession.Username);
            Application.Exit();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            label1.Text = "username";
            label4.Text = "password";
            label3.Text = "";
            label2.Text = "";
            label3.ForeColor = Color.Red;
            label2.ForeColor = Color.Red;
            button1.Text = "register";
            button2.Text = "login";
        }

        // register
        private void button1_Click(object sender, EventArgs e)
        {
            // reset the warnings
            label3.Text = "";
            label2.Text = "";

            // validate
            if (textBox1.Text.Length == 0) { label3.Text = "username can't be empty"; return; }
            if (textBox2.Text.Length == 0) { label2.Text = "password can't be empty"; return; }
            string username = textBox1.Text;
            string password = textBox2.Text;
            bool response = MySession.UsersService.Register(username, password);
            if (response)
            {
                new Login("user registered successfully\nyou can now login").Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("there is some error.\n(1) make sure to have a unique username.\n(2) server is busy try again later.");
            }
        }

        // login
        private void button2_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }
    }
}
