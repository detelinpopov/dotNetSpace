using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Interfaces.Core.Services;
using WebProject.Enums;
using WebProject.Models.Quiz;

namespace WebProject.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuizController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public static IList<int> AnsweredQuestionsIds
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["AnsweredQuestionsIds"] == null)
                {
                    IList<int> answeredQuestionsIds = new List<int>();
                    System.Web.HttpContext.Current.Session["AnsweredQuestionsIds"] = answeredQuestionsIds;
                    return answeredQuestionsIds;
                }
                return (IList<int>) System.Web.HttpContext.Current.Session["AnsweredQuestionsIds"];
            }
        }

        public static int NumberOfCorrectAnswers
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["NumberOfCorrectAnswers"] == null)
                {
                    System.Web.HttpContext.Current.Session["NumberOfCorrectAnswers"] = 0;
                }
                return (int) System.Web.HttpContext.Current.Session["NumberOfCorrectAnswers"];
            }
            set { System.Web.HttpContext.Current.Session["NumberOfCorrectAnswers"] = value; }
        }

        [HttpPost]
        public async Task<JsonResult> CheckAnswers(ResponseModel responseModel)
        {
            if (responseModel.AnswerIds.Count(i => i > 0) == 0)
            {
                return Json(AnswerResult.NoAnswerSelected.ToString(), JsonRequestBehavior.AllowGet);
            }

            var correctAnswers = await IsResponseCorrectAsync(responseModel);
            var result = correctAnswers ? AnswerResult.Correct.ToString() : AnswerResult.Wrong.ToString();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetNextQuestion(ResponseModel responseModel)
        {
            await IsResponseCorrectAsync(responseModel);

            var question = await _questionService.FindRandomQuestionAsync(AnsweredQuestionsIds);
            if (question == null)
            {
                return RedirectToAction("QuizCompleted", "Quiz");
            }
            return RedirectToAction("Question", new {id = question.Id});
        }

        public async Task<ActionResult> Question(int id)
        {
            if (AnsweredQuestionsIds.Contains(id))
            {
                return View("AnsweredQuestion");
            }

            var question = await _questionService.FindAsync(id);
            var model = new QuestionModel
            {
                Id = question.Id,
                Text = question.Text,
                Image = question.Image,
                IsAnswered = AnsweredQuestionsIds.Contains(id)
            };
            foreach (var answer in question.Answers)
            {
                var answerModel = new AnswerModel
                {
                    Id = answer.Id,
                    Text = answer.Text,
                    IsCorrect = answer.IsCorrect
                };
                model.Answers.Add(answerModel);
            }
            return View(model);
        }

        public ActionResult QuizCompleted()
        {
            var model = new QuizCompletedModel {NumberOfCorrectAnswers = NumberOfCorrectAnswers};
            return View(model);
        }

        private async Task<bool> IsResponseCorrectAsync(ResponseModel responseModel)
        {
            var correctAnswers = await _questionService.CheckAnswersAsync(responseModel.QuestionId, responseModel.AnswerIds);
            if (correctAnswers && !AnsweredQuestionsIds.Contains(responseModel.QuestionId))
            {
                NumberOfCorrectAnswers++;
            }
            AnsweredQuestionsIds.Add(responseModel.QuestionId);
            return correctAnswers;
        }
    }
}