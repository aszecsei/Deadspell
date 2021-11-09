using System;
using Deadspell.Data;
using Deadspell.Features;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Deadspell.Core
{
    public class GameManager : SerializedMonoBehaviour
    {
        public static GameManager Instance;

        public Services.Services Services;
        public Entitas.Systems Systems;
        public Faction PlayerFaction;
        public Faction EnemyFaction;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            var contexts = Contexts.sharedInstance;
            Systems = new Feature("Systems")
                .Add(new ServiceRegistrationSystems(contexts, Services))
                .Add(new TutorialSystems(contexts)) // TODO: Delete this eventually
                .Add(new InputSystems(contexts))
                .Add(new GameplaySystems(contexts))
                .Add(new RenderingSystems(contexts));
            
            Systems.Initialize();
        }

        void Update()
        {
            Systems.Execute();
            Systems.Cleanup();
        }
    }
}