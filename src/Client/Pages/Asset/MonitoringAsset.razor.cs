using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace Client.Pages.Asset;

public partial class MonitoringAsset
{
    private HubConnection? _hubConnection;
    private bool isConnected = false;
    private AssetsMonitoring? assetsMonitoring;
    private Timer? _updateTimer;
    private bool _disposed;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (string.IsNullOrEmpty(token))
            {
                Nav.NavigateTo("/login");
                return;
            }

            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Register for monitoring
            var response = await Http.PostAsync("api/monitoring/register", null);
            if (!response.IsSuccessStatusCode)
            {
                Console.Error.WriteLine("Failed to register for monitoring");
                return;
            }

            // Initialize SignalR connection
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(HubConfig.HubUrl, options =>
                {
                    options.AccessTokenProvider = async () =>
                        await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
                })
                .WithAutomaticReconnect(new[] {
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                })
                .Build();

            _hubConnection.Reconnected += _ =>
            {
                isConnected = true;
                StateHasChanged();
                return Task.CompletedTask;
            };

            _hubConnection.Reconnecting += _ =>
            {
                isConnected = false;
                StateHasChanged();
                return Task.CompletedTask;
            };

            _hubConnection.On<AssetsMonitoring>("AssetUpdate", updatedAssets =>
            {
                assetsMonitoring = updatedAssets;
                InvokeAsync(StateHasChanged);
            });

            await _hubConnection.StartAsync();
            isConnected = true;

            // Request initial data
            await _hubConnection.SendAsync("RequestInitialData");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Monitoring initialization error: {ex.Message}");
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;

        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }

    private class AssetsMonitoring
    {
        public Solar Solar { get; set; } = default!;
        public Turbine Turbine { get; set; } = default!;
        public Battery Battery { get; set; } = default!;
    }
}