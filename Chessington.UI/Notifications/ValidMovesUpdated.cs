using Chessington.GameEngine;

namespace Chessington.UI.Notifications;

public class ValidMovesUpdated
{
    public ValidMovesUpdated(IReadOnlyCollection<Square> moves)
    {
        Moves = moves;
    }

    public IReadOnlyCollection<Square> Moves { get; }
}
