using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UsersService : IUsersService
    {
        private readonly string _dbString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=chatdb;Integrated Security=True;Connect Timeout=30";
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
                            return new Users { Username = reader["username"].ToString(), Password = reader["password"].ToString() };
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
                            users.Add(new Users { Username = reader["username"].ToString(), Password = reader["password"].ToString() });
                        }
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
            return users;
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
                        if(reader.Read())
                        {
                            response = reader["password"].ToString() == password ? "success" : "wrong password";
                        }
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message.ToString()); }
            return response;
            //throw new NotImplementedException();
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
                            users.Add(new Users { Username = reader["username"].ToString(), Password = reader["password"].ToString() });
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
