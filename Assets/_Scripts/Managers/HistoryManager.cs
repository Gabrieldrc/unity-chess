using System;
using System.Collections.Generic;
using Game._Scripts.Core.Notations;
using Game.Components;
using UnityEngine;

namespace Game.Managers
{
    public class HistoryManager : MonoBehaviour
    {
        [SerializeField]
        private List<Checkpoint> _checkpoints = new List<Checkpoint>();

        [SerializeField]
        private ChessManager _chessManager;

        [SerializeField]
        private PieceGraveyardManager _graveyardManager;

        private int _checkpointIndex = -1;
        public event Action OnHistoryUpdate;

        public List<Checkpoint> GetHistory()
        {
            var copy = new List<Checkpoint>();
            foreach (var checkpoint in _checkpoints)
            {
                var checkpointClone = checkpoint.Clone() as Checkpoint;
                copy.Add(checkpointClone);
            }
            return copy;
        }
        
        public int CheckpointIndex { get => _checkpointIndex; }

        public void AddCheckpoint(Checkpoint checkpoint)
        {
            _checkpointIndex++; //Verificar si no es la ultima posicion, para eliminar los restantes
            if (_checkpointIndex < _checkpoints.Count)
            {
                _checkpoints.RemoveRange(_checkpointIndex, _checkpoints.Count - _checkpointIndex);
            }
            _checkpoints.Add(checkpoint);
            Notify();
        }
        
        public void Undo()
        {
            if (_checkpointIndex <= 0) return;
            var checkpoint = _checkpoints[_checkpointIndex];
            //Deshacer el movimiento del checkpoint actual
            var movedPiece = checkpoint.MovedPiece.Piece;
            movedPiece.Position = checkpoint.MovedPiece.FromPosition;
            movedPiece.Container.transform.position = checkpoint.MovedPiece.FromTransformPosition;
            checkpoint.DeadPiece.Container.transform.position = checkpoint.MovedPiece.ToTransformPosition;
            checkpoint.DeadPiece.Position = checkpoint.MovedPiece.ToPosition;
            //ir al nuevo checkpoint actual y decirle al chessmanager que actualize su board, turn y lastpiece segun este checkpoint
            _checkpointIndex--;
            var lastCheckpoint = _checkpoints[_checkpointIndex];
            _chessManager.SetGameState(lastCheckpoint);
            Notify();
        }

        public void Redo()
        {
            if (_checkpointIndex < _checkpoints.Count - 1) return;
            //Rehacer el movimiento del checkpoint actual
            var checkpoint = _checkpoints[_checkpointIndex];
            var movedPiece = checkpoint.MovedPiece.Piece;
            movedPiece.Position = checkpoint.MovedPiece.ToPosition;
            movedPiece.Container.transform.position = checkpoint.MovedPiece.ToTransformPosition;
            checkpoint.DeadPiece.Container.transform.position = checkpoint.MovedPiece.ToTransformPosition;
            checkpoint.DeadPiece.Position = checkpoint.MovedPiece.ToPosition;
            _checkpointIndex++;
            var nextCheckpoint = _checkpoints[_checkpointIndex];
            _chessManager.SetGameState(nextCheckpoint);
            Notify();
        }

        public void Subscribe(Action callback)
        {
            OnHistoryUpdate += callback;
        }

        public void Unsubscribe(Action callback)
        {
            OnHistoryUpdate -= callback;
        }

        void Notify()
        {
            List<string> notationsString = new List<string>();
            foreach (var notation in _checkpoints)
            {
                notationsString.Add(notation.ToString());
            }

            OnHistoryUpdate?.Invoke();
        }
    }
}