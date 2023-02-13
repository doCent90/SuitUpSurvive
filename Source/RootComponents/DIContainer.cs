using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Source
{
    public class DIContainer : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public InputHandler InputHandler { get; private set; }
        [field: SerializeField] public CameraInitializer CameraInitializer { get; private set; }
        [field: SerializeField] public DebugMenuView DebugMenuView { get; private set; }
        [field: SerializeField] public AimInWorldSpaceOrientation AimInWorldSpaceOrientation { get; private set; }
        [field: SerializeField] public GameUIPresenter GameUIPresenter { get; private set; }

        [field: SerializeField] public WeaponPresenter WeaponPresenterPrefab { get; private set; }
        [field: SerializeField] public PlayerPresenter PlayerPresenterPrefab { get; private set; }
        [field: SerializeField] public EnemySpawnerPresenter EnemiesSpawnerPrefab { get; private set; }
        [field: SerializeField] public CollectablesSpawnerPresenter CollectablesSpawnerPrefab { get; private set; }

        [field: SerializeField] public WeaponConfig WeaponConfig { get; private set; }
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        [field: SerializeField] public EnemySpawnerConfig SpawnerConfig { get; private set; }
        [field: SerializeField] public SpellsListConfig SpellsConfig { get; internal set; }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Camera == null)
                throw new NullReferenceException();

            InputHandler = FindObjectOfType<InputHandler>();
            CameraInitializer = FindObjectOfType<CameraInitializer>();
            DebugMenuView = FindObjectOfType<DebugMenuView>();
            AimInWorldSpaceOrientation = FindObjectOfType<AimInWorldSpaceOrientation>();
            GameUIPresenter = FindObjectOfType<GameUIPresenter>();
        }
#endif
    }
}
