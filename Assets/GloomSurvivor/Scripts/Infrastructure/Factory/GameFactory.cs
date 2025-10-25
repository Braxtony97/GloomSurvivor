using Assets.GloomSurvivor.Scripts.UI.Elements;
using Assets.GloomSurvivor.Scripts.UI.Services;
using GloomSurvivor.Scripts.Characters.Enemy;
using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Infrastructure.AssetManagment;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using GloomSurvivor.Scripts.Infrastructure.StaticData;
using GloomSurvivor.Scripts.Logic;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using GloomSurvivor.Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace GloomSurvivor.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; set; }

        private readonly IAssetProvider _assetProvider;
        private IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly IWindowService _windowService;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData, IPersistentProgressService progressService, IWindowService windowService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _progressService = progressService;
            _windowService = windowService;
        }

        public GameObject CreateHero(GameObject initPoint)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, initPoint.transform.position);
            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);
            
            LootCounterUI lootCounterUI = hud.GetComponentInChildren<LootCounterUI>();
            lootCounterUI.Construct(_progressService.Progress.WorldData);

            foreach (OpenWindowButton button in hud.GetComponentsInChildren<OpenWindowButton>())
                button.Construct(_windowService);       

            return hud;
        }

        public LootHandler CreateLoot()
        {
            LootHandler loot = InstantiateRegistered(AssetPath.LootPath)
                .GetComponent<LootHandler>();
            
            loot.Construct(_progressService.Progress.WorldData);
            
            return loot;
        }

        public void CreateSpawner(EnemySpawnerData spawnerData)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.SpawnerPath, spawnerData.Position)
                .GetComponent<SpawnPoint>();

            spawner.Construct(this);

            spawner.Id = spawnerData.Id;
            spawner.MonsterTypeId = spawnerData.Type;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            var gameObject = _assetProvider.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            var gameObject = _assetProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }

        public GameObject CreateMonster(Enums.MonsterTypeId monsterTypeId, Transform parent)
        {
            MonsterStaticData monsterStaticData = _staticData.GetMonster(monsterTypeId);
            GameObject monster = Object.Instantiate(monsterStaticData.Prefab, parent.position, Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();

            health.CurrentHealth = monsterStaticData.Hp;
            health.MaxHealth = monsterStaticData.Hp;
            
            monster.GetComponent<ActorHpBarUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject);
            monster.GetComponent<RotateToPlayer>()?.Construct(HeroGameObject);
            monster.GetComponent<NavMeshAgent>().speed = monsterStaticData.MoveSpeed;

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterStaticData.MinLoot, monsterStaticData.MaxLoot);
            lootSpawner.Construct(this);

            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject);
            attack.Damage = monsterStaticData.Damage;
            attack.EffectiveDistance = monsterStaticData.EffectiveDistance;

            return monster;
        }

        public void RestoreLoot(Progress progress)
        {
            progress.WorldData.WorldLootData.ActiveLoots.RemoveAll(loot => loot.Picked);

            foreach (var lootData in progress.WorldData.WorldLootData.ActiveLoots)
            {
                if (!lootData.Picked)
                {
                    LootHandler loot = CreateLoot();
                    loot.transform.position = lootData.Position;
                    loot.InitializeWithExistingData(lootData, new Loot { Value = lootData.Value }); 
                }
            }
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}