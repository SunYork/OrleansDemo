using System.Threading.Tasks;
using Orleans;
using GrainInterfaces;
using System;

namespace GrainCollection
{
	public class HelloGrain : Grain, IHello
	{
		public Task<string> SayHello(string msg)
		{
			return Task.FromResult($"You said {msg}, I say: hello!");
		}
	}
}
