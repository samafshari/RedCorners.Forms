using RedCorners.Forms.Services;

using RedCorners.Extensions;

using Xamarin.Forms;

[assembly: Dependency(typeof(RandomService))]
namespace RedCorners.Forms.Services
{
    public class RandomService
    {
        public string GenerateId()
        {
            return IdExtensions.GenerateId();
        }
    }
}