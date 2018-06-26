using System;
namespace StudentVerifierService
{
	[Serializable]
    public class Student
    {

		public string regNum;
		public string name;
		public string gender;
		public string fatherName;
		public string motherName;
		public string session;
		public string instOrDept;

		public Student()
        {
        }

    }
}
