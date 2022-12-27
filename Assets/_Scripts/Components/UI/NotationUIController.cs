using System.Collections.Generic;
using Game._Scripts.Core.Notations;
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
        [SerializeField] private List<NotationData> _notationsData = new List<NotationData>();

        private void OnEnable()
        {
            historyManager.Subscribe(NotationHandler);
        }

        private void OnDisable()
        {
            historyManager.Unsubscribe(NotationHandler);
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
                        + checkpoint.MovedPiece.ToPosition.ToString();
                    continue;
                }
                else
                {
                    //TODO jugo black
                    notationData.blackMove =
                        checkpoint.MovedPiece.Piece.Sign
                        + (checkpoint.DeadPiece is Empty ? "" : "x")
                        + checkpoint.MovedPiece.ToPosition.ToString();
                    _notationsData.Add(notationData);
                    moveCounter++;
                }
            }

            foreach (var data in _notationsData)
            {
                var notationGO = _notationUISinglePooler.GetPooledObject();
                _notations.Add(notationGO);
                notationGO.SetActive(true);
                notationGO.GetComponent<NotationUISingle>()
                    ?.SetNotationText(
                        data.moved, data.whiteMove + " " + data.blackMove
                        );
            }
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