using System.Threading.Tasks;

namespace FriendsOrganizer.UI.ViewModels.Abstraction
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int id);

        Task LoadAddableAsync();


        bool HasChange { get; set; }
    }
}
