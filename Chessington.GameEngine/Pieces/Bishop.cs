namespace Chessington.GameEngine.Pieces;

public class Bishop : Piece
{
    public Bishop(Player player)
        : base(player) { }

    public override IEnumerable<Square> GetAvailableMoves(Board board)
    {
        return Enumerable.Empty<Square>();
    }
}
