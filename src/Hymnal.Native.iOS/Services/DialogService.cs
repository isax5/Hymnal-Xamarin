using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Hymnal.Core.Services;
using UIKit;

namespace Hymnal.iOS.Services
{
    public class DialogService : IDialogService
    {
        public async Task Alert(string title, string message, string cancel)
        {
            Console.WriteLine($"Alert: {title} - {message}");
            //throw new NotImplementedException();
        }

        public async Task Alert(string title, string message, string accept, string cancel)
        {
            Console.WriteLine($"Alert: {title} - {message}");
            //throw new NotImplementedException();
        }

        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            Console.WriteLine($"Alert: {title} - {buttons}");
            return buttons.First();
            //throw new NotImplementedException();
        }
    }
}
