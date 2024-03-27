# Chatbox

"Words that connect, chat that reflects."

The system will consist of the following components:

1 Client
The client application will be responsible for sending and receiving messages to and from the server. It will also display the status of messages and the online/offline status of users.

2 Server
The server application will be responsible for providing the services to the host and the host will eventually be hosting these services.

3 Host
The host application will host the SOAP web services and manage communication between clients. It will also handle user authentication and maintain user status information.

## Installation


```bash
step 1:
add connection string and create necessary tables
build service

step 2:
add service.dll as a reference in host

step 3:
start host

step 4:
add UsersService and MessagesService as service reference in client

step 5:
run client
```
## Schemas

Users
```bash
  CREATE TABLE [dbo].[Users] (
    [username]  VARCHAR (50) NOT NULL,
    [password]  VARCHAR (50) NOT NULL,
    [last_seen] DATETIME     NOT NULL DEFAULT GETDATE(),
    [is_online] BIT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([username] ASC)
    );
```

Messages
```bash
 CREATE TABLE[dbo].[Messages]
    (
    [sender] VARCHAR (50)  NOT NULL,
    [receiver]        VARCHAR(50)  NOT NULL,
    [message_content] VARCHAR(200) NOT NULL,
    [message_id]      INT IDENTITY(1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED([message_id] ASC),
    FOREIGN KEY([receiver]) REFERENCES[dbo].[Users] ([username]),
    FOREIGN KEY([sender]) REFERENCES[dbo].[Users] ([username])
    );
```

## Screenshots

![Screenshot 2024-03-27 164813](https://github.com/unknown-29/chat-app-project/assets/107257619/be665026-46f3-4817-a715-c75f5d324f78)

![Screenshot 2024-03-27 164855](https://github.com/unknown-29/chat-app-project/assets/107257619/47240796-4921-4d6d-80a5-e6139b2fdf54)
![Screenshot 2024-03-27 1665750](https://github.com/unknown-29/chat-app-project/assets/107257619/eef9df8c-c3ef-43da-aca1-bfd893ce447e)
![Screenshot 2024-03-27 165055](https://github.com/unknown-29/chat-app-project/assets/107257619/6aafc7bf-ca8f-4f36-9f7d-71fcc2344075)
![Screenshot 2024-03-27 165158](https://github.com/unknown-29/chat-app-project/assets/107257619/1cbdbdf4-9cc6-4a1e-8b55-de4c658a3814)
![Screenshot 2024-03-27 165452](https://github.com/unknown-29/chat-app-project/assets/107257619/968fc84c-8766-49c1-bd9c-1806ce301791)


## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
