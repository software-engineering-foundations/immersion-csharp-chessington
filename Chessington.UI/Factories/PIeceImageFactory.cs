using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Chessington.GameEngine.Pieces;

namespace Chessington.UI.Factories;

/// <summary>
///     Given a piece, returns in image for that piece. Change things here if you want new icons.
/// </summary>
public static class PieceImageFactory
{
    private static readonly Dictionary<Type, string> PieceSuffixes =
        new()
        {
            { typeof(Pawn), "pawn" },
            { typeof(Rook), "rook" },
            { typeof(Knight), "knight" },
            { typeof(Bishop), "bishop" },
            { typeof(King), "king" },
            { typeof(Queen), "queen" }
        };

    public static Bitmap GetImage(Piece piece)
    {
        return new Bitmap(
            AssetLoader.Open(
                new Uri(
                    string.Format(
                        $"{InterfaceSettings.IconRoot}{piece.Player.ToString().ToLower()}-{PieceSuffixes[piece.GetType()]}.ico"
                    )
                )
            )
        );
    }
}
