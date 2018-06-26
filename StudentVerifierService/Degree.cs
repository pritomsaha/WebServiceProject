using System;
namespace StudentVerifierService
{
	[Serializable]
    public class Degree
    {
		public int id;
		public string degree;

		public Degree()
		{
		}

		public Degree(int id, string degree)
        {
			this.id = id;
			this.degree = degree;
        }
    }
}
