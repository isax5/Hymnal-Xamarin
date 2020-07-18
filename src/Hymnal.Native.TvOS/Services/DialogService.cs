using System;
using System.Threading.Tasks;
using Hymnal.Core.Services;

namespace Hymnal.Native.TvOS.Services
{
    public class DialogService : IDialogService
    {
        public DialogService()
        {
        }

        public Task Alert(string title, string message, string cancel)
        {
            throw new NotImplementedException();
        }

        public Task Alert(string title, string message, string accept, string cancel)
        {
            throw new NotImplementedException();
        }

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            throw new NotImplementedException();
        }
    }
}
