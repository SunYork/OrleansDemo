using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orleans;
using Orleans.Runtime.Configuration;
using System.Net;
using OrleansWebInterface;

namespace OrleansWebClient
{
	public partial class Index : System.Web.UI.Page
	{
		protected async void Page_Load(object sender, EventArgs e)
		{
			var clientConfig = new ClientConfiguration();
			clientConfig.Gateways.Add(new System.Net.IPEndPoint(IPAddress.Loopback, 30000));
			GrainClient.Initialize(clientConfig);

			var user = GrainClient.GrainFactory.GetGrain<IUserService>(1);
			var item = await user.GetUserById(1);
			var list = await user.GetUserList();

			try
			{
				var i = await user.GetUserByName("");
			}
			catch(Exception ex)
			{

			}
		}
	}
}