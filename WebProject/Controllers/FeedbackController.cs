using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Interfaces.Core.Services;
using WebProject.Models.Feedback;

namespace WebProject.Controllers
{
    public class FeedbackController : Controller
    {
        private static IPublicFeedbackService _publicFeedbackService;

        public FeedbackController(IPublicFeedbackService publicFeedbackService)
        {
            _publicFeedbackService = publicFeedbackService;
        }

        public async Task<ActionResult> Feedback()
        {
            var model = await GetFeedbackModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(FeedbackModel model)
        {
            if (!ModelState.IsValid)
            {
                var originalModel = await GetFeedbackModelAsync();
                return View("Feedback", originalModel);
            }

            await _publicFeedbackService.SaveMessageAsync(model.Message, model.SenderName);

            return RedirectToAction("Feedback");
        }

        private async Task<FeedbackModel> GetFeedbackModelAsync()
        {
            var model = new FeedbackModel();
            var publicMessages = await _publicFeedbackService.GetPublicChatMessagesAsync();
            var messages = publicMessages.Select(message => new FeedbackMessageModel
                {
                    Message = message.Message,
                    Author = message.SenderName,
                    CreateDateTime = message.CreateDateTime
                })
                .ToList();

            model.MessageModels.AddRange(messages);
            return model;
        }
    }
}