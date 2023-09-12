namespace Chessington.GameEngine.Pieces;

public class King : Piece
{
    public King(Player player)
        : base(player) { }

    public override IEnumerable<Square> GetAvailableMoves(Board board)
    {
        return Enumerable.Empty<Square>();
    }
}
