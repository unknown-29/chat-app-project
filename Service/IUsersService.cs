using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [ServiceContract]
    public interface IUsersService
    {
        [OperationContract] string Login(string username, string password);
        [OperationContract] bool Register(string username, string password);
        [OperationContract] IEnumerable<Users> GetAllUsers();
        [OperationContract] IEnumerable<Users> SearchUsers(string username);
        [OperationContract] Users FindUser(string username);
    }
}
