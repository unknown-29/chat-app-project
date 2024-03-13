using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UsersService : IUsersService
    {
        private readonly string _dbString = @"Data Source=192.168.228.23;Initial Catalog=chatdb;Integrated Security=True;Connect Timeout=30";
        public Users FindUser(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbString))
                {
                    SqlCommand cmd = new SqlCommand("select * from users where username = @username",conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Users { Username = reader["username"].ToString(), Password = reader["password"].ToString(), LastSeen = DateTime.Parse(reader["last_seen"].ToString()), IsOnline = bool.Parse(reader["is_online"].ToString()) };
                        }
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
            return null;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            List<Users> users = new List<Users>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbString))
                {
                    SqlCommand cmd = new SqlCommand("select * from users",conn);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new Users { Username = reader["username"].ToString(), Password = reader["password"].ToString(), LastSeen = DateTime.Parse(reader["last_seen"].ToString()), IsOnline = bool.Parse(reader["is_online"].ToString()) });
                        }
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
            return users;
        }

        public string GetUserStatus(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbString))
                {
                    SqlCommand cmd = new SqlCommand("select * from users where username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return bool.Parse(reader["is_online"].ToString()) ? "Online" : "Last Seen " + DateTime.Parse(reader["last_seen"].ToString()).ToString("dddd, dd MMMM yyyy HH:mm");
                        }
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
            return "";
        }

        public void SetUserOnline(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbString))
                {
                    SqlCommand cmd = new SqlCommand("update users set is_online=@is_online where username=@username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@is_online", true);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    //using (SqlDataReader reader = cmd.ExecuteReader())
                    //{
                    //    while (reader.Read())
                    //    {
                    //        return bool.Parse(reader["is_online"].ToString()) ? "Online" : "Last Seen " + DateTime.Parse(reader["last_seen"].ToString()).ToString("dddd, dd MMMM yyyy HH:mm");
                    //    }
                    //}
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
        }

        public string Login(string username, string password)
        {
            string response = "username not found";
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbString))
                {
                    SqlCommand cmd = new SqlCommand("select * from users where username=@username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            if (reader["password"].ToString() != password) { 
                                response = "wrong password";
                                return response;
                            }
                            reader.Close();
                            SqlCommand updatecmd = new SqlCommand("update users set is_online=@is_online where username=@username", conn);
                            updatecmd.Parameters.AddWithValue("@is_online", true);
                            updatecmd.Parameters.AddWithValue("@username", username);
                            updatecmd.ExecuteNonQuery();
                            response = "success";
                        }
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
            return response;
            //throw new NotImplementedException();
        }

        public void Logout(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbString))
                {
                    SqlCommand cmd = new SqlCommand("update users set last_seen=@last_seen, is_online=@is_online where username=@username", conn);
                    cmd.Parameters.AddWithValue("@last_seen", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@is_online", false);
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return;
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
        }

        public bool Register(string username, string password)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbString))
                {
                    SqlCommand cmd = new SqlCommand("insert into users (username,password) values (@username,@password)",conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    conn.Open();
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
            return result;
        }

        public IEnumerable<Users> SearchUsers(string username)
        {
            List<Users> users = new List<Users>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbString))
                {
                    SqlCommand cmd = new SqlCommand("select * from users where username like @username",conn);
                    cmd.Parameters.AddWithValue("@username", username+"%");
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new Users { Username = reader["username"].ToString(), Password = reader["password"].ToString(), LastSeen = DateTime.Parse(reader["last_seen"].ToString()), IsOnline = bool.Parse(reader["is_online"].ToString()) });
                        }
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
            return users;
            //throw new NotImplementedException();
        }
    }
}
