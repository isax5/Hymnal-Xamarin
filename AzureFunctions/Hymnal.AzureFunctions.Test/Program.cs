using System;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.AzureFunctions.Client;

namespace Hymnal.AzureFunctions.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => AsyncMain()).Wait();
        }

        static async Task AsyncMain()
        {
            var service = new HymnService();
            var results = await service.MusicSettingsAsync();

            Console.WriteLine($"{results.Count()} Items en settings");

        }
    }
}
