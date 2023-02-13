using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source
{
    public class GameUIPresenter : Presenter
    {
        [Header("AIM")]
        [SerializeField] private CanvasGroup _aimImage;
        [Header("__Bars__\nImages")]
        [SerializeField] private Image _heathlsImage;
        [SerializeField] private Image _experionsImage;
        [SerializeField] private CanvasGroup _barsGroup;
        [Header("Texts")]
        [SerializeField] private TMP_Text _heathlsText;
        [SerializeField] private TMP_Text _experionsText;
        [SerializeField] private TMP_Text _playerLevelText;
        [Header("__Windows__\nWin/Lose window")]
        [SerializeField] private CanvasGroup _winWindow;
        [SerializeField] private CanvasGroup _loseWindow;
        [SerializeField] private CanvasGroup _specialSpellWindow;
        [SerializeField] private CanvasGroup _defaultSpellWindow;
        [Header("Special Spell Select Buttons")]
        [SerializeField] private SpellButtonPresenter _firstSpecialSpellButton;
        [SerializeField] private SpellButtonPresenter _secondSpecialSpellButton;
        [SerializeField] private SpellButtonPresenter _thirdSpecialSpellButton;
        [Header("Default Spell Select Buttons")]
        [SerializeField] private SpellButtonPresenter _firstDefaultSpellButton;
        [SerializeField] private SpellButtonPresenter _secondDefaultSpellButton;
        [SerializeField] private SpellButtonPresenter _thirdDefaultSpellButton;

        private PlayerStatesView _playerStatesView;
        private GameStateWnindowsUI _gameStateWnindowsUI;
        private SpellChooseList _spellChooseList;
        private SpellSelectListView _spellChooseListView;

        public IBar HealthsBar => _playerStatesView.HeathlsBar;
        public IBar ExperionceBar => _playerStatesView.ExperionceBar;
        public IPlayerLevelView PlayerLevelView => _playerStatesView;
        public IGameStateWindowsUI GameStateWindowsUI => _gameStateWnindowsUI;
        public ISpellMenuOpen SpellMenuOpen => _spellChooseList;

        public void Construct(IInputHadler inputHadler, IReadOnlyList<SpellConfig> specialSpells, IReadOnlyList<SpellConfig> defaultSpells)
        {
            _playerStatesView = new PlayerStatesView(_heathlsImage, _experionsImage, _heathlsText, _experionsText, _playerLevelText);
            _gameStateWnindowsUI = new GameStateWnindowsUI(_winWindow, _loseWindow, _barsGroup, _aimImage, inputHadler, _defaultSpellWindow, _specialSpellWindow);
            _spellChooseListView = new SpellSelectListView(_gameStateWnindowsUI, _firstSpecialSpellButton, _secondSpecialSpellButton, _thirdSpecialSpellButton,
                _firstDefaultSpellButton, _secondDefaultSpellButton, _thirdDefaultSpellButton);
            _spellChooseList = new SpellChooseList(specialSpells, defaultSpells);
        }

        public void Initialize(IPlayerUpgradeHadler playerUpgradeHadler, IGameObjectsPlayBackHandler playBackHandler)
        {
            _spellChooseList.Initialize(playerUpgradeHadler, playBackHandler, _spellChooseListView);
            _spellChooseListView.SubscribeButtons(_spellChooseList);
        }

        public void OnStarted() => _gameStateWnindowsUI.OnStarted();

        private void OnDestroy() => _spellChooseListView.UnSubscribeButtons();
    }
}
