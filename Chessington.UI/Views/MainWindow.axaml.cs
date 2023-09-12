using Avalonia.Controls;
using Chessington.UI.Factories;
using Chessington.UI.ViewModels;

namespace Chessington.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        DataContext = new GameViewModel();
        InitializeComponent();

        var boardViewModel = new BoardViewModel();
        StartingPositionFactory.Setup(boardViewModel.Board);
        BoardViewFactory.CreateBoardView(boardViewModel, GridContainer);

        boardViewModel.PiecesMoved();
    }
}
