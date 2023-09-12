using Chessington.GameEngine.Pieces;
using FluentAssertions;
using Xunit;

namespace Chessington.GameEngine.Tests.Pieces;

public class RookTests
{
    [Fact]
    public void Rooks_CanMove_Laterally()
    {
        var board = new Board();
        var rook = new Rook(Player.White);
        board.AddPiece(Square.At(4, 4), rook);

        var moves = rook.GetAvailableMoves(board);
        var expectedMoves = new List<Square>();

        for (var i = 0; i < 8; i++)
            expectedMoves.Add(Square.At(4, i));

        for (var i = 0; i < 8; i++)
            expectedMoves.Add(Square.At(i, 4));

        // Get rid of our starting location
        expectedMoves.RemoveAll(s => s == Square.At(4, 4));

        moves.Should().Contain(expectedMoves);
    }

    [Fact]
    public void Rooks_CannotPassThrough_OpposingPieces()
    {
        var board = new Board();
        var rook = new Rook(Player.White);
        board.AddPiece(Square.At(4, 4), rook);
        var pieceToTake = new Pawn(Player.Black);
        board.AddPiece(Square.At(4, 6), pieceToTake);

        var moves = rook.GetAvailableMoves(board);
        moves.Should().NotContain(Square.At(4, 7));
    }

    [Fact]
    public void Rooks_CannotPassThrough_FriendlyPieces()
    {
        var board = new Board();
        var rook = new Rook(Player.White);
        board.AddPiece(Square.At(4, 4), rook);
        var friendlyPiece = new Pawn(Player.White);
        board.AddPiece(Square.At(4, 6), friendlyPiece);

        var moves = rook.GetAvailableMoves(board);
        moves.Should().NotContain(Square.At(4, 7));
    }

    [Fact]
    public void Rooks_CanTake_OpposingPieces()
    {
        var board = new Board();
        var rook = new Rook(Player.White);
        board.AddPiece(Square.At(4, 4), rook);
        var pieceToTake = new Pawn(Player.Black);
        board.AddPiece(Square.At(4, 6), pieceToTake);

        var moves = rook.GetAvailableMoves(board);
        moves.Should().Contain(Square.At(4, 6));
    }

    [Fact]
    public void Rooks_CannotTake_FriendlyPieces()
    {
        var board = new Board();
        var rook = new Rook(Player.White);
        board.AddPiece(Square.At(4, 4), rook);
        var friendlyPiece = new Pawn(Player.White);
        board.AddPiece(Square.At(4, 6), friendlyPiece);

        var moves = rook.GetAvailableMoves(board);
        moves.Should().NotContain(Square.At(4, 6));
    }
}
