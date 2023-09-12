using Chessington.GameEngine.Pieces;
using FluentAssertions;
using Xunit;

namespace Chessington.GameEngine.Tests.Pieces;

public class KingTests
{
    [Fact]
    public void Kings_CanMoveTo_AdjacentSquares()
    {
        var board = new Board();
        var king = new King(Player.White);
        board.AddPiece(Square.At(4, 4), king);

        var moves = king.GetAvailableMoves(board);

        var expectedMoves = new List<Square>();

        for (var i = -1; i <= 1; i++)
            for (var j = -1; j <= 1; j++)
                expectedMoves.Add(Square.At(4 + i, 4 + j));

        // Get rid of our starting location
        expectedMoves.RemoveAll(s => s == Square.At(4, 4));

        moves.Should().AllBeEquivalentTo(expectedMoves);
    }

    [Fact]
    public void Kings_CannotLeave_TheBoard()
    {
        var board = new Board();
        var king = new King(Player.White);
        board.AddPiece(Square.At(0, 0), king);

        var moves = king.GetAvailableMoves(board);

        var expectedMoves = new List<Square> { Square.At(1, 0), Square.At(1, 1), Square.At(0, 1) };

        moves.Should().AllBeEquivalentTo(expectedMoves);
    }

    [Fact]
    public void Kings_CanTake_OpposingPieces()
    {
        var board = new Board();
        var king = new King(Player.White);
        board.AddPiece(Square.At(4, 4), king);
        var pawn = new Pawn(Player.Black);
        board.AddPiece(Square.At(4, 5), pawn);

        var moves = king.GetAvailableMoves(board);
        moves.Should().Contain(Square.At(4, 5));
    }

    [Fact]
    public void Kings_CannotTake_FriendlyPieces()
    {
        var board = new Board();
        var king = new King(Player.White);
        board.AddPiece(Square.At(4, 4), king);
        var pawn = new Pawn(Player.White);
        board.AddPiece(Square.At(4, 5), pawn);

        var moves = king.GetAvailableMoves(board);
        moves.Should().NotContain(Square.At(4, 5));
    }
}
