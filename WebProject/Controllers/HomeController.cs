﻿using System.Threading.Tasks;
using System.Web.Mvc;
using Interfaces.Core.Services;
using WebProject.Models.Quiz;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public HomeController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Contact(FeedbackModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var feedback = _feedbackService.CreateFeedback();
            feedback.Email = model.Email;
            feedback.Text = model.Text;
            await _feedbackService.SaveAsync(feedback);
            return RedirectToAction("Index", "Quiz");
        }
    }
}