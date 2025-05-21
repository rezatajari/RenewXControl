namespace RenewXControl.Domain.Assets
{
    // It's good to mark your base classes with 'abstract' modifier so, these classes can not be initialized directly.
    // We never use them as a stand-alone object in out logic. We just use them to share some functionalities.
    // Base class for all assets
    public class Asset
    {
        public Asset(int siteId)
        {
            SiteId=siteId;
        }
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
    }
}
