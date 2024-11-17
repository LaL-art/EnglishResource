using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishResourceUI.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class LevelController : Controller
    {
        private readonly ILevelRepository _levelRepo;

        public LevelController(ILevelRepository levelRepo)
        {
            _levelRepo = levelRepo;
        }
        public async Task<IActionResult> Index()
        {
            var levels = await _levelRepo.GetLevels();
            return View(levels);
        }

        public IActionResult AddLevel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLevel(LevelDTO level)
        {
            if (!ModelState.IsValid)
            {
                return View(level);
            }
            try
            {
                var levelToAdd = new Level { CEFRLevel = level.CEFRLevel, Id = level.Id };
                await _levelRepo.AddLevel(levelToAdd);
                TempData["successMessage"] = "Level added successfully";
                return RedirectToAction(nameof(AddLevel));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Level was not added";
                return View(level);
            }
        }

        public async Task<IActionResult> UpdateLevel(int id)
        {
            var level = await _levelRepo.GetLevelById(id);
            if (level == null)
            {
                throw new InvalidOperationException($"Level with id: {id} was not found");
            }
            var levelToUpdate = new LevelDTO
            {
                Id = level.Id,
                CEFRLevel = level.CEFRLevel
            };
            return View(levelToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLevel(LevelDTO levelToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(levelToUpdate);
            }
            try
            {
                var level = new Level { CEFRLevel = levelToUpdate.CEFRLevel, Id = levelToUpdate.Id };
                await _levelRepo.UpdateLevel(level);
                TempData["successMessage"] = "Level was updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Level could not be updated";
                return View(levelToUpdate);
            }
        }

        public async Task<IActionResult> DeleteLevel(int id)
        {
            var level = await _levelRepo.GetLevelById(id);
            if (level is null)
                throw new InvalidOperationException($"Level with id: {id} was not found");
            await _levelRepo.DeleteLevel(level);
            return RedirectToAction(nameof(Index));
        }
    }
}

