using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace OrleansWebInterface
{
	public interface IUserService : IGrainWithIntegerKey
	{
		Task<User> GetUserById(int id);
		Task<List<User>> GetUserList();
		Task<User> GetUserByName(string userName);
	}
}
