using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Identity.Client;
using PrivateCinema_VideoHosting.Data;
using PrivateCinema_VideoHosting.Services;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;
namespace PrivateCinema_VideoHosting.Hubs
{
    public class CinemaHub : Hub
    {
        public readonly DatabaseService _databaseService;
        public static List<CinemaHubItem> movieList = new List<CinemaHubItem>();

        public CinemaHub(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.UserIdentifier != null)
            {
                var hubUser = new HubUser()
                {
                    Name = Context.User.Identity.Name,
                    UserIdentifier = Context.UserIdentifier
                };
                var movieList = _databaseService.GetMovieListByUserId(hubUser.UserIdentifier);
                if (movieList != null) await Clients.Caller.SendAsync("RecieveMovieList", movieList);
                await Clients.Caller.SendAsync("InitCurrentUser", hubUser);
            }

            await base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task RefreshMovieList(string userId)
        {
            var movieList = _databaseService.GetMovieListByUserId(userId);
            if (movieList != null) await Clients.Caller.SendAsync("RecieveMovieList", movieList);
        }
       public async Task GetValidPathSource(string videoPath)
       {
            
            string path = videoPath;
           
            await Clients.Caller.SendAsync("InitSource", "~/PrivateCinema_VideoLibrary/0/Rayman 3.mp4");
       }
    }
}
