using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using Chessington.GameEngine;
using Chessington.UI.Factories;
using Chessington.UI.Notifications;
using ReactiveUI;

namespace Chessington.UI.ViewModels;

public class GameViewModel : ViewModelBase, INotifyPropertyChanged
{
    private string? _currentPlayer;

    public GameViewModel()
    {
        CapturedPieces = new ObservableCollection<Bitmap>();
        CurrentPlayer = Enum.GetName(typeof(Player), Player.White);
        MessageBus.Current.Listen<PieceTaken>().Subscribe(Handle);
        MessageBus.Current.Listen<CurrentPlayerChanged>().Subscribe(Handle);
    }

    public ObservableCollection<Bitmap> CapturedPieces { get; }

    public string? CurrentPlayer
    {
        get => _currentPlayer;
        private set
        {
            if (value == _currentPlayer)
                return;
            _currentPlayer = value;
            OnPropertyChanged();
        }
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    public void Handle(PieceTaken message)
    {
        CapturedPieces.Add(PieceImageFactory.GetImage(message.Piece));
    }

    public void Handle(CurrentPlayerChanged message)
    {
        CurrentPlayer = Enum.GetName(typeof(Player), message.Player);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        var handler = PropertyChanged;
        handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
