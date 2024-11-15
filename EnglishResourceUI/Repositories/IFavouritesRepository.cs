namespace EnglishResourceUI.Repositories
{
    public interface IFavouritesRepository
    {
        Task<int> AddItem(int studyFileId);
        Task<int> RemoveItem(int studyFileId);
        Task<Favourites> GetUserFavourites();
        Task<int> GetFavouritesItemCount(string userId = "");
        Task<Favourites> GetFavourite(string userId);
    }
}
