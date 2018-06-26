using System;
namespace StudentVerifierService
{
	[Serializable]
    public class Result
    {
		public string regNum;
		public string degree;
		public float cgpa;
		public int passYear;
		public int semester;
		public int totalSemester;
        public Result()
        {
			
        }



    }
}
