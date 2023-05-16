using System;
using Infastructure.AssetProvider;
using Infastructure.Data;
using UnityEngine;

namespace Infastructure.Character
{
    public class CharacterTrigger : MonoBehaviour
    {
        [SerializeField] private FxPlayer _characterFx;
        [SerializeField] private MoveAlongPath _characterMove;

        public SexId SexId { get; set; }

        public Action OnWinHappened;
        public Action OnLossHappened;

        private int _characterLayerMask;
        private int _obstaclesLayerMask;


        private void Start()
        {
            _characterLayerMask = LayerMask.NameToLayer(AssetsPath.CharacterLayerMask);
            _obstaclesLayerMask = LayerMask.NameToLayer(AssetsPath.ObstacleLayerMask);
        }

        public void NotifyToVictory()
        {
            _characterMove.StopMove();
            _characterFx.StartVictoryPlayFx(() => OnWinHappened?.Invoke());
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == _characterLayerMask || col.gameObject.layer == _obstaclesLayerMask)
            {
                _characterMove.StopMove();
                _characterFx.StartHitPlayFx(() => OnLossHappened?.Invoke());
            }
        }

        private void OnDestroy() =>
            Destroy(gameObject);
    }
}