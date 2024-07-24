using _253502.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using _253502.Domain.Abstractions;
using _253502.Domain.Entities;
using _253502.Garnik.Pages;
using _253502.Application.Queries;
using System.Collections.ObjectModel;
using _253502.Application.Commands;

namespace _253502.Garnik.ViewModels
{
    [QueryProperty("AddOrUpdate", "Action")]
    [QueryProperty("BookToUpsert", nameof(Book))]
    [QueryProperty("Author", nameof(Author))]
    public partial class AddOrUpdateBookViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        public ObservableCollection<Author> Authors { get; set; } = new ObservableCollection<Author>();

        public AddOrUpdateBookViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ObservableProperty]
        Book? bookToUpsert = new Book(new BookInfo("", new DateTime()));

        [ObservableProperty]
        Author author;

        [ObservableProperty]
        FileResult image;


        [RelayCommand]
        async Task UpdateAuthorPicker() {
            try {
                await GetAuthors();
            } catch (Exception ex) {
                Console.WriteLine($"UpdateAythorPicker threw {ex.Message}");
            }
        }

        public async Task GetAuthors()//LGTM
{
            var authors = await _mediator.Send(new GetAuthorsRequest());

            await MainThread.InvokeOnMainThreadAsync(() => Authors.Clear());
            await MainThread.InvokeOnMainThreadAsync(() => {
                foreach (var author in authors)
                    Authors.Add(author);
            });
        }


        [RelayCommand]
        public async Task PickImage()
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                        { DevicePlatform.Android, new[] { ".png" } },
                        { DevicePlatform.WinUI, new[] { ".png" } },
                });

            PickOptions options = new()
            {
                PickerTitle = "Please select a png image",
                FileTypes = customFileType,
            };

            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        Image = result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return;
        }

        public int AddOrUpdate { get; set; }//0-update 1-add

        [RelayCommand]
        async Task AddOrUpdateBook()
        {
            if (BookToUpsert.InfoData is null ||
                BookToUpsert.InfoData.Name == string.Empty)
            {
                return;
            }

            var tmp = BookToUpsert.AuthorID;
            BookToUpsert.AuthorID = author.Id;
            

            if(AddOrUpdate==0)
            {
                try {
                    await _mediator.Send(new UpdateBookCommand(BookToUpsert, tmp));
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
                
            }
            else
            {
                await _mediator.Send(new AddBookCommand(BookToUpsert.InfoData.Name, BookToUpsert.InfoData.DateOfPublishment, author.Id, BookToUpsert.Rating));
            }

            if (Image != null)
            {
                using var stream = await Image.OpenReadAsync();
                var image = ImageSource.FromStream(() => stream);

                string filename = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)), $"{BookToUpsert.Id}.png");
                try
                {
                    if(File.Exists(filename))
                    {
                        
                        File.Delete(filename);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                using var fileStream = File.Create(filename);
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
                stream.Seek(0, SeekOrigin.Begin);
            }
            await Shell.Current.GoToAsync("///AuthorsPage");
            
        }
    }
}
