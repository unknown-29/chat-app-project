using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Host
{
    public partial class Host : Form
    {
        public Host()
        {
            InitializeComponent();
        }

        private void Host_Load(object sender, EventArgs e)
        {
            /* need to have access to ports 8733 and 8734
            run command prompt or powershell in administrator mode and run command
            $ whoami
            abc\xyz
            $ netsh http add urlacl url="http://+:8733/" user="abc\xyz"
            */
            Uri httpaU = new Uri("http://localhost:8733/Services/UsersService");
            Uri httpaM = new Uri("http://localhost:8734/Services/MessagesService");
            try
            {
                ServiceHost shU = new ServiceHost(typeof(UsersService), httpaU);
                ServiceHost shM = new ServiceHost(typeof(MessagesService), httpaM);
                
                WSHttpBinding httpbU = new WSHttpBinding();
                WSHttpBinding httpbM = new WSHttpBinding();
                
                shU.Description.Behaviors.Add(new ServiceMetadataBehavior());
                shM.Description.Behaviors.Add(new ServiceMetadataBehavior());
                
                shU.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
                shM.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
                
                shU.AddServiceEndpoint(typeof(IUsersService), httpbU, httpaU);
                shM.AddServiceEndpoint(typeof(IMessagesService), httpbM, httpaM);

                shU.Open();
                shM.Open();

                label1.Text = "service is running ...";
            } catch (Exception ex) { Console.Error.WriteLine(ex.Message); }
        }
    }
}
