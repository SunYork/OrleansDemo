using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace OrleansWebHost
{
	public class Global : System.Web.HttpApplication
	{
		protected static SiloHost siloHost;

		protected void Application_Start(object sender, EventArgs e)
		{
			siloHost = new SiloHost(System.Net.Dns.GetHostName());
			siloHost.ConfigFileName = Server.MapPath("~/OrleansConfiguration.xml");

			siloHost.InitializeOrleansSilo();
			if (!siloHost.StartOrleansSilo())
			{
				throw new SystemException("Failed to start Orleans silo");
			}
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{
			if (siloHost != null)
			{
				siloHost.Dispose();
				GC.SuppressFinalize(siloHost);
				siloHost = null;
			}
		}
	}
}