using System.Collections;
using Infastructure.AssetProvider;
using UnityEngine;

namespace Infastructure.Character
{
    public class MoveAlongPath : MonoBehaviour
    {
        [SerializeField] private CharacterTrigger _character;
        [SerializeField] private Animator _animator;

        private readonly float _defaultSpeedAnim = 1f;

        public DrawPath DrawPath;
        public float MoveSpeed;

        private int _currentPoint = 0;
        private Coroutine _moveCoroutine;

        public void StartMove()
        {
            _moveCoroutine = StartCoroutine(Move());
            _animator.speed = MoveSpeed;
        }

        private IEnumerator Move()
        {
            while (_currentPoint < DrawPath.points.Count - 1 && _character != null)
            {
                if (Vector2.Distance(transform.position, DrawPath.points[_currentPoint]) < 0.1f)
                    _currentPoint++;

                if (_currentPoint == DrawPath.points.Count - 1)
                    _character.NotifyToVictory();


                _character.transform.position = Vector2.MoveTowards(transform.position, DrawPath.points[_currentPoint],
                    MoveSpeed * Time.deltaTime);

                yield return null;
            }
        }

        public void StopMove()
        {
            _animator.speed = _defaultSpeedAnim;
            StopCoroutine(_moveCoroutine);
        }
    }
}