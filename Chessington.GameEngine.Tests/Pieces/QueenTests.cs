using Chessington.GameEngine.Pieces;
using FluentAssertions;
using Xunit;

namespace Chessington.GameEngine.Tests.Pieces;

public class QueenTests
{
    [Fact]
    public void Queens_CanMove_Laterally()
    {
        var board = new Board();
        var queen = new Queen(Player.White);
        board.AddPiece(Square.At(4, 4), queen);

        var moves = queen.GetAvailableMoves(board);
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
    public void Queens_CanMove_Diagonally()
    {
        var board = new Board();
        var queen = new Queen(Player.White);
        board.AddPiece(Square.At(4, 4), queen);

        var moves = queen.GetAvailableMoves(board);
        var expectedMoves = new List<Square>();

        for (var i = 0; i < 8; i++)
            expectedMoves.Add(Square.At(i, i));

        for (var i = 1; i < 7; i++)
            expectedMoves.Add(Square.At(i, 8 - i));

        // Get rid of our starting location
        expectedMoves.RemoveAll(s => s == Square.At(4, 4));

        moves.Should().Contain(expectedMoves);
    }

    [Fact]
    public void Queens_CannotMake_AnyOtherMoves()
    {
        var board = new Board();
        var queen = new Queen(Player.White);
        board.AddPiece(Square.At(4, 4), queen);

        var moves = queen.GetAvailableMoves(board);

        // There are 27 valid lateral and diagonal moves. We need to make sure that no other moves are available
        moves.Should().HaveCount(27);
    }

    [Fact]
    public void Queens_CannotPassThrough_OpposingPieces()
    {
        var board = new Board();
        var queen = new Queen(Player.White);
        board.AddPiece(Square.At(4, 4), queen);
        var pieceToTake = new Pawn(Player.Black);
        board.AddPiece(Square.At(4, 6), pieceToTake);

        var moves = queen.GetAvailableMoves(board);
        moves.Should().NotContain(Square.At(4, 7));
    }

    [Fact]
    public void Queens_CannotPassThrough_FriendlyPieces()
    {
        var board = new Board();
        var queen = new Queen(Player.White);
        board.AddPiece(Square.At(4, 4), queen);
        var friendlyPiece = new Pawn(Player.White);
        board.AddPiece(Square.At(4, 6), friendlyPiece);

        var moves = queen.GetAvailableMoves(board);
        moves.Should().NotContain(Square.At(4, 7));
    }

    [Fact]
    public void Queens_CanTake_OpposingPieces()
    {
        var board = new Board();
        var queen = new Queen(Player.White);
        board.AddPiece(Square.At(4, 4), queen);
        var pieceToTake = new Pawn(Player.Black);
        board.AddPiece(Square.At(4, 6), pieceToTake);

        var moves = queen.GetAvailableMoves(board);
        moves.Should().Contain(Square.At(4, 6));
    }

    [Fact]
    public void Queens_CannotTake_FriendlyPieces()
    {
        var board = new Board();
        var queen = new Queen(Player.White);
        board.AddPiece(Square.At(4, 4), queen);
        var friendlyPiece = new Pawn(Player.White);
        board.AddPiece(Square.At(4, 6), friendlyPiece);

        var moves = queen.GetAvailableMoves(board);
        moves.Should().NotContain(Square.At(4, 6));
    }
}
