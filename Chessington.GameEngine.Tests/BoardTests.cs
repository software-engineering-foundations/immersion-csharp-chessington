using Chessington.GameEngine.Pieces;
using FluentAssertions;
using Xunit;

namespace Chessington.GameEngine.Tests;

public class BoardTests
{
    [Fact]
    public void Piece_CanBeAddedToBoard()
    {
        var board = new Board();
        var pawn = new Pawn(Player.White);
        board.AddPiece(Square.At(0, 0), pawn);

        board.GetPiece(Square.At(0, 0)).Should().BeSameAs(pawn);
    }

    [Fact]
    public void Piece_CanBeFoundOnBoard()
    {
        var board = new Board();
        var pawn = new Pawn(Player.White);
        var square = Square.At(6, 4);
        board.AddPiece(square, pawn);

        var location = board.FindPiece(pawn);

        location.Should().Be(square);
    }
}
