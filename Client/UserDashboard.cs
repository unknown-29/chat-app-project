using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace Client
{
    public partial class UserDashboard : Form
    {
        private readonly BackgroundWorker _worker;
        public UserDashboard()
        {
            InitializeComponent();
            this._worker = new BackgroundWorker();
            this._worker.DoWork += RefreshUsersList;
            this._worker.WorkerSupportsCancellation = true;
        }

        private void RefreshUsersList(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (this._worker.CancellationPending) return;
                if (this.InvokeRequired)
                {
                    Invoke(new Action(() =>
                    LoadUsersList()));
                    Thread.Sleep(1000 * new Random().Next(3));
                }
                else
                {
                    LoadUsersList();
                }
            }
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            LoadUsersList();
            this._worker.RunWorkerAsync();
            //dataGridView1.Columns[0].DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadUsersList()
        {
            if (textBox1.Text.Length != 0) return;
            //dataGridView1.;
            int currentSelectedCell = -1; 
            if(dataGridView1.SelectedCells.Count > 0) { currentSelectedCell= dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]); }
            if (MySession.Username == null || MySession.Username.Length == 0)
            {
                new Login("please login") { Location = this.Location }.Show();
                this.Hide();
            }
            dataGridView1.DataSource = GetUsersList();
            if(currentSelectedCell!=-1) dataGridView1.Rows[currentSelectedCell].Selected = true;
        }

        private DataTable GetUsersList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Username");
            dt.Columns.Add("Status");
            IEnumerable<UsersService.Users> users = MySession.UsersService.GetAllUsers().Where(user => !user.Username.Equals(MySession.Username));
            foreach (var user in users)
            {
                dt.Rows.Add(new string[] { user.Username, user.IsOnline ? "Online" : "Last Seen " + user.LastSeen.ToString("dddd, dd MMMM yyyy HH:mm") });
            }
            return dt;
        }

        private void UserDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._worker.CancelAsync();
            if (MySession.Username != null && MySession.Username.Length != 0) MySession.UsersService.Logout(MySession.Username);
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Username");
            dt.Columns.Add("Status");
            IEnumerable<UsersService.Users> users = MySession.UsersService.SearchUsers(textBox1.Text.ToString()).Where(user => !user.Username.Equals(MySession.Username));
            foreach (var user in users)
            {
                dt.Rows.Add(new string[] { user.Username, user.IsOnline ? "Online" : user.LastSeen.ToString("yyyy-MM-dd HH:mm:ss") });
            }
            dataGridView1.DataSource = dt;
            //dataGridView1.Columns[0].DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleCenter;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this._worker.CancelAsync();
            //MessageBox.Show(dataGridView1.SelectedCells[0].Value.ToString());
            new ChatRoom(dataGridView1.SelectedCells[0].Value.ToString()) { Location = this.Location }.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FileInfo(MySession.SessionFilePath).Delete();
            this._worker.CancelAsync();
            MySession.UsersService.Logout(MySession.Username);
            MySession.Username = null;
            new Login("logout successfully") { Location = this.Location }.Show();
            this.Hide();
        }
    }
}
