using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [DataContract]
    public class Users
    {
        [DataMember] public string Username { get; set; }
        [DataMember] public string Password { get; set; }
    }
    /*
     CREATE TABLE [dbo].[Users] (
    [username] VARCHAR (50) NOT NULL,
    [password] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([username] ASC)
    );
     */
}
