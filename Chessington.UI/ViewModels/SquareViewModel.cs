using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using Chessington.GameEngine;
using Chessington.UI.Factories;
using Chessington.UI.Notifications;
using ReactiveUI;

namespace Chessington.UI.ViewModels;

public class SquareViewModel : ViewModelBase, INotifyPropertyChanged
{
    private Bitmap? _image;
    private bool _selected;
    private bool _validMovementTarget;

    public SquareViewModel(Square square)
    {
        Location = square;
        MessageBus.Current.Listen<PieceSelected>().Subscribe(Handle);
        MessageBus.Current.Listen<PiecesMoved>().Subscribe(Handle);
        MessageBus.Current.Listen<ValidMovesUpdated>().Subscribe(Handle);
        MessageBus.Current.Listen<SelectionCleared>().Subscribe(Handle);
    }

    public Square Location { get; }

    public SquareViewModel Self => this;

    public bool Selected
    {
        get => _selected;
        set
        {
            if (value.Equals(_selected))
                return;
            _selected = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Self));
        }
    }

    public bool ValidMovementTarget
    {
        get => _validMovementTarget;
        set
        {
            if (value.Equals(_validMovementTarget))
                return;
            _validMovementTarget = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Self));
        }
    }

    public Bitmap? Image
    {
        get => _image;
        set
        {
            if (Equals(value, _image))
                return;
            _image = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Self));
        }
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    public void Handle(PieceSelected notification)
    {
        Selected = notification.Square.Equals(Location);
    }

    public void Handle(PiecesMoved notification)
    {
        var currentPiece = notification.Board.GetPiece(Location);

        if (currentPiece == null)
        {
            Image = null;
            return;
        }

        Image = PieceImageFactory.GetImage(currentPiece);
    }

    public void Handle(ValidMovesUpdated message)
    {
        ValidMovementTarget = message.Moves.Contains(Location);
    }

    public void Handle(SelectionCleared message)
    {
        Selected = false;
        ValidMovementTarget = false;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        var handler = PropertyChanged;
        handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
