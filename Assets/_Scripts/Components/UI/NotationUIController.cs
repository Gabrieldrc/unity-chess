using System.Collections.Generic;
using Game._Scripts.Core.Notations;
using Game.Core.GameStates;
using Game.Core.Pieces;
using Game.Managers;
using UnityEngine;

namespace Game.Components.UI
{
    public class NotationUIController : MonoBehaviour
    {
        [SerializeField] private HistoryManager historyManager;
        [SerializeField] private ObjectPooler _notationUISinglePooler;
        [SerializeField] private List<GameObject> _notations = new List<GameObject>();
        private List<NotationData> _notationsData = new List<NotationData>();

        private void OnEnable()
        {
            historyManager.Subscribe(NotationHandler);
        }

        private void OnDisable()
        {
            historyManager.Unsubscribe(NotationHandler);
        }

        public void UpdateNotations()
        {
            NotationHandler();
        }

        private void NotationHandler()
        {
            //TODO index arrow
            ResetNotations();
            var history = historyManager.GetHistory();
            var moveCounter = 0;
            NotationData notationData = new NotationData();
            foreach (var checkpoint in history)
            {
                if (moveCounter <= 0)
                {
                    moveCounter++;
                    continue;
                }
                if (checkpoint.Turn == PieceColor.Black)
                {
                    //TODO jugo white
                    notationData = new NotationData();
                    notationData.moved = moveCounter;
                    notationData.whiteMove =
                        checkpoint.MovedPiece.Piece.Sign
                        + (checkpoint.DeadPiece is Empty ? "" : "x")
                        + checkpoint.MovedPiece.ToPosition.ToString()
                        + SetStateSign(checkpoint);
                    _notationsData.Add(notationData);
                    
                    continue;
                }
                else
                {
                    //TODO jugo black
                    notationData.blackMove =
                        checkpoint.MovedPiece.Piece.Sign
                        + (checkpoint.DeadPiece is Empty ? "" : "x")
                        + checkpoint.MovedPiece.ToPosition.ToString()
                        + SetStateSign(checkpoint);
                    _notationsData[_notationsData.Count - 1] = notationData;
                    moveCounter++;
                }
            }

            for (int i = 0; i < _notationsData.Count; i++)
            {
                var data = _notationsData[i];
                var notationGO = _notationUISinglePooler.GetPooledObject();
                _notations.Add(notationGO);
                notationGO.SetActive(true);
                var notationUISingle = notationGO.GetComponent<NotationUISingle>();
                if (notationUISingle != null)
                {
                    notationUISingle
                        .SetNotationText(
                            data.moved, data.whiteMove + " " + data.blackMove
                            );
                    notationUISingle.SetColor(i % 2);
                }
            }
        }

        private string SetStateSign(Checkpoint checkpoint)
        {
            var sign = "";
            if (checkpoint.CurrentState is CheckState)
            {
                sign = "+";
            }
            else if (checkpoint.CurrentState is CheckMateState)
            {
                sign = "#" ;
                sign += (checkpoint.MovedPiece.Piece.Color == PieceColor.Black ? "0-1" : "1-0");
            }
            return sign;
        }

        private void ResetNotations()
        {
            foreach (var notation in _notations)
            {
                notation.SetActive(false);
            }
            _notations.Clear();
            _notationsData.Clear();
        }
    }
}