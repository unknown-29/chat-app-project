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
    public partial class Login : Form
    {
        private string _msg;
        public Login()
        {
            InitializeComponent();
        }
        public Login(string msg)
        {
            InitializeComponent();
            this._msg = msg;
        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MySession.Username != null && MySession.Username.Length != 0) MySession.UsersService.Logout(MySession.Username);
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            label1.Text = "username";
            label4.Text = "password";
            label3.Text = "";
            label2.Text = "";
            label5.Text = "";
            label3.ForeColor = Color.Red;
            label2.ForeColor = Color.Red;
            label5.ForeColor = Color.Green;
            button1.Text = "login";
            button2.Text = "register";
            if (_msg != null && _msg.Length > 0) { label5.Text = _msg; _msg = null; }
        }
        // login
        private void button1_Click(object sender, EventArgs e)
        {
            // reset the warnings
            label3.Text = "";
            label2.Text = "";
            label5.Text = "";
            // validate
            if (textBox1.Text.Length == 0) { label3.Text = "username can't be empty"; return; }
            if (textBox2.Text.Length == 0) { label2.Text = "password can't be empty"; return; }
            string username = textBox1.Text;
            string password = textBox2.Text;
            string response = MySession.UsersService.Login(username, password);
            switch(response)
            {
                case "success":
                    MySession.Username = username;
                    new UserDashboard
                    {
                        Location = this.Location
                    }.Show();
                    this.Hide();
                    break;
                case "username not found":
                    label3.Text = "username not found";
                    break;
                case "wrong password":
                    label2.Text = "wrong password";
                    break;
            }
        }
        //register
        private void button2_Click(object sender, EventArgs e)
        {
            new Register
            {
                Location = this.Location
            }.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
