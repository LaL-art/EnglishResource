using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EnglishResourceUI.Controllers
{
    [Authorize]
    public class FavouritesController : Controller
    {
        private readonly IFavouritesRepository _favouritesRepo;

        public FavouritesController(IFavouritesRepository favouritesRepo)
        {
            _favouritesRepo = favouritesRepo;
        }
        public async Task <IActionResult> AddItem(int studyFileId, int redirect=0)
        {
            var favouritesCount = await _favouritesRepo.AddItem(studyFileId);
            if (redirect > 0)
                return Ok(favouritesCount);
            return RedirectToAction("GetUserFavourites");
        }
        public async Task<IActionResult> RemoveItem(int studyFileId)
        {
            var favouritesCount = await _favouritesRepo.RemoveItem(studyFileId);
            return RedirectToAction("GetUserFavourites");
        }
        public async Task<IActionResult> GetUserFavourites()
        {
            var favourite = await _favouritesRepo.GetUserFavourites();
            return View(favourite);
        }
        public async Task<IActionResult> GetTotalItemInFavourites(int studyFileId)
        {
            int favouritesItem = await _favouritesRepo.GetFavouritesItemCount();
            return Ok(favouritesItem);
        }
    }
}
