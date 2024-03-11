using _253502.Domain.Abstractions;
using _253502.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using _253502.Garnik.Pages;
using _253502.Application.Queries;
using _253502.Application.Commands;

namespace _253502.Garnik.ViewModels
{
    [QueryProperty("Book", "Book")]
    public partial class BookViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        public BookViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }


        [ObservableProperty]
        Book book;

        [RelayCommand]
        async void UpdateBook() => await GotoAddOrUpdatePage<AddOrUpdateBook, Book>(0, Book);
        private async Task GotoAddOrUpdatePage<Page, Entity>(int command, params object[] entities)
            where Entity : class
            where Page : ContentPage
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Action", command }
            };

            foreach (object entity in entities)
            {
                string name = entity.GetType().Name;
                parameters.Add(name, entity);
            }

            try
            {
                await Shell.Current.GoToAsync("AddOrUpdateBook", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
