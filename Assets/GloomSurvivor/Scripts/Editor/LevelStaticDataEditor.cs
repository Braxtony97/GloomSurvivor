using GloomSurvivor.Scripts.Infrastructure.StaticData;
using GloomSurvivor.Scripts.Logic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GloomSurvivor.Scripts.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners =
                    FindObjectsOfType<SpawnMarker>()
                    .Select(x => new EnemySpawnerData(x.GetComponent<UniqueIdEnemySpawner>().Id, x.MonsterTypeId, x.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }

            EditorUtility.SetDirty(target);
        }
    }
}
