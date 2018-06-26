using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace TestimonialWebApp.Controllers
{
    public class HomeController : Controller
    {

		public ActionResult Index()
		{
			return View("Index");
        }

		[HttpPost]
		public ActionResult VerifyStudent(FormCollection data)
		{
			string submit = data["submit"];
			string regNum = data["regNum"];
			if (regNum == "")
            {
				TempData["error"] = "Registration Number is not valid!";
				return Redirect(Url.Action("Index", "Home"));
            }
			TestimonialWebApp.StudentVerifierService.StudentVerifierService studentVerifier = new StudentVerifierService.StudentVerifierService();

			if (studentVerifier.verifyStudent(regNum) == false)
			{
				TempData["error"] = "Registration Number is not valid!";
				return Redirect(Url.Action("Index", "Home"));
			}
           
			TempData["regNum"] = regNum;

			if (submit.Equals("Application Status"))
			{
				return Redirect(Url.Action("Status", "Home"));
			}
			else return Redirect(Url.Action("Apply", "Home"));


		}

		public ActionResult Status()
		{
			
			return View("Status");
		}

		[HttpPost]
		public ActionResult GetStatus(FormCollection data)
		{
			string regNum = data["regNum"];
			string transacNum = data["transacNum"];

			TempData["regNum"] = regNum;
			TempData["transacNum"] = transacNum;

			ApplicationService.ApplicationService applicationService = new ApplicationService.ApplicationService();
			var status = applicationService.getApplicationStatus(regNum, transacNum);
			int appId = status.Item1;
			bool approved = status.Item2;
			TempData["message"] = null;
			if (appId == -1)
			{
				TempData["message"] = "There is no application with given reg. number and transaction num!";
			}
			else 
			{
				if (approved == false)
				{
					TempData["message"] = "Application Number is: " + appId + " and testimonial is not deliverd yet."; 
				}
				else TempData["message"] = "Application Number is: " + appId + " and the testimonial was deliverd.";
			}
           
			return Redirect(Url.Action("Status", "Home"));
		}

		public ActionResult Apply()
        {
			TestimonialWebApp.ResultService.ResultService resultService = new ResultService.ResultService();
			TestimonialWebApp.ResultService.Degree[] degreeList = resultService.getDegreeList();

			Dictionary<int, string> degrees = new Dictionary<int, string>();

			for (int i = 0; i < degreeList.Length; i++)
			{
				degrees[degreeList[i].id] = degreeList[i].degree;
			}

			ViewData["dict"] = degrees;

			return View("Apply");
        }

		[HttpPost]
		public ActionResult Submit(FormCollection data)
		{
			string regNum = data["regNum"];
			string transacNum = data["transacNum"];
			int degreeId = Int32.Parse(data["degree"]);
			string email = data["email"];
			TempData["regNum"] = regNum;

			TestimonialWebApp.ResultService.ResultService resultService = new ResultService.ResultService();
            bool checkResult = resultService.checkStudentInResult(regNum, degreeId);

            if (checkResult == false)
            {
                TempData["error_program"] = "There is no result under the selected degree program for the student";
                return Redirect(Url.Action("Apply", "Home"));
            }

			TestimonialWebApp.PaymentService.PaymentService paymentService = new PaymentService.PaymentService();
            float transAmount = (float)paymentService.getPayment(transacNum);

			if (transAmount < 100.00)
			{
				TempData["error"] = "Given Transaction number is not applicable!";
				return Redirect(Url.Action("Apply", "Home"));
			}

			TestimonialWebApp.ApplicationService.ApplicationService applicationService = new ApplicationService.ApplicationService();
			long appId = (long)applicationService.apply(regNum, transacNum, degreeId, email);

			if (appId==-1)
			{
				TempData["error"] = "There is already an application with given transaction number!";
                return Redirect(Url.Action("Apply", "Home"));
			}


			TempData["success"] = "Your Application for testimonial has been successfully sent and your application id is "+ appId +".";
			return Redirect(Url.Action("Index", "Home"));
		}
    }
}
