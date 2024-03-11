using _253502.Domain.Abstractions;
using _253502.Domain.Entities;
using _253502.Garnik.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using _253502.Application.Queries;
using System.Collections.ObjectModel;

namespace _253502.Garnik.ViewModels
{
    public partial class AuthorViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        public AuthorViewModel(IMediator mediator)
        {
            _mediator = mediator;    
        }


        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();


        public ObservableCollection<Author> Authors { get; set; } = new ObservableCollection<Author>();

        [ObservableProperty]
        public Author? selectedAuthor;
        private async Task GotoDetailsPage()
        {
            await Shell.Current.GoToAsync(nameof(BooksPage));
        }
        
        [RelayCommand]
        async Task UpdateGroupList()
        {
            try
            {
                await GetAuthors();
                await UpdateMembersList();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"UpdateGroupList threw {ex.Message}");
            }
           
        }
        [RelayCommand]
        async void AddAuthor() => await GotoAddOrUpdatePage<AddOrUpdateAuthor, Author>(1);

        [RelayCommand]
        async void UpdateAuthor()
        {
            if (SelectedAuthor is not null)
            {
                await GotoAddOrUpdatePage<AddOrUpdateAuthor, Author>(0, SelectedAuthor);
            }
        }

        [RelayCommand]
        async void AddBook()
        {
            if (SelectedAuthor is not null)
            {
                await GotoAddOrUpdatePage<AddOrUpdateBook, Book>(1, SelectedAuthor);
            }
        }
        private async Task GotoAddOrUpdatePage<Page, Entity>(int method, params object[] entities)
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

            try
            {
                await Shell.Current.GoToAsync(typeof(Page).Name, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        // Команда обновления списка слушателей курса
        [RelayCommand]
        async Task UpdateMembersList() => await GetBooks();

        public async Task GetAuthors()//LGTM
        {
            var authors = await _mediator.Send(new GetAuthorsRequest());

            await MainThread.InvokeOnMainThreadAsync(() => Authors.Clear());
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                foreach (var author in authors)
                        Authors.Add(author);
            });

        }
        public async Task GetBooks()//LGTM
        {
            if (SelectedAuthor == null)
            {
                await MainThread.InvokeOnMainThreadAsync(() => Books.Clear());
                return;
            }
            var books = await _mediator.Send(new GetBooksByGroupRequest(SelectedAuthor.Id));
            await MainThread.InvokeOnMainThreadAsync(() => Books.Clear());
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                foreach (var book in books)
                    Books.Add(book);
            });
            
        }

        [RelayCommand]//DONT TOUCH
        async void ShowDetails(Book book) => await GotoDetailsPage(book);
        private async Task GotoDetailsPage(Book book)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Book", book }
            };

            await Shell.Current.GoToAsync(nameof(BooksPage), parameters);
        }

    }

}

