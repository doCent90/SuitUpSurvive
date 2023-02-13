namespace Assets.Source
{
    public class PlayerUpgradeHandler : IPlayerUpgradeHadler
    {
        private readonly IWeapon _weapon;
        private readonly IPlayerMover _playerMover;
        private readonly IPlayerStatesUpgrade _playerStateUpgrade;
        private readonly IPlayerCollectTrigger _playerCollectTrigger;

        public PlayerUpgradeHandler(IWeapon weapon, IPlayerMover playerMover, IPlayerStatesUpgrade playerStateUpgrade, IPlayerCollectTrigger playerCollectTrigger)
        {
            _weapon = weapon;
            _playerMover = playerMover;
            _playerStateUpgrade = playerStateUpgrade;
            _playerCollectTrigger = playerCollectTrigger;
        }

        public void OnUdgraded(Spell spell)
        {
            SpellData spellData = spell.SpellData;

            SetWeaponData(spellData);
            SetDefenceData(spellData);
            SetPlayerStateData(spellData);
        }

        private void SetWeaponData(SpellData spellData)
        {
            switch (spellData.AttackSpellVariants.AttackSpellVariants)
            {
                case AttackSpellVariants.MassiveAttack1lvl:
                    _weapon.AddedMassiveAttack(spellData.AttackSpellVariants.Value);
                    break;
                default:
                    break;
            }
        }

        private void SetDefenceData(SpellData spellData)
        {
            switch (spellData.DefenceSpellVariants.DefenceSpellVariants)
            {
                case DefenceSpellVariants.EnergyField1lvl:
                    _playerMover.OnUpgraded(spellData.DefenceSpellVariants.Value);
                    break;
                default:
                    break;
            }
        }

        private void SetPlayerStateData(SpellData spellData)
        {
            foreach (PlayerStatesData statesVariant in spellData.PlayerStatsDataList)
            {
                switch (statesVariant.PlayerStatesVariants)
                {
                    case PlayerStatesVariants.Speed:
                        _playerMover.OnUpgraded(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.Armor:
                        _playerStateUpgrade.OnArmorUpgraded(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.Heathls:
                        _playerStateUpgrade.OnMaxHealthUpgraded(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.CollectRange:
                        _playerCollectTrigger.IcreaseRadius(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.ExperienceMultiplier:
                        _playerStateUpgrade.OnExperienceUpGraded(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.MoneyMultiplier:
                        _playerStateUpgrade.OnMoneyMiltiUpgraded(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.BulletsCount:
                        _weapon.IncreaseBulletsCount((int)statesVariant.Value);
                        break;
                    case PlayerStatesVariants.Damage:
                        _weapon.IncreaseDamage(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.BulletsSpeed:
                        _weapon.IncreaseBulletSpeed(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.PushPower:
                        _weapon.IncreasePushPower(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.CoolDown:
                        _weapon.DecreaseColldown(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.CoolDownBetweenMissiles:
                        _weapon.DecreaseColldownMissiles(statesVariant.Value);
                        break;
                    case PlayerStatesVariants.Range:
                        _weapon.IncreaseRange(statesVariant.Value);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
