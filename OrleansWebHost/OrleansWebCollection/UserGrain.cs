using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using OrleansWebInterface;

namespace OrleansWebCollection
{
	public class UserGrain : Grain, IUserService
	{
		public Task<User> GetUserById(int id)
		{
			return Task.FromResult(new User
			{
				UserId = id,
				UserName = $"测试{id}",
				Password = "****",
				RealName = $"真实姓名{id}"
			});
		}

		public Task<User> GetUserByName(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				throw new ArgumentNullException(nameof(userName));
			}

			return Task.FromResult(new User
			{

				UserId = userName.GetHashCode(),
				UserName = userName,
				Password = "****",
				RealName = "真实姓名"
			});
		}

		public Task<List<User>> GetUserList()
		{
			return Task.FromResult(new List<User>
			{
				new User
				{
					UserId = 1,
					UserName = "1",
					Password = "***",
					RealName = "1"
				},
				new User
				{
					UserId = 2,
					UserName = "2",
					Password = "***",
					RealName = "2"
				}
			});
		}
	}
}
