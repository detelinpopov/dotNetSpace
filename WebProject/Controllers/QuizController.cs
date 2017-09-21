using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Interfaces.Core.Services;
using Interfaces.Sql.Entities;
using WebProject.Enums;
using WebProject.Models.Quiz;

namespace WebProject.Controllers
{
    public class QuizController : Controller
    {
        private const int TotalQuestionsCount = 20;

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

        public static DateTime? StartTime
        {
            get { return (DateTime?) System.Web.HttpContext.Current.Session["StartTime"]; }
            set { System.Web.HttpContext.Current.Session["StartTime"] = value; }
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

        public static int NumberOfAnsweredQuestions
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["NumberOfAnsweredQuestions"] == null)
                {
                    System.Web.HttpContext.Current.Session["NumberOfAnsweredQuestions"] = 0;
                }
                return (int) System.Web.HttpContext.Current.Session["NumberOfAnsweredQuestions"];
            }
            set { System.Web.HttpContext.Current.Session["NumberOfAnsweredQuestions"] = value; }
        }

        [HttpPost]
        public async Task<JsonResult> CheckAnswers(ResponseModel responseModel)
        {
            var correctAnswers = await IsResponseCorrectAsync(responseModel);
            var verifyAnswerModel = new VerifyAnswerModel {AnswerResult = correctAnswers ? AnswerResult.Correct.ToString() : AnswerResult.Wrong.ToString()};
            var correctAnswersIds = await _questionService.GetCorrectAnswersIdsAsync(responseModel.QuestionId);
            verifyAnswerModel.CorrectAnswersIds = correctAnswersIds.Select(i => i.ToString()).ToArray();

            return Json(verifyAnswerModel);
        }

        public async Task<ActionResult> GetNextQuestion(ResponseModel responseModel)
        {
            if (StartTime == null)
            {
                StartTime = DateTime.Now;
            }
            if (NumberOfAnsweredQuestions == TotalQuestionsCount)
            {
                return RedirectToAction("QuizCompleted", "Quiz");
            }

            await IsResponseCorrectAsync(responseModel);

            var question = await _questionService.FindRandomQuestionAsync(AnsweredQuestionsIds);
            if (question == null)
            {
                return RedirectToAction("QuizCompleted", "Quiz");
            }

            NumberOfAnsweredQuestions++;
            var model = CreateQuestionModel(question);
            return View("Question", model);
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Question(int id)
        {
            if (StartTime == null)
            {
                StartTime = DateTime.Now;
            }
            if (AnsweredQuestionsIds.Contains(id))
            {
                return View("AnsweredQuestion");
            }

            var question = await _questionService.FindAsync(id);
            var model = CreateQuestionModel(question);
            return View(model);
        }

        public ActionResult QuizCompleted()
        {
            var timeSpentText = string.Empty;
            var timeSpent = DateTime.Now - StartTime;
            if (timeSpent != null)
            {
                var plural = timeSpent.Value.Minutes == 1 ? string.Empty : "s";
                timeSpentText = $"{timeSpent.Value.Minutes} minute{plural}  and {timeSpent.Value.Seconds} seconds";
            }
            var model = new QuizCompletedModel {NumberOfCorrectAnswers = NumberOfCorrectAnswers, TimeSpentText = timeSpentText, TotalQuestionsCount = TotalQuestionsCount};
            return View(model);
        }

        public async Task<ActionResult> StartTest()
        {
            StartTime = DateTime.Now;
            AnsweredQuestionsIds.Clear();
            NumberOfAnsweredQuestions = 0;
            NumberOfCorrectAnswers = 0;
            var question = await _questionService.FindRandomQuestionAsync(AnsweredQuestionsIds);
            var model = CreateQuestionModel(question);
            return View("Question", model);
        }

        private QuestionModel CreateQuestionModel(IQuestion question)
        {
            var model = new QuestionModel
            {
                Id = question.Id,
                Text = question.Text,
                Image = question.Image,
                Number = AnsweredQuestionsIds.Count + 1
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
            return model;
        }

        private async Task<bool> IsResponseCorrectAsync(ResponseModel responseModel)
        {
            var correctAnswers = await _questionService.CheckAnswersAsync(responseModel.QuestionId, responseModel.AnswerIds);
            if (correctAnswers && !AnsweredQuestionsIds.Contains(responseModel.QuestionId))
            {
                NumberOfCorrectAnswers++;
            }
            if (responseModel.QuestionId > 0 && !AnsweredQuestionsIds.Contains(responseModel.QuestionId))
            {
                AnsweredQuestionsIds.Add(responseModel.QuestionId);
            }
            return correctAnswers;
        }
    }
}