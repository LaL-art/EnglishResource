using EnglishResourceUI.Models;
using EnglishResourceUI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EnglishResourceUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm="", int levelId = 0)
        {
            IEnumerable<StudyFile> studyFiles = await _homeRepository.DisplayFiles(sterm, levelId);
            IEnumerable<Level> levels = await _homeRepository.Levels();
            StudyFileDisplayModel studyFileModel = new StudyFileDisplayModel
            {
                StudyFiles = studyFiles,
                Levels = levels     
            };
            return View(studyFileModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
