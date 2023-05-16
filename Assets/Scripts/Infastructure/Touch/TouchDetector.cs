using System.Collections.Generic;
using Infastructure.AssetProvider;
using Infastructure.Character;
using Infastructure.Data;
using Infastructure.Toilet;
using UnityEngine;

namespace Infastructure.Touch
{
    public class TouchDetector : MonoBehaviour
    {
        private readonly List<MoveAlongPath> _moveCharacters = new List<MoveAlongPath>();
        private readonly List<ToiletWC> _toilets = new List<ToiletWC>();

        private readonly float _maxDistance = 10f;

        private DrawPath _drawPath;
        private MoveAlongPath _moveAlongPath;
        private CharacterTrigger _character;
        private FinishTimeCalculator _finishTimeCalculator;
        private Camera _camera;

        private int _maxCharacters = 0;
        
        [SerializeField]
        private int _characterLayerMask;
        private int _toiletLayerMask;
        private bool _isDrawing = false;

        public void Construct(FinishTimeCalculator finishTimeCalculator, int maxCharacters)
        {
            _finishTimeCalculator = finishTimeCalculator;
            _maxCharacters = maxCharacters;
        }

        private void Start()
        {
            _camera = Camera.main;

            _characterLayerMask = 1 << LayerMask.NameToLayer(AssetsPath.CharacterLayerMask);
            _toiletLayerMask = 1 << LayerMask.NameToLayer(AssetsPath.ToiletLayerMask);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TryToBuildPath();

            if (_isDrawing)
            {
                Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;

                _drawPath.StartDrawing(mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
                StopBuildPath();
        }

        private void TryToBuildPath()
        {
            var mouseDownPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = DetectTouch(mouseDownPosition, _characterLayerMask);

            if (hit.collider != null)
            {
                _drawPath = hit.collider.GetComponentInChildren<DrawPath>();
                _moveAlongPath = hit.collider.GetComponentInChildren<MoveAlongPath>();
                _character = hit.collider.GetComponent<CharacterTrigger>();

                if (!_drawPath.IsPathReady)
                    _isDrawing = true;
            }
        }

        private void StopBuildPath()
        {
            var mouseUpPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = DetectTouch(mouseUpPosition, _toiletLayerMask);

            if (IsWrongPath())
                return;

            if (hit.collider != null && _character != null)
                ToiletSelection(hit);
            else if (_drawPath != null && !_drawPath.IsPathReady)
                _drawPath.StopDrawing();

            CheckFinish();
            Cleanup();
            
            _isDrawing = false;
        }

        private void Cleanup()
        {
            _character = null;
            _drawPath = null;
            _moveAlongPath = null;
        }

        private void ToiletSelection(RaycastHit2D hit)
        {
            var toilet = hit.collider.GetComponent<ToiletWC>();
            
            if ((_character.SexId == toilet.SexId || toilet.SexId == SexId.Gender) && !toilet.IsBusy)
            {
                _moveCharacters.Add(_moveAlongPath);
                _toilets.Add(toilet);
                _drawPath.IsPathReady = true;
                toilet.IsBusy = true;
            }
            else if(!_drawPath.IsPathReady)
                _drawPath.StopDrawing();
        }

        private void CheckFinish()
        {
            if (_moveCharacters.Count == _maxCharacters)
            {
                ResetToilet();
                _finishTimeCalculator.CalculateTimeForAll(_moveCharacters);
            }
        }

        
        private void ResetToilet()
        {
            foreach (ToiletWC toilet in _toilets)
                toilet.IsBusy = false;
        }

        private bool IsWrongPath()
        {
            if (_drawPath != null)
            {
                if (_drawPath.LineWrongPath.positionCount > 0)
                {
                    _drawPath.StopWrongDrawing();
                    _drawPath.StopDrawing();
                    _isDrawing = false;

                    return true;
                }
            }

            return false;
        }


        private RaycastHit2D DetectTouch(Vector3 mouseDownPosition, LayerMask layerMask) =>
            Physics2D.Raycast(mouseDownPosition, Vector2.zero, _maxDistance, layerMask);
    }
}