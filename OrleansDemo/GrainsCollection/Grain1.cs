using System.Threading.Tasks;
using Orleans;
using GrainInterfaces;
using System;

namespace GrainsCollection
{
	/// <summary>
	/// Grain implementation class Grain1.
	/// </summary>
	public class Grain1 : Grain, IGrain1
	{
		public Task<string> SayHello()
		{
			return Task.FromResult("Hello World!");
		}
	}
}
