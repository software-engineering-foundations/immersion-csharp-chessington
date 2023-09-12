﻿namespace Chessington.GameEngine.Pieces;

public abstract class Piece
{
    protected Piece(Player player)
    {
        Player = player;
    }

    public Player Player { get; }

    public abstract IEnumerable<Square> GetAvailableMoves(Board board);

    public void MoveTo(Board board, Square newSquare)
    {
        var currentSquare = board.FindPiece(this);
        board.MovePiece(currentSquare, newSquare);
    }
}
