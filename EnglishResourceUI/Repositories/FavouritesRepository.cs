using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using EnglishResourceUI.Models;
using EnglishResourceUI.Data;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks.Dataflow;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnglishResourceUI.Repositories
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FavouritesRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddItem(int studyFileId)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not logged-in");
                }
                var favourite = await GetFavourite(userId);

                if (favourite is null)
                {
                    favourite = new Favourites
                    {
                        UserId = userId,
                    };
                    _db.Favoritess.Add(favourite);
                }
                _db.SaveChanges();
                //favourites detail section
                var favouritesItem = _db.FavoritesDetails
                                     .FirstOrDefault(a => a.FavouritesId == favourite.Id && a.StudyFileId == studyFileId);
                if (favouritesItem is null)
                {
                    var studyFile = _db.Favoritess.Find(studyFileId);
                    favouritesItem = new FavouritesDetail
                    {
                        StudyFileId = studyFileId, 
                        FavouritesId = favourite.Id

                    };
                   
                }
                _db.FavoritesDetails.Add(favouritesItem);
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            { 
            }
            var favouriteItemsCount = GetFavouritesItemCount(userId);
            return await favouriteItemsCount;
        }

        public async Task<int> RemoveItem(int studyFileId)
        {
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not logged-in");
                }
                var favourite = await GetFavourite(userId);

                if (favourite is null)
                {
                    throw new Exception("Invalid Favourites");
                }
                //favourites detail section
                var favouritesItem = _db.FavoritesDetails
                                    .FirstOrDefault(a => a.FavouritesId == favourite.Id && a.StudyFileId == studyFileId);
                if (favouritesItem is null)
                {
                    throw new Exception("No favourites were found");
                }
                else
                {
                    _db.FavoritesDetails.Remove(favouritesItem);
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            var favouriteItemsCount = GetFavouritesItemCount(userId);
            return await favouriteItemsCount;
        }

        public async Task<Favourites> GetUserFavourites()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Invalid User Id");
            var favourite = await _db.Favoritess
                               .Include(a => a.FavouritesDetails)
                               .ThenInclude(a => a.StudyFile)
                               .ThenInclude(a=>a.Level)
                               .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return favourite;
        }

        public async Task<Favourites> GetFavourite(string userId)
        {
            var favourite = await _db.Favoritess.FirstOrDefaultAsync(x => x.UserId == userId);
            return favourite;
        }

        public async Task<int> GetFavouritesItemCount(string userId="")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId =GetUserId();
            }
            var data = await (from favourite in _db.Favoritess
                              join favouritesDetail in _db.FavoritesDetails
                              on favourite.Id equals favouritesDetail.FavouritesId
                              where favourite.UserId == userId
                              select new { favouritesDetail.Id }
                               ).ToListAsync();
            return data.Count;
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
