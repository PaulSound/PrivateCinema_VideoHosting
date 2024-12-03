using PrivateCinema_VideoHosting.Data;
using PrivateCinema_VideoHosting.Hubs;

namespace PrivateCinema_VideoHosting.Services
{
    public class DatabaseService
    {
        private readonly ApplicationContext _dbContext;
        public DatabaseService(ApplicationContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public  List<CinemaHubItem> GetMovieListByUserId(string userId)
        {
            int id=int.Parse(userId);
            List<CinemaHubItem>movieList = _dbContext._videoList.Where(x=>x.userId==id).Select(ToHubItemModel).ToList();
            if (movieList != null)
                return movieList;
            return null;
        }
        
        private CinemaHubItem ToHubItemModel(Video video)
        {
            return new CinemaHubItem { FileName = Path.GetFileNameWithoutExtension(video.FileName), FilePath = video.FilePath };
        }
    }
}
