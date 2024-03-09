using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [DataContract]
    public class Messages
    {
        [DataMember] public string Sender { get; set; }
        [DataMember] public string MessageContent { get; set; }
        [DataMember] public string Receiver { get; set; }
    }
    /*
    CREATE TABLE[dbo].[Messages]
    (
    [sender] VARCHAR (50)  NOT NULL,
    [receiver]        VARCHAR(50)  NOT NULL,
    [message_content] VARCHAR(200) NOT NULL,
    [message_id]      INT IDENTITY(1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED([message_id] ASC),
    FOREIGN KEY([receiver]) REFERENCES[dbo].[Users] ([username]),
    FOREIGN KEY([sender]) REFERENCES[dbo].[Users] ([username])
    );*/
}
