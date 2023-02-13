using System;
using UnityEngine;

namespace Assets.Source
{
    public class Weapon : IAttack, IGameObjectsPlayBackHandler, IWeapon
    {
        private readonly IBulletsPool _bulletsPool;
        private readonly WeaponAttacker _weaponAttacker;
        private readonly MassiveAttackModule _massiveAttackModule;

        private WeaponData _weaponData;

        public WeaponData WeaponData => _weaponData;
        public int CountMassiveAttack { get; private set; } = 1;

        public Weapon(WeaponData weaponData, Transform startPoint, Transform directionPoint, IBulletsPool bulletsPool, ICoroutineRunner coroutineRunner)
        {
            _weaponData = weaponData;
            _bulletsPool = bulletsPool;
            _massiveAttackModule = new MassiveAttackModule(directionPoint);
            _weaponAttacker = new WeaponAttacker(coroutineRunner, bulletsPool, this, startPoint, directionPoint, _massiveAttackModule);
        }

        public void Resume() => _weaponAttacker.Resume();
        public void Pause() => _weaponAttacker.Pause();
        public void StartAttack() => _weaponAttacker.StartAttack();
        public void Attack(Action onAction) => _weaponAttacker.Attack(onAction);
        public void StopAttack() => _weaponAttacker.StopAttack();

        public void IncreaseDamage(float value) => _weaponData.Damage += value;
        public void IncreaseBulletSpeed(float value) => _weaponData.BulletSpeed += value;
        public void IncreasePushPower(float value) => _weaponData.PushPower += value;
        public void DecreaseColldown(float value) => _weaponData.Cooldown += value;
        public void DecreaseColldownMissiles(float value) => _weaponData.CooldownBeetwenMissiles += value;
        public void IncreaseRange(float value) => _weaponData.Range += value;

        public void AddedMassiveAttack(float value)
        {
            CountMassiveAttack = (int)value;
            _massiveAttackModule.AddedMassiveAttack(CountMassiveAttack);
            _bulletsPool.IncreasePool(WeaponData.BulletCount + CountMassiveAttack);
        }

        public void IncreaseBulletsCount(int value)
        {
            _weaponData.BulletCount += value;
            _bulletsPool.IncreasePool(WeaponData.BulletCount + CountMassiveAttack);
        }
    }
}
