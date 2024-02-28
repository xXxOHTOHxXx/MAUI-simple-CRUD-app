using _253502.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;

namespace _253502.UI.ViewModels
{
    [QueryProperty("AddOrUpdate", "Action")]
    [QueryProperty("LandmarkToUpsert", nameof(Book))]
    [QueryProperty("Route", nameof(Author))]
    public partial class AddOrUpdateLandmarkViewModel : ObservableObject
    {
        [ObservableProperty]
        Book bookToUpsert = new();

        [ObservableProperty]
        Author author = new();

        [ObservableProperty]
        FileResult image;

        [RelayCommand]
        public async void PickImage()
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
                return;
            }

            return;
        }

        public Func<Book, Task<Book>> AddOrUpdate { get; set; }

        [RelayCommand]
        async void AddOrUpdateLandmark()
        {
            if (
                BookToUpsert.City is null ||
                BookToUpsert.City == string.Empty ||
                BookToUpsert.Description is null ||
                BookToUpsert.Description == string.Empty
                )
            {
                return;
            }

            BookToUpsert.TouristRoute = BookToUpsert.TouristRoute ?? Route;

            await AddOrUpdate(LandmarkToUpsert);

            if (Image != null)
            {
                using var stream = await Image.OpenReadAsync();
                var image = ImageSource.FromStream(() => stream);

                string filename = Path.Combine(Preferences.Default.Get<string>("LocalData", null), $"{LandmarkToUpsert.Id}.png");

                using var fileStream = File.Create(filename);
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
                stream.Seek(0, SeekOrigin.Begin);
            }
            

            await Shell.Current.GoToAsync("..");
        }
    }
}
