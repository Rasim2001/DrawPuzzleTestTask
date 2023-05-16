using System.IO;
using System.Linq;
using System.Xml;
using Infastructure.Character;
using Infastructure.StaticData.Character;
using Infastructure.StaticData.Level;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;
            
            if (GUILayout.Button("Collect"))
            {
                levelData.CharacterSpawners =
                    FindObjectsOfType<SpawnMarker>()
                        .Select(x =>
                            new CharacterSpawnerData(x.SexId, x.transform.position))
                        .ToList();

                string sceneActiveName = SceneManager.GetActiveScene().name;
                levelData.LevelKey = sceneActiveName;

                var sceneIndex = SceneManager.GetSceneByName(sceneActiveName).buildIndex;
                string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndex + 1);
                string nextSceneName = Path.GetFileNameWithoutExtension(scenePath);

                levelData.NextLevelKey = nextSceneName;
            }

            EditorUtility.SetDirty(target);
        }
    }
}