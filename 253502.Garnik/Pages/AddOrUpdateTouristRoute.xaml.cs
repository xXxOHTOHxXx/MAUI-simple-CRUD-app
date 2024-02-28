using _253502.UI.ViewModels;

namespace _253502.UI.Pages;

public partial class AddOrUpdateTouristRoute : ContentPage
{
    public AddOrUpdateTouristRoute(AddOrUpdateTouristRouteViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}