using EnglishResourceUI.Constants;
using EnglishResourceUI.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishResourceUI.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class TopicController : Controller
    {
        private readonly ITopicRepository _topicRepo;

        public TopicController(ITopicRepository topicRepo)
        {
            _topicRepo = topicRepo;
        }

        public async Task<IActionResult> Index()
        {
            var topics = await _topicRepo.GetTopics();
            return View(topics);
        }

        public IActionResult AddTopic()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddTopic(TopicDTO topic)
        {
            if (!ModelState.IsValid)
            {
                return View(topic);
            }
            try
            {
                var topicToAdd = new Topic { TopicName = topic.TopicName, Id = topic.Id };
                await _topicRepo.AddTopic(topicToAdd);
                TempData["successMessage"] = "Topic added successfully";
                return RedirectToAction(nameof(AddTopic));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Topic was not added";
                return View(topic);
            }
        }

        public async Task<IActionResult> UpdateTopic(int id)
        {
            var topic = await _topicRepo.GetTopicById(id);
            if (topic == null)
            {
                throw new InvalidOperationException($"Topic with id: {id} was not found");
            }
            var topicToUpdate = new TopicDTO
            {
                Id = topic.Id,
                TopicName = topic.TopicName
            };
            return View(topicToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTopic(TopicDTO topicToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(topicToUpdate);
            }
            try
            {
                var topic = new Topic {TopicName = topicToUpdate.TopicName, Id = topicToUpdate.Id };
                await _topicRepo.UpdateTopic(topic);
                TempData["successMessage"] = "Topic was updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Topic could not be updated";
                return View(topicToUpdate);
            }
        }

        public async Task<IActionResult> DeleteTopic(int id)
        {
            var topic = await _topicRepo.GetTopicById(id);
            if (topic is null)
                throw new InvalidOperationException($"Topic with id: {id} was not found");
            await _topicRepo.DeleteTopic(topic);
            return RedirectToAction(nameof(Index));
        }
    }
}
