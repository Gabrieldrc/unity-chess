using System;
using UnityEngine;

namespace Game.Core.Pieces
{
    public class Pawn : Piece
    {
        private bool _isFirstMove = true;
        public Pawn(Position position, PieceColor color) : base(position, color)
        {
        }

        public override string Sign { get; set; } = "";

        protected override bool CanMove(Position position, ChessBoard board)
        {
            var multiplier = Color == PieceColor.Black ? 1 : -1;
            var itMoveOneStepInRow = position.row - Position.row == 1 * multiplier;
            var itMoveTwoStepsInRowInItsFirstMove = position.row - Position.row == 2 * multiplier && _isFirstMove;
            var itStayInSameCol = Position.col == position.col;
            var destinyPiece = board.GetPieceIn(position);
            var theDestinyPositionIsEmpty = destinyPiece is Empty;

            if (
                itStayInSameCol 
                && (itMoveOneStepInRow || itMoveTwoStepsInRowInItsFirstMove) 
                && theDestinyPositionIsEmpty 
                )
            {
                return true;
            }
            var itMoveOneStepInCol = Math.Abs(position.col - Position.col) == 1;

            var isAnEnemy = destinyPiece.Color != Color;
            if (
                itMoveOneStepInCol
                && itMoveOneStepInRow
                && !theDestinyPositionIsEmpty
                && isAnEnemy
                )
            {
                return true;
            }

            return false;
        }

        public override void Move(Position to, Piece destinationPiece)
        {
            base.Move(to, destinationPiece);
            if (_isFirstMove)
            {
                _isFirstMove = false;
            }
        }
    }
}
