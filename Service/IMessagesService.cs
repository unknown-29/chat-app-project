using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [ServiceContract]
    public interface IMessagesService
    {
        [OperationContract] void SendMessage(string sender, string receiver, string messageContent);
        [OperationContract] IEnumerable<Messages> GetChat(string user1, string user2);
    }
}
