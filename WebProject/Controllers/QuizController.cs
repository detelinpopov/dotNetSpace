using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Interfaces.Core.Services;
using Interfaces.Sql.Entities;
using Shared.Entities;
using WebProject.Enums;
using WebProject.Models.Quiz;

namespace WebProject.Controllers
{
    public class QuizController : Controller
    {
        private const int TotalQuestionsCount = 10;

        private readonly IQuestionService _questionService;

        public QuizController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public static IList<int> AnsweredQuestionsIds
        {
            get
            {
                var key = SessionKeys.AnsweredQuestionsIdsKey;
                if (System.Web.HttpContext.Current.Session[key] == null)
                {
                    IList<int> answeredQuestionsIds = new List<int>();
                    System.Web.HttpContext.Current.Session[key] = answeredQuestionsIds;
                    return answeredQuestionsIds;
                }
                return (IList<int>) System.Web.HttpContext.Current.Session[key];
            }
        }

        public static DateTime? StartTime
        {
            get { return (DateTime?) System.Web.HttpContext.Current.Session[SessionKeys.StartTimeKey]; }
            set { System.Web.HttpContext.Current.Session[SessionKeys.StartTimeKey] = value; }
        }

        public static QuestionCategory QuestionCategory
        {
            get { return (QuestionCategory) System.Web.HttpContext.Current.Session[SessionKeys.QuestionCategoryKey]; }
            set { System.Web.HttpContext.Current.Session[SessionKeys.QuestionCategoryKey] = value; }
        }

        public static int NumberOfCorrectAnswers
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[SessionKeys.NumberOfCorrectAnswersKey] == null)
                {
                    System.Web.HttpContext.Current.Session[SessionKeys.NumberOfCorrectAnswersKey] = 0;
                }
                return (int) System.Web.HttpContext.Current.Session[SessionKeys.NumberOfCorrectAnswersKey];
            }
            set { System.Web.HttpContext.Current.Session[SessionKeys.NumberOfCorrectAnswersKey] = value; }
        }

        public static int NumberOfAnsweredQuestions
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[SessionKeys.NumberOfAnsweredQuestionsKey] == null)
                {
                    System.Web.HttpContext.Current.Session[SessionKeys.NumberOfAnsweredQuestionsKey] = 0;
                }
                return (int) System.Web.HttpContext.Current.Session[SessionKeys.NumberOfAnsweredQuestionsKey];
            }
            set { System.Web.HttpContext.Current.Session[SessionKeys.NumberOfAnsweredQuestionsKey] = value; }
        }

        public static IList<int> CorrectAnswersIds
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[SessionKeys.CorrectAnswersIdsKey] == null)
                {
                    System.Web.HttpContext.Current.Session[SessionKeys.CorrectAnswersIdsKey] = new List<int>();
                }
                return (List<int>)System.Web.HttpContext.Current.Session[SessionKeys.CorrectAnswersIdsKey];
            }
            set { System.Web.HttpContext.Current.Session[SessionKeys.CorrectAnswersIdsKey] = value; }
        }

        public ActionResult CategoriesDetails()
        {
            return View("Categories/CategoriesDetails");
        }

        public ActionResult CategoryDetails(QuestionCategory category)
        {
            return View("Categories/CategoryDetails" + category);
        }

        [HttpPost]
        public JsonResult CheckAnswers(ResponseModel responseModel)
        {
            var correctAnswers = IsResponseCorrect(responseModel);
            var verifyAnswerModel = new VerifyAnswerModel {AnswerResult = correctAnswers ? AnswerResult.Correct.ToString() : AnswerResult.Wrong.ToString()};
            var correctAnswersIds = CorrectAnswersIds;
            verifyAnswerModel.CorrectAnswersIds = correctAnswersIds.Select(i => i.ToString()).ToArray();

            return Json(verifyAnswerModel);
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int id)
        {
            var question = await _questionService.FindAsync(id);
            var model = CreateQuestionModel(question);
            return View("Question", model);
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Question(ResponseModel responseModel, QuestionCategory questionCategory)
        {
            if (NumberOfAnsweredQuestions == 0)
            {
                ResetQuizSettings();
                QuestionCategory = questionCategory;
            }

            if (NumberOfAnsweredQuestions == TotalQuestionsCount)
            {
                return RedirectToAction("QuizCompleted", "Quiz");
            }

            if (responseModel != null)
            {
                 IsResponseCorrect(responseModel);
            }

            var question = await _questionService.FindRandomQuestionAsync(questionCategory, AnsweredQuestionsIds);
            if (question == null)
            {
                return RedirectToAction("QuizCompleted", "Quiz");
            }

            CorrectAnswersIds = question.Answers.Where(a => a.IsCorrect).Select(a => a.Id).ToList();
            var model = CreateQuestionModel(question);
            return View("Question", model);
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
            NumberOfAnsweredQuestions = 0;
            return View(model);
        }

        private QuestionModel CreateQuestionModel(IQuestion question)
        {
            var model = new QuestionModel
            {
                Id = question.Id,
                Text = question.Text,
                Image = question.Image,
                Number = AnsweredQuestionsIds.Count + 1,
                SelectedQuestionCategory = question.Category
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

        private bool IsResponseCorrect(ResponseModel responseModel)
        {
            var correctAnswers = CorrectAnswersIds.SequenceEqual(responseModel.AnswerIds);
            if (correctAnswers && !AnsweredQuestionsIds.Contains(responseModel.QuestionId))
            {
                NumberOfCorrectAnswers++;
            }
            if (responseModel.QuestionId > 0 && !AnsweredQuestionsIds.Contains(responseModel.QuestionId))
            {
                NumberOfAnsweredQuestions++;
                AnsweredQuestionsIds.Add(responseModel.QuestionId);
            }

            return correctAnswers;
        }

        private void ResetQuizSettings()
        {
            StartTime = DateTime.Now;
            AnsweredQuestionsIds.Clear();
            NumberOfAnsweredQuestions = 0;
            NumberOfCorrectAnswers = 0;
        }

        private static class SessionKeys
        {
            public static readonly string AnsweredQuestionsIdsKey = "AnsweredQuestionsIds";

            public static readonly string StartTimeKey = "StartTime";

            public static readonly string QuestionCategoryKey = "QuestionCategory";

            public static readonly string NumberOfCorrectAnswersKey = "NumberOfCorrectAnswers";

            public static readonly string NumberOfAnsweredQuestionsKey = "NumberOfAnsweredQuestions";

            public static readonly string CorrectAnswersIdsKey = "CorrectAnswersIds";
        }
    }
}