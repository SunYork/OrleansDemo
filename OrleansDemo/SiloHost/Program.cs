using GrainsCollection;
using GrainInterfaces;
using Orleans;
using System;
using System.Threading.Tasks;

namespace SiloHost
{
	/// <summary>
	/// Orleans test silo host
	/// </summary>
	public class Program
	{
		static void Main(string[] args)
		{
			AppDomain hostDomain = AppDomain.CreateDomain("OrleansHost", null, new AppDomainSetup
			{
				AppDomainInitializer = InitSilo,
				AppDomainInitializerArguments = args,
			});

			Orleans.GrainClient.Initialize("DevTestClientConfiguration.xml");

			var friend = GrainClient.GrainFactory.GetGrain<IGrain1>(0);
			Console.WriteLine("\n\n{0}\n\n", friend.SayHello().Result);

			Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
			Console.ReadLine();

			hostDomain.DoCallBack(ShutdownSilo);
		}

		static void InitSilo(string[] args)
		{
			hostWrapper = new OrleansHostWrapper(args);

			if (!hostWrapper.Run())
			{
				Console.Error.WriteLine("Failed to initialize Orleans silo");
			}
		}

		static void ShutdownSilo()
		{
			if (hostWrapper != null)
			{
				hostWrapper.Dispose();
				GC.SuppressFinalize(hostWrapper);
			}
		}

		private static OrleansHostWrapper hostWrapper;
	}
}
