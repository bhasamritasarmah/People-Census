using MongoDB.Driver;
using PeopleCensus.Models;

namespace PeopleCensus.Services
{
	public class PeopleCensusService : IPeopleCensusService
	{
		private readonly IMongoCollection<People> _people;

		public PeopleCensusService(IPeopleCensusDatabaseSettings settings, IMongoClient mongoClient) 
		{
			var database = mongoClient.GetDatabase(settings.DatabaseName);
			_people = database.GetCollection<People>(settings.CollectionName);
		}

		public People Create(People person)
		{
			_people.InsertOne(person);
			return person;
		}

		public List<People> Get()
		{
			return _people.Find(student => true).ToList();
		}

		public People Get(string id)
		{
			return _people.Find(student => student.Id == id).FirstOrDefault();
		}

		public void Remove(string id)
		{
			_people.DeleteOne(student => student.Id == id);
		}

		public void Update(string id, People person)
		{
			_people.ReplaceOne(student => student.Id == id, person);
		}
	}
}
