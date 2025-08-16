using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using WebClient.DTOs.AssetMonitoring;
using WebClient.Utility;

namespace WebClient.Pages;

public partial class MonitoringAsset
{
    [Inject] private HubConfig HubConfig { get; set; } = default!;
    private HubConnection? _hubConnection;

    private readonly List<UserMonitoringInfoDto> _assetsMonitoring = [];

    protected override async Task OnInitializedAsync()
    {
        if (_hubConnection is null)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(HubConfig.HubUrl)
                .WithAutomaticReconnect()
                .Build();

            // Receive updates for one site at a time
            _hubConnection.On<UserMonitoringInfoDto>("UpdateAsset", (data) =>
            {
                var existingSite = _assetsMonitoring
                    .FirstOrDefault(x => x.SiteName == data.SiteName && x.UserName == data.UserName);

                if (existingSite != null)
                {
                    // Update only that site's data
                    existingSite.Solar = data.Solar;
                    existingSite.Turbine = data.Turbine;
                    existingSite.Battery = data.Battery;
                }
                else
                {
                    // Add new site
                    _assetsMonitoring.Add(data);
                }

                InvokeAsync(StateHasChanged);
            });
        }

        await _hubConnection.StartAsync();

    }


    private class UserMonitoringInfoDto
    {
        public string UserName { get; set; } = string.Empty;
        public string SiteName { get; set; } = string.Empty;
        public string SiteLocation { get; set; } = string.Empty;
        public Solar Solar { get; set; } = default!;
        public Turbine Turbine { get; set; } = default!;
        public Battery Battery { get; set; } = default!;
    }

}