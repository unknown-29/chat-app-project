using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class MySession
    {
        public static string Username { get; set; }
        public static readonly UsersService.IUsersService UsersService = new UsersService.UsersServiceClient();
        public static readonly MessagesService.IMessagesService MessagesService = new MessagesService.MessagesServiceClient();
    }
}
