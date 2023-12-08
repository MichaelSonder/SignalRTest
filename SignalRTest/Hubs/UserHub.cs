using Microsoft.AspNetCore.SignalR;

namespace SignalRTest.Hubs
{
    public class UserHub : Hub
    {

        public static int TotalViews { get; set; } = 0;

        public static int TotalUsers { get; set; } = 0;

        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.SendAsync("uddateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("uddateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task<string> NewWindowLoaded()
        {
            TotalViews++;

            await Clients.All.SendAsync("uddateTotalViews", TotalViews);
            return $"total views - {TotalViews}";
        }
    }
}
