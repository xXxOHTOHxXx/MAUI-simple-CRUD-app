using _253502.UI.ViewModels;

namespace _253502.UI.Pages;

public partial class AddOrUpdateLandmark : ContentPage
{
    public AddOrUpdateLandmark(AddOrUpdateLandmarkViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}