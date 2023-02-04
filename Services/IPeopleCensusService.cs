using PeopleCensus.Models;

namespace PeopleCensus.Services
{
	public interface IPeopleCensusService
	{
		List<People> Get();
		People Get(string id);
		People Create(People person);
		void Update (string id, People person);
		void Remove(string id);
	}
}
