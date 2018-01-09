using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Shared.Entities;

namespace WebProject.Models.Quiz
{
    public class QuestionModel
    {
        public IList<AnswerModel> Answers { get; } = new List<AnswerModel>();

        public bool HasSingleAnswer { get; set; }

        public int Id { get; set; }

        public byte[] Image { get; set; }

        public int Number { get; set; }

        public IList<SelectListItem> QuestionCategories
        {
            get
            {
                IList<SelectListItem> categories = new List<SelectListItem>();
                foreach (var category in Enum.GetValues(typeof(QuestionCategory)))
                {
                    var item = new SelectListItem
                    {
                        Value = category.ToString(),
                        Text = category.ToString()
                    };
                    categories.Add(item);
                }

                return categories;
            }
        }

        public string SelectedQuestionCategory { get; set; }

        public string Text { get; set; }

        public int TotalQuestionsCount { get; set; } = 20;
    }
}