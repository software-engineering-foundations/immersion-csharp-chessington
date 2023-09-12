using Chessington.GameEngine.Pieces;
using FluentAssertions;
using Xunit;

namespace Chessington.GameEngine.Tests.Pieces;

public class PawnTests
{
    [Fact]
    public void WhitePawns_CanMove_OneSquareUp()
    {
        var board = new Board();
        var pawn = new Pawn(Player.White);
        board.AddPiece(Square.At(7, 0), pawn);

        var moves = pawn.GetAvailableMoves(board);

        moves.Should().Contain(Square.At(6, 0));
    }

    [Fact]
    public void BlackPawns_CanMove_OneSquareDown()
    {
        var board = new Board();
        var pawn = new Pawn(Player.Black);
        board.AddPiece(Square.At(1, 0), pawn);

        var moves = pawn.GetAvailableMoves(board);

        moves.Should().Contain(Square.At(2, 0));
    }

    [Fact]
    public void WhitePawns_WhichHaveNeverMoved_CanMove_TwoSquaresUp()
    {
        var board = new Board();
        var pawn = new Pawn(Player.White);
        board.AddPiece(Square.At(7, 5), pawn);

        var moves = pawn.GetAvailableMoves(board);

        moves.Should().Contain(Square.At(5, 5));
    }

    [Fact]
    public void BlackPawns_WhichHaveNeverMoved_CanMove_TwoSquaresDown()
    {
        var board = new Board();
        var pawn = new Pawn(Player.Black);
        board.AddPiece(Square.At(1, 3), pawn);

        var moves = pawn.GetAvailableMoves(board);

        moves.Should().Contain(Square.At(3, 3));
    }

    [Fact]
    public void WhitePawns_WhichHaveAlreadyMoved_CanOnlyMove_OneSquare()
    {
        var board = new Board();
        var pawn = new Pawn(Player.White);
        board.AddPiece(Square.At(7, 2), pawn);

        pawn.MoveTo(board, Square.At(6, 2));
        var moves = pawn.GetAvailableMoves(board).ToList();

        moves.Should().HaveCount(1);
        moves.Should().Contain(square => square.Equals(Square.At(5, 2)));
    }

    [Fact]
    public void BlackPawns_WhichHaveAlreadyMoved_CanOnlyMove_OneSquare()
    {
        var board = new Board(Player.Black);
        var pawn = new Pawn(Player.Black);
        board.AddPiece(Square.At(5, 2), pawn);

        pawn.MoveTo(board, Square.At(6, 2));
        var moves = pawn.GetAvailableMoves(board).ToList();

        moves.Should().HaveCount(1);
        moves.Should().Contain(square => square.Equals(Square.At(7, 2)));
    }

    [Fact]
    public void Pawns_CannotMove_IfThereIsAPieceInFront()
    {
        var board = new Board();
        var pawn = new Pawn(Player.Black);
        var blockingPiece = new Rook(Player.White);
        board.AddPiece(Square.At(1, 3), pawn);
        board.AddPiece(Square.At(2, 3), blockingPiece);

        var moves = pawn.GetAvailableMoves(board);

        moves.Should().BeEmpty();
    }

    [Fact]
    public void Pawns_CannotMove_TwoSquares_IfThereIsAPieceTwoSquaresInFront()
    {
        var board = new Board();
        var pawn = new Pawn(Player.Black);
        var blockingPiece = new Rook(Player.White);
        board.AddPiece(Square.At(1, 3), pawn);
        board.AddPiece(Square.At(3, 3), blockingPiece);

        var moves = pawn.GetAvailableMoves(board);

        moves.Should().NotContain(Square.At(3, 3));
    }

    [Fact]
    public void WhitePawns_CannotMove_AtTheTopOfTheBoard()
    {
        var board = new Board();
        var pawn = new Pawn(Player.White);
        board.AddPiece(Square.At(0, 3), pawn);

        var moves = pawn.GetAvailableMoves(board);

        moves.Should().BeEmpty();
    }

    [Fact]
    public void BlackPawns_CannotMove_AtTheBottomOfTheBoard()
    {
        var board = new Board();
        var pawn = new Pawn(Player.Black);
        board.AddPiece(Square.At(7, 3), pawn);

        var moves = pawn.GetAvailableMoves(board);

        moves.Should().BeEmpty();
    }

    [Fact]
    public void BlackPawns_CanMove_Diagonally_IfThereIsAPieceToTake()
    {
        var board = new Board();
        var pawn = new Pawn(Player.Black);
        var firstTarget = new Pawn(Player.White);
        var secondTarget = new Pawn(Player.White);
        board.AddPiece(Square.At(5, 3), pawn);
        board.AddPiece(Square.At(6, 4), firstTarget);
        board.AddPiece(Square.At(6, 2), secondTarget);

        var moves = pawn.GetAvailableMoves(board).ToList();

        moves.Should().Contain(Square.At(6, 2));
        moves.Should().Contain(Square.At(6, 4));
    }

    [Fact]
    public void WhitePawns_CanMove_Diagonally_IfThereIsAPieceToTake()
    {
        var board = new Board();
        var pawn = new Pawn(Player.White);
        var firstTarget = new Pawn(Player.Black);
        var secondTarget = new Pawn(Player.Black);
        board.AddPiece(Square.At(7, 3), pawn);
        board.AddPiece(Square.At(6, 4), firstTarget);
        board.AddPiece(Square.At(6, 2), secondTarget);

        var moves = pawn.GetAvailableMoves(board).ToList();

        moves.Should().Contain(Square.At(6, 2));
        moves.Should().Contain(Square.At(6, 4));
    }

    [Fact]
    public void BlackPawns_CannotMove_Diagonally_IfThereIsNoPieceToTake()
    {
        var board = new Board();
        var pawn = new Pawn(Player.Black);
        board.AddPiece(Square.At(5, 3), pawn);

        var friendlyPiece = new Pawn(Player.Black);
        board.AddPiece(Square.At(6, 2), friendlyPiece);

        var moves = pawn.GetAvailableMoves(board).ToList();

        moves.Should().NotContain(Square.At(6, 2));
        moves.Should().NotContain(Square.At(6, 4));
    }

    [Fact]
    public void WhitePawns_CannotMove_Diagonally_IfThereIsNoPieceToTake()
    {
        var board = new Board();
        var pawn = new Pawn(Player.White);
        board.AddPiece(Square.At(7, 3), pawn);

        var friendlyPiece = new Pawn(Player.White);
        board.AddPiece(Square.At(6, 2), friendlyPiece);

        var moves = pawn.GetAvailableMoves(board).ToList();

        moves.Should().NotContain(Square.At(6, 2));
        moves.Should().NotContain(Square.At(6, 4));
    }
}
