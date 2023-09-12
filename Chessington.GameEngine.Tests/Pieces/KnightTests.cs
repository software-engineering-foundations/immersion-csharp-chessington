using Chessington.GameEngine.Pieces;
using FluentAssertions;
using Xunit;

namespace Chessington.GameEngine.Tests.Pieces;

public class KnightTests
{
    [Fact]
    public void Knights_CanPerform_KnightMoves()
    {
        var board = new Board();
        var knight = new Knight(Player.White);
        board.AddPiece(Square.At(4, 4), knight);

        var moves = knight.GetAvailableMoves(board);

        var expectedMoves = new List<Square>
        {
            Square.At(2, 5),
            Square.At(2, 3),
            Square.At(3, 6),
            Square.At(3, 2),
            Square.At(5, 6),
            Square.At(5, 2),
            Square.At(6, 5),
            Square.At(6, 3)
        };

        moves.Should().AllBeEquivalentTo(expectedMoves);
    }

    [Fact]
    public void Knights_CanJumpOver_Pieces()
    {
        var board = new Board();
        var knight = new Knight(Player.White);
        board.AddPiece(Square.At(4, 4), knight);

        var firstPawn = new Pawn(Player.White);
        var secondPawn = new Pawn(Player.Black);
        board.AddPiece(Square.At(3, 4), firstPawn);
        board.AddPiece(Square.At(3, 5), secondPawn);

        var moves = knight.GetAvailableMoves(board);

        moves.Should().Contain(Square.At(2, 5));
    }

    [Fact]
    public void Knights_CannotLeave_TheBoard()
    {
        var board = new Board();
        var knight = new Knight(Player.White);
        board.AddPiece(Square.At(0, 0), knight);

        var moves = knight.GetAvailableMoves(board);

        var expectedMoves = new List<Square> { Square.At(1, 2), Square.At(2, 1) };
        moves.Should().AllBeEquivalentTo(expectedMoves);
    }

    [Fact]
    public void Knights_CanTake_OpposingPieces()
    {
        var board = new Board();
        var knight = new Knight(Player.White);
        board.AddPiece(Square.At(4, 4), knight);

        var pawn = new Pawn(Player.Black);
        board.AddPiece(Square.At(2, 5), pawn);

        var moves = knight.GetAvailableMoves(board);

        moves.Should().Contain(Square.At(2, 5));
    }

    [Fact]
    public void Knights_CannotTake_FriendlyPieces()
    {
        var board = new Board();
        var knight = new Knight(Player.White);
        board.AddPiece(Square.At(4, 4), knight);
        var pawn = new Pawn(Player.White);
        board.AddPiece(Square.At(2, 5), pawn);

        var moves = knight.GetAvailableMoves(board);

        moves.Should().NotContain(Square.At(2, 5));
    }
}
