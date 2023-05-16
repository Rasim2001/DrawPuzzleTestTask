using System.Collections.Generic;
using Infastructure.AssetProvider;
using UnityEngine;

namespace Infastructure.Character
{
    public class DrawPath : MonoBehaviour
    {
        public LineRenderer LineRender;
        public LineRenderer LineWrongPath;

        public bool IsPathReady;
        public Color PathColor;
        public List<Vector3> points = new List<Vector3>();


        private int _labyrinthWallLayerMask;
        private int _wrongPathIndex = 0;

        private void Start()
        {
            _labyrinthWallLayerMask = 1 << LayerMask.NameToLayer(AssetsPath.LabyrinthWallLayerMask);

            LineRender.startColor = PathColor;
            LineRender.endColor = PathColor;

            LineWrongPath.startColor = Color.black;
            LineWrongPath.endColor = Color.black;
        }

        public void StopDrawing()
        {
            points.Clear();
            LineRender.positionCount = 0;
        }


        public void StopWrongDrawing()
        {
            LineWrongPath.positionCount = 0;
            _wrongPathIndex = 0;
        }

        public void StartDrawing(Vector3 mousePosition)
        {
            if (points.Count == 0)
                StartCorrectDrawing(mousePosition);
            else
            {
                if (Vector3.Distance(mousePosition, points[points.Count - 1]) > 0.1f)
                {
                    if (!IsObstacleBetween(mousePosition, points[points.Count - 1]))
                    {
                        StartCorrectDrawing(mousePosition);
                        StopWrongDrawing();
                    }
                    else
                        StartWrongDrawing(mousePosition);
                }
            }
            
        }

        private void StartCorrectDrawing(Vector3 mousePosition)
        {
            points.Add(mousePosition);
            LineRender.positionCount = points.Count;
            LineRender.SetPosition(points.Count - 1, mousePosition);
        }

        private void StartWrongDrawing(Vector3 mousePosition)
        {
            _wrongPathIndex++;
            LineWrongPath.positionCount = _wrongPathIndex;
            LineWrongPath.SetPosition(_wrongPathIndex - 1, mousePosition);
        }

        private bool IsObstacleBetween(Vector2 start, Vector2 end)
        {
            RaycastHit2D hit = Physics2D.Linecast(start, end, _labyrinthWallLayerMask);
            return hit.collider != null;
        }
    }
}