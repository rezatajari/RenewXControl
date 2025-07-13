namespace RXC.Client.Services
{
    public class DashboardState
    {
        public bool HasSite { get; private set; }
        public event Action OnChange;

        public void SetHasSite(bool value)
        {
            HasSite = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}
