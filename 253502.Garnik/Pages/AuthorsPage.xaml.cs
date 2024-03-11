using _253502.Garnik.ViewModels;
using _253502.Garnik;
namespace _253502.Garnik.Pages;

public partial class AuthorsPage : ContentPage
{
    public AuthorsPage(AuthorViewModel viewModel)//
    {     
        InitializeComponent();
        BindingContext = viewModel;
    }
}