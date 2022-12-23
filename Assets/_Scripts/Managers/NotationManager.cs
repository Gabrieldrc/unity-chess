using System;
using System.Collections.Generic;
using Game._Scripts.Core.Notations;
using Game.Components;
using UnityEngine;

namespace Game.Managers
{
    public class NotationManager : MonoBehaviour
    {
        [SerializeField]
        private List<Notation> _notations;

        [SerializeField]
        private ChessManager _chessManager;

        [SerializeField]
        private PieceGraveyardManager _graveyardManager;

        private int _notationIndex = -1;
        public event Action<int, List<string>> OnNotationChange;

        public void AddNotation(Notation notation)
        {
            _notationIndex++; //Verificar si no es la ultima posicion, para eliminar los restantes
            if (_notationIndex < _notations.Count)
            {
                _notations.RemoveRange(_notationIndex, _notations.Count - _notationIndex);
            }
            _notations.Add(notation);
            Notify();
        }
        
        public void Undo()
        {
            if (_notationIndex < 0) return;
            var notation = _notations[_notationIndex];
            var movedPiece = notation.MovedPiece.Piece;
            movedPiece.Position = notation.MovedPiece.FromPosition;
            movedPiece.Container.transform.position = notation.MovedPiece.FromTransformPosition;
            notation.DeadPiece.Container.transform.position = notation.MovedPiece.ToTransformPosition;
            notation.DeadPiece.Position = notation.MovedPiece.ToPosition;
            /*
             * TODO:
             * Colocar el contenedor de la pieza muerta donde estaba.
             * Actualizar el board y el Turn
             */
            _notationIndex--;
            Notify();
        }

        public void Redo()
        {
            if (_notationIndex == _notations.Count - 1) return;
            _notationIndex++;
            var notation = _notations[_notationIndex];
            var movedPiece = notation.MovedPiece.Piece;
            movedPiece.Position = notation.MovedPiece.ToPosition;
            movedPiece.Container.transform.position = notation.MovedPiece.ToTransformPosition;
            notation.DeadPiece.Container.transform.position = notation.MovedPiece.ToTransformPosition;
            notation.DeadPiece.Position = notation.MovedPiece.ToPosition;
            /*
             * TODO:
             * Colocar el contenedor de la pieza muerta donde estaba.
             * Actualizar el board y el Turn
             */
            Notify();
        }

        void Notify()
        {
            List<string> notationsString = new List<string>();
            foreach (var notation in _notations)
            {
                notationsString.Add(notation.ToString());
            }

            OnNotationChange.Invoke(_notationIndex, notationsString);
        }
    }
}