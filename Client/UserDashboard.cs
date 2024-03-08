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
    public partial class UserDashboard : Form
    {
        public UserDashboard()
        {
            InitializeComponent();
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            if(MySession.Username==null || MySession.Username.Length==0)
            {
                new Login("please login").Show();
                this.Hide();
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Username");
            IEnumerable<UsersService.Users> users = MySession.UsersService.GetAllUsers().Where(user => !user.Username.Equals(MySession.Username));
            foreach (var user in users)
            {
                dt.Rows.Add(new string[] {user.Username});
            }
            dataGridView1.DataSource = dt;
            //dataGridView1.Columns[0].DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleCenter;
        }

        private void UserDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Username");
            IEnumerable<UsersService.Users> users = MySession.UsersService.SearchUsers(textBox1.Text.ToString()).Where(user => !user.Username.Equals(MySession.Username));
            foreach (var user in users)
            {
                dt.Rows.Add(new string[] { user.Username });
            }
            dataGridView1.DataSource = dt;
            //dataGridView1.Columns[0].DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleCenter;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.SelectedCells[0].Value.ToString());
            new ChatRoom(dataGridView1.SelectedCells[0].Value.ToString()).Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySession.Username = null;
            new Login("logout successfully").Show();
            this.Hide();
        }
    }
}
