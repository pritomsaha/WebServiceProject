using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace ApplicationService.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(FormCollection data)
		{
			string username = data["username"];
			string password = data["password"];
			AuthService.AuthService authService = new AuthService.AuthService();
			bool authStatus = authService.login(username, password);
			if (!authStatus)
			{
				TempData["error"] = "Username or Password is incorrect!";
				return Redirect(Url.Action("Index", "Home"));
			}
			Session["user"] = username;
			return Redirect(Url.Action("List", "Home"));
		}

		[HttpPost]
        public ActionResult Logout(FormCollection data)
        {

            return Redirect(Url.Action("Index", "Home"));
        }

		public ActionResult List()
        {
			if ((string)Session["user"] == null)
			{   
				return Redirect(Url.Action("Index", "Home"));
			}

			ApplicationService.ApplicationService applicationService = new ApplicationService.ApplicationService();
			ApplicationService.Application[] applications = applicationService.getApplications();
			Dictionary<string, string>[] dicts= new Dictionary<string, string>[applications.Length];
			for (int i = 0; i < applications.Length; i++)
			{
				Dictionary<string, string> dict = new Dictionary<string, string>();
				dict["id"] = applications[i].id.ToString();
				dict["regNum"] = applications[i].regNum;
				dict["email"] = applications[i].email;
				dict["degreeId"] = applications[i].degreeId.ToString();
				dict["date"] = applications[i].date;
				dicts[i] = dict;
			}
			ViewData["dicts"] = dicts;
			return View("List");
        }

		[HttpPost]
		public ActionResult GenerateTestimonial(FormCollection data)
		{
			if ((string)Session["user"] == null)
            {
                return Redirect(Url.Action("Index", "Home"));
            }

			string regNum = data["regNum"];
			int degreeId = System.Convert.ToInt32(data["degreeId"]);
			int appId = System.Convert.ToInt32(data["appId"]);

			StudentVerifierService.StudentVerifierService studentVerifier = new StudentVerifierService.StudentVerifierService();
			StudentVerifierService.Student student = studentVerifier.getStudentInfo(regNum);

			ResultService.ResultService resultService = new ResultService.ResultService();
			ResultService.Result result = resultService.getUpdatedResult(regNum, degreeId);

			ViewData["appId"] = appId;
			ViewData["name"] = student.name;
			ViewData["fatherName"] = student.fatherName;
			ViewData["motherName"] = student.motherName;
			ViewData["gender"] = student.gender;
			ViewData["regNum"] = student.regNum;
			ViewData["session"] = student.session;
			ViewData["degree"] = result.degree;
			ViewData["semester"] = result.semester;
			ViewData["totalSemester"] = result.totalSemester;
			ViewData["cgpa"] = result.cgpa;

			return View("Testimonial");
		}

		[HttpPost]
		public ActionResult Deliver(FormCollection data)
		{
			int appId = System.Convert.ToInt32(data["appId"]);
			ApplicationService.ApplicationService applicationService = new ApplicationService.ApplicationService();
			applicationService.makeDelivered(appId);
			return Redirect(Url.Action("List", "Home"));
		}


    }
}
