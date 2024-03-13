using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MessagesService : IMessagesService
    {
        private readonly string _dbString = @"Data Source=192.168.228.23;Initial Catalog=chatdb;Integrated Security=True;Connect Timeout=30";
        public IEnumerable<Messages> GetChat(string user1, string user2)
        {
            List<Messages> chat=new List<Messages>();
            try {
                using(SqlConnection conn= new SqlConnection(_dbString))
                {
                    SqlCommand cmd = new SqlCommand("select sender,receiver,message_content from messages where (receiver=@user1 and sender=@user2) or (receiver=@user2 and sender=@user1)", conn);
                    cmd.Parameters.AddWithValue("@user1", user1);
                    cmd.Parameters.AddWithValue("@user2", user2);
                    conn.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            chat.Add(new Messages { MessageContent = reader["message_content"].ToString(), Sender = reader["sender"].ToString(), Receiver = reader["receiver"].ToString() });
                        }
                    }
                }
            } catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
            return chat;
        }

        public void SendMessage(string sender, string receiver, string messageContent)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbString))
                {
                    SqlCommand cmd = new SqlCommand("insert into messages (sender,receiver,message_content) values (@sender,@receiver,@messageContent)", conn);
                    cmd.Parameters.AddWithValue("@sender", sender);
                    cmd.Parameters.AddWithValue("@receiver", receiver);
                    cmd.Parameters.AddWithValue("@messageContent", messageContent);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
        }
    }
}
