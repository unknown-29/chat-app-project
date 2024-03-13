using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        [DataMember] public DateTime LastSeen { get; set; }
        [DataMember] public bool IsOnline { get; set; }
    }
    /*
     CREATE TABLE [dbo].[Users] (
    [username]  VARCHAR (50) NOT NULL,
    [password]  VARCHAR (50) NOT NULL,
    [last_seen] DATETIME     NOT NULL DEFAULT GETDATE(),
    [is_online] BIT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([username] ASC)
    );
     */
}
