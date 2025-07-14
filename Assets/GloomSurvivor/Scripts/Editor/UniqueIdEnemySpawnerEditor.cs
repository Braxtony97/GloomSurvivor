using System;
using System.Linq;
using GloomSurvivor.Scripts.Logic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace GloomSurvivor.Scripts.Editor
{
    [CustomEditor(typeof(UniqueIdEnemySpawner))]
    public class UniqueIdEnemySpawnerEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueIdEnemySpawner) target;
            
            if (IsPrefab(uniqueId))
                return;

            if (string.IsNullOrEmpty(uniqueId.Id)) 
                Generate(uniqueId);
            else
            {
                UniqueIdEnemySpawner[] uniqueIdEnemySpawners = FindObjectsOfType<UniqueIdEnemySpawner>();
                
                if (uniqueIdEnemySpawners.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                    Generate(uniqueId);
            }
        }

        private bool IsPrefab(UniqueIdEnemySpawner uniqueId) => 
            uniqueId.gameObject.scene.rootCount == 0;

        private void Generate(UniqueIdEnemySpawner uniqueId)
        {
            uniqueId.Id = uniqueId.gameObject.scene.name + "_" + Guid.NewGuid().ToString();

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene); 
            }
        }
    }
}