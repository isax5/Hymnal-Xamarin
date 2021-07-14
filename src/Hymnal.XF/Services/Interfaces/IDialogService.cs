using System.Threading.Tasks;

namespace Hymnal.XF.Services
{
    public interface IDialogService
    {
        Task Alert(string title, string cancel);

        Task Alert(string title, string message, string cancel);

        Task Alert(string title, string message, string accept, string cancel);

        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
    }
}
