using _253502.UI.ViewModels;

namespace _253502.UI.Pages;

public partial class BooksPage : ContentPage
{
    public BooksPage(BookViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}