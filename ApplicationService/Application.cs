using System;
namespace ApplicationService
{
	[Serializable]
	public class Application
	{
		public int id;
        public string regNum;
        public int degreeId;
		public string email;
		public string date;
        public bool approved;

		public Application() { }

		public Application(int id, string regNum, int degreeId, string email, string date, bool approved)
		{
			this.id = id;
			this.regNum = regNum;
			this.degreeId = degreeId;
			this.email = email;
			this.date = date;
			this.approved = approved;
		}

    }
}
