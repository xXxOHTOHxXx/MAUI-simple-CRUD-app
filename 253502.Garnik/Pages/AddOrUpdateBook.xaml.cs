using _253502.Garnik.ViewModels;

namespace _253502.Garnik.Pages;

public partial class AddOrUpdateBook : ContentPage
{
    public AddOrUpdateBook(AddOrUpdateBookViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}