using GrainInterfaces;
using Orleans;
using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
	class Program
	{
		static SiloHost siloHost;

		static void Main(string[] args)
		{
			AppDomain hostDomain = AppDomain.CreateDomain("OrleansHost", null, new AppDomainSetup()
			{
				AppDomainInitializer = InitSilo
			});

			DoSomeClientWork();

			Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
			Console.ReadLine();

			hostDomain.DoCallBack(ShutdownSilo);
		}

		static void DoSomeClientWork()
		{
			var clientconfig = new Orleans.Runtime.Configuration.ClientConfiguration();
			clientconfig.Gateways.Add(new IPEndPoint(IPAddress.Loopback, 30000));

			GrainClient.Initialize(clientconfig);

			var friend = GrainClient.GrainFactory.GetGrain<IHello>(0);
			var result = friend.SayHello("Goodbye").Result;
			Console.WriteLine(result);

		}

		static void InitSilo(string[] args)
		{
			siloHost = new SiloHost(System.Net.Dns.GetHostName());
			siloHost.ConfigFileName = "OrleansConfiguration.xml";

			siloHost.InitializeOrleansSilo();
			var startedok = siloHost.StartOrleansSilo();
			if (!startedok)
				throw new SystemException(String.Format("Failed to start Orleans silo '{0}' as a {1} node", siloHost.Name, siloHost.Type));
		}

		static void ShutdownSilo()
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
