using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Media;
using Chessington.GameEngine;
using Chessington.UI.Converters;
using Chessington.UI.Notifications;
using Chessington.UI.ViewModels;
using ReactiveUI;

namespace Chessington.UI.Factories;

public class BoardViewFactory
{
    public static void CreateBoardView(BoardViewModel boardViewModel, Panel parent)
    {
        var grid = new Grid();
        grid.MaxWidth =
            grid.MaxHeight =
            grid.Width =
            grid.Height =
                GameSettings.BoardSize * InterfaceSettings.SquareSize;
        grid.DataContext = boardViewModel;
        parent.Children.Add(grid);

        for (var i = 0; i < GameSettings.BoardSize; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (var row = 0; row < GameSettings.BoardSize; row++)
            for (var col = 0; col < GameSettings.BoardSize; col++)
                CreateSquare(row, col, grid);
    }

    private static void CreateSquare(int row, int col, Panel grid)
    {
        var squareColor = (row + col) % 2 == 0 ? Colors.White : Colors.Black;
        var square = new Canvas
        {
            Width = InterfaceSettings.SquareSize,
            Height = InterfaceSettings.SquareSize,
            Background = new SolidColorBrush(squareColor)
        };

        grid.Children.Add(square);
        Grid.SetRow(square, row);
        Grid.SetColumn(square, col);

        var squareViewModel = new SquareViewModel(Square.At(row, col));
        square.DataContext = squareViewModel;

        var pieceImage = new Image();
        pieceImage.Bind(Image.SourceProperty, new Binding("Image"));
        pieceImage.Height = pieceImage.Width = InterfaceSettings.SquareSize - 4; //Allow for border space...

        var pieceBorder = new Border { BorderThickness = new Thickness(2) };
        pieceBorder.Bind(
            Border.BorderBrushProperty,
            new Binding("Self") { Converter = new SquareToBorderBrushConverter() }
        );
        pieceBorder.Child = pieceImage;

        square.PointerPressed += SquareOnMouseDown;

        square.Children.Add(pieceBorder);
    }

    private static void SquareOnMouseDown(
        object? sender,
        PointerPressedEventArgs pointerPressedEventArgs
    )
    {
        var canvas = (Canvas)sender!;
        var square = (SquareViewModel)canvas.DataContext!;

        MessageBus.Current.SendMessage(new SquareSelected(square.Location));
        pointerPressedEventArgs.Handled = true;
    }
}
