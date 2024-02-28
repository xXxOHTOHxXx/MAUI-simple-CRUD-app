using _253502.Domain.Abstractions;
using _253502.Domain.Entities;
using Bahdanau_153502.UI.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _253502.UI.ViewModels
{
    [QueryProperty("Book", "Book")]
    public partial class BookViewModel : ObservableObject
    {
        ILandmarkService _landmarkService;
        public BookViewModel(ILandmarkService landmarkService)
        {
            _landmarkService = landmarkService;
        }

        [ObservableProperty]
        Book book;

        [RelayCommand]
        async void UpdateBook() => await GotoAddOrUpdatePage<AddOrUpdateLandmark, Book>(_landmarkService.UpdateAsync, Book);

        private async Task GotoAddOrUpdatePage<Page, Entity>(Func<Entity, Task<Entity>> method, params object[] entities)
            where Entity : class
            where Page : ContentPage
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Action", method }
            };

            foreach (object entity in entities)
            {
                string name = entity.GetType().Name;
                parameters.Add(name, entity);
            }

            await Shell.Current.GoToAsync(typeof(Page).Name, parameters);
        }
    }
}
