using _253502.Application.Commands;
using _253502.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _253502.Garnik.ViewModels
{
    [QueryProperty("AddOrUpdate", "Action")]
    [QueryProperty("AuthorToUpsert", nameof(Author))]

    public partial class AddOrUpdateAuthorViewModel : ObservableObject
    {
        [ObservableProperty]
        Author authorToUpsert = new Author("Shrek",new DateTime(),123);

        private readonly IMediator _mediator;

        public AddOrUpdateAuthorViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public int AddOrUpdate { get; set; }

        [RelayCommand]
        async Task AddOrUpdateAuthor()
        {
            if (AuthorToUpsert.Name is null || AuthorToUpsert.Name == string.Empty)
            {
                return;
            }

            if (AddOrUpdate == 0)
            {
                await _mediator.Send(new UpdateAuthorCommand(AuthorToUpsert));
            }
            else
            {
                await _mediator.Send(new AddAuthorCommand(AuthorToUpsert.Name, AuthorToUpsert.DateOfBirth, AuthorToUpsert.FavoriteNumber));
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
