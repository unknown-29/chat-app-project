using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ChatRoom : Form
    {
        private readonly string _receiver;
        private readonly BackgroundWorker _worker;
        public ChatRoom()
        {
            InitializeComponent();
        }

        public ChatRoom(string receiver)
        {
            InitializeComponent();
            this._receiver = receiver;
            this._worker = new BackgroundWorker();
            this._worker.DoWork += RefreshChat;
        }

        private void RefreshChat(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Invoke(new Action(() =>
                LoadChat()));
                Thread.Sleep(3000);
            }
        }

        private void ChatRoom_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.RowsDefaultCellStyle.SelectionBackColor = Color.Empty;
            if (MySession.Username == null || MySession.Username.Length == 0)
            {
                new Login("please login").Show();
                this.Hide();
            }
            label1.Text = _receiver;
            // receiver is the other guy and sender is the guy logged in just now
            LoadChat();
            _worker.RunWorkerAsync();
        }

        private void LoadChat()
        {
            IEnumerable<MessagesService.Messages> chat = MySession.MessagesService.GetChat(MySession.Username, _receiver);
            DataTable dt = new DataTable();
            dt.Columns.Add("receiver");
            dt.Columns.Add("sender");
            foreach (var msg in chat)
            {
                if (MySession.Username == msg.Sender) dt.Rows.Add(new string[] { "", msg.MessageContent });
                else dt.Rows.Add(new string[] { msg.MessageContent, "" });
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if(dataGridView1.RowCount > 3) dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySession.MessagesService.SendMessage(MySession.Username, _receiver, textBox1.Text);
            textBox1.Clear();
            LoadChat();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new UserDashboard().Show();
            this.Hide();
        }

        private void ChatRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_SelectionChanged(Object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
