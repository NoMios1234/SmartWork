using SmartWork.PC.Configurations;
using System.Threading.Tasks;

namespace SmartWorkServerApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await HostSettings<Startup>.SetBuildSettings(args, true);
        }
    }
}