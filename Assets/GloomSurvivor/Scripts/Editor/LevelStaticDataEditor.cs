using Assets.GloomSurvivor.Scripts.Logic.LevelTransferTriggerSpawner;
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
        private const string InitialPointTag = "InitialPoint";
        private const string LevelTransferTriggerTag = "LevelTransferTrigger";

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

                levelData.InitialPointHero = GameObject.FindWithTag(InitialPointTag).transform.position;

                levelData.LevelTransferSpawners =
                    FindObjectsOfType<LevelTransferMarker>()
                    .Select(x => new LevelTransferSpawnerData(x.GetComponent<UniqueIdLevelTransferSpawner>().Id, x.Scene, x.transform.position))
                    .ToList();
            }

            EditorUtility.SetDirty(target);
        }
    }
}
