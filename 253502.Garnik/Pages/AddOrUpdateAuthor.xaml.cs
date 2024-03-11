using _253502.Garnik.ViewModels;

namespace _253502.Garnik.Pages;

public partial class AddOrUpdateAuthor : ContentPage
{
    public AddOrUpdateAuthor(AddOrUpdateAuthorViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}