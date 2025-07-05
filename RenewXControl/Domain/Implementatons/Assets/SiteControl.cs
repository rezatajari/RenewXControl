using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Domain.Implementatons.Assets
{
    public class SiteControl:ISiteControl
    {
        private readonly Site _site;

        public SiteControl(Site site)
        {
            _site= site; ;
        }

        public void AddAsset(Asset asset)
        {
           _site.AddAsset(asset);
        }
    }
}
