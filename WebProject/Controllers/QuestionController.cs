﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Interfaces.Core.Services;
using Interfaces.Sql.Entities;
using WebProject.Models.Quiz;

namespace WebProject.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuestionModel model, HttpPostedFileBase uploadImage)
        {
            var question = _questionService.CreateQuestion();
            question.Text = model.Text;
            if (uploadImage != null && uploadImage.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    var imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    question.Image = imageData;
                }
            }
            IList<IAnswer> answers = new List<IAnswer>();
            foreach (var answerModel in model.Answers)
            {
                var answer = _questionService.CreateAnswer();
                answer.Text = answerModel.Text;
                answer.IsCorrect = answerModel.IsCorrect;
                answers.Add(answer);
            }
            question.Answers = answers;
            await _questionService.SaveAsync(question);
            return RedirectToAction("Question", "Quiz", new {id = question.Id});
        }
    }
}