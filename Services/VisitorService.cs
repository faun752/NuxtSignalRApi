using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using NuxtSignalRApi.Hubs;

namespace NuxtSignalRApi.Services
{
    /// <summary>
    /// 擬似的に来訪カウントをカウントアップするクラス
    /// </summary>
    public class VisitorService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer _timer;
        private readonly IHubContext<VisitorHub> _visitorHub;
        private int visitorCount = 0;

        public VisitorService (IHubContext<VisitorHub> visitorHub)
        {
            _visitorHub = visitorHub;
        }

        public Task StartAsync (CancellationToken cancellationToken)
        {
            // 5秒毎に来訪数をカウントアップする
            _timer = new Timer (DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds (5));

            return Task.CompletedTask;
        }

        private void DoWork (object state)
        {
            visitorCount++;
            var age = new Random ().Next (0, 99);
            string[] genderArray = { "male", "female", "unknown" };
            var index = new Random ().Next (genderArray.Length);
            var gender = genderArray[index];
            var count = Interlocked.Increment (ref executionCount);
            _visitorHub.Clients.All.SendAsync ("ReceiveVisitor", visitorCount, age, gender);
        }

        public Task StopAsync (CancellationToken cancellationToken)
        {
            _timer?.Change (Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose ()
        {
            _timer?.Dispose ();
        }
    }
}