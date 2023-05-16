using System;
using System.Collections;
using UnityEngine;

namespace Infastructure.Character
{
    public class FxPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _hitTextFX;
        [SerializeField] private GameObject _hitBackGroundFX;

        [SerializeField] private GameObject _victoryFx;
        [SerializeField] private Vector3 _offsetVixtoryFx;
        
        [SerializeField] private float _timeActive;

        private bool _fxIsPlayed;

        public void StartHitPlayFx(Action onLoaded) =>
            StartCoroutine(PlayHitFx(onLoaded));

        public void StartVictoryPlayFx(Action onLoaded) =>
            StartCoroutine(PlayVictoryFx(onLoaded));

        private IEnumerator PlayHitFx(Action onLoaded)
        {
            var hitTextPrefab = Instantiate(_hitTextFX, transform.position, Quaternion.identity);
            var hitBackgroundPrefab = Instantiate(_hitBackGroundFX, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_timeActive);

            onLoaded?.Invoke();
            Destroy(hitTextPrefab);
            Destroy(hitBackgroundPrefab);
        }

        private IEnumerator PlayVictoryFx(Action onLoaded)
        {
            var _victoryFxPrefab = Instantiate(_victoryFx, transform.position + _offsetVixtoryFx, _victoryFx.transform.rotation);
            yield return new WaitForSeconds(_timeActive);
            onLoaded?.Invoke();
            Destroy(_victoryFxPrefab);
        }
    }
}