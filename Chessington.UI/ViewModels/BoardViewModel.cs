using System.Collections.ObjectModel;
using Chessington.GameEngine;
using Chessington.GameEngine.Pieces;
using Chessington.UI.Notifications;
using ReactiveUI;

namespace Chessington.UI.ViewModels;

public class BoardViewModel : ViewModelBase
{
    private Piece? _currentPiece;

    public BoardViewModel()
    {
        Board = new Board();
        Board.PieceCaptured += BoardOnPieceCaptured;
        Board.CurrentPlayerChanged += BoardOnCurrentPlayerChanged;

        MessageBus.Current.Listen<PieceSelected>().Subscribe(Handle);
        MessageBus.Current.Listen<SquareSelected>().Subscribe(Handle);
        MessageBus.Current.Listen<SelectionCleared>().Subscribe(Handle);
    }

    public Board Board { get; }

    public void Handle(PieceSelected message)
    {
        _currentPiece = Board.GetPiece(message.Square);
        if (_currentPiece == null)
            return;

        var moves = new ReadOnlyCollection<Square>(_currentPiece.GetAvailableMoves(Board).ToList());
        MessageBus.Current.SendMessage(new ValidMovesUpdated(moves));
    }

    public void Handle(SelectionCleared message)
    {
        _currentPiece = null;
    }

    public void Handle(SquareSelected message)
    {
        var piece = Board.GetPiece(message.Square);
        if (piece != null && piece.Player == Board.CurrentPlayer)
        {
            MessageBus.Current.SendMessage(new PieceSelected(message.Square));
            return;
        }

        if (_currentPiece == null)
            return;

        var moves = _currentPiece.GetAvailableMoves(Board);

        if (moves.Contains(message.Square))
        {
            _currentPiece.MoveTo(Board, message.Square);

            MessageBus.Current.SendMessage(new PiecesMoved(Board));
            MessageBus.Current.SendMessage(new SelectionCleared());
        }
    }

    public void PiecesMoved()
    {
        MessageBus.Current.SendMessage(new PiecesMoved(Board));
    }

    private static void BoardOnPieceCaptured(Piece piece)
    {
        MessageBus.Current.SendMessage(new PieceTaken(piece));
    }

    private static void BoardOnCurrentPlayerChanged(Player player)
    {
        MessageBus.Current.SendMessage(new CurrentPlayerChanged(player));
    }
}
