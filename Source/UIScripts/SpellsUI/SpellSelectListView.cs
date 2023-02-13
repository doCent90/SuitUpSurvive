using System.Collections.Generic;

namespace Assets.Source
{
    public class SpellSelectListView : ISpellChooseListView
    {
        private readonly SpellButtonPresenter _spellSpecialButtonPresenterFirst;
        private readonly SpellButtonPresenter _spellSpecialButtonPresenterSecond;
        private readonly SpellButtonPresenter _spellSpecialButtonPresenterThird;
        private readonly SpellButtonPresenter _spellDefaultButtonPresenterFirst;
        private readonly SpellButtonPresenter _spellDefaultButtonPresenterSecond;
        private readonly SpellButtonPresenter _spellDefaultButtonPresenterThird;
        private readonly ISpellMenuWindow _gameStateWnindowsUI;
        private ISpellButton _spellButton;

        public SpellSelectListView(ISpellMenuWindow gameStateWnindowsUI, SpellButtonPresenter specialBbuttonFirst, SpellButtonPresenter specialButtonSecond, SpellButtonPresenter specialButtonThird,
            SpellButtonPresenter defaultButtonFirst, SpellButtonPresenter defaultButtonSecond, SpellButtonPresenter defaultButtonThird)
        {
            _gameStateWnindowsUI = gameStateWnindowsUI;
            _spellSpecialButtonPresenterFirst = specialBbuttonFirst;
            _spellSpecialButtonPresenterSecond = specialButtonSecond;
            _spellSpecialButtonPresenterThird = specialButtonThird;
            _spellDefaultButtonPresenterFirst = defaultButtonFirst;
            _spellDefaultButtonPresenterSecond = defaultButtonSecond;
            _spellDefaultButtonPresenterThird = defaultButtonThird;
        }

        public void SubscribeButtons(ISpellButton spellButton)
        {
            _spellButton = spellButton;
            _spellSpecialButtonPresenterFirst.Button.onClick.AddListener(OnSpecialFirstButtonCliked);
            _spellSpecialButtonPresenterSecond.Button.onClick.AddListener(OnSpecialSecondButtonCliked);
            _spellSpecialButtonPresenterThird.Button.onClick.AddListener(OnSpecialThirdButtonCliked);
            _spellDefaultButtonPresenterFirst.Button.onClick.AddListener(OnDefaultFirstButtonCliked);
            _spellDefaultButtonPresenterSecond.Button.onClick.AddListener(OnDefaultSecondButtonCliked);
            _spellDefaultButtonPresenterThird.Button.onClick.AddListener(OnDefaultThirdButtonCliked);
        }

        public void UnSubscribeButtons()
        {
            _spellSpecialButtonPresenterFirst.Button.onClick.RemoveListener(OnSpecialFirstButtonCliked);
            _spellSpecialButtonPresenterSecond.Button.onClick.RemoveListener(OnSpecialSecondButtonCliked);
            _spellSpecialButtonPresenterThird.Button.onClick.RemoveListener(OnSpecialThirdButtonCliked);
            _spellDefaultButtonPresenterFirst.Button.onClick.RemoveListener(OnDefaultFirstButtonCliked);
            _spellDefaultButtonPresenterSecond.Button.onClick.RemoveListener(OnDefaultSecondButtonCliked);
            _spellDefaultButtonPresenterThird.Button.onClick.RemoveListener(OnDefaultThirdButtonCliked);
        }

        public void OpenSpellMenu(IReadOnlyList<Spell> specialSpells, IReadOnlyList<Spell> defaultSpells)
        {
            _gameStateWnindowsUI.ShowSpecialSpellSelectMenu();

            _spellDefaultButtonPresenterFirst.SetValue(defaultSpells[0].Icon, defaultSpells[0].Name, defaultSpells[0].Description);
            _spellDefaultButtonPresenterSecond.SetValue(defaultSpells[1].Icon, defaultSpells[1].Name, defaultSpells[1].Description);
            _spellDefaultButtonPresenterThird.SetValue(defaultSpells[2].Icon, defaultSpells[2].Name, defaultSpells[2].Description);

            if(specialSpells == null)
            {
                _gameStateWnindowsUI.ShowDefaultSpellSelectMenu();
            }
            else
            {
                _spellSpecialButtonPresenterFirst.SetValue(specialSpells[0].Icon, specialSpells[0].Name, specialSpells[0].Description);
                _spellSpecialButtonPresenterSecond.SetValue(specialSpells[1].Icon, specialSpells[1].Name, specialSpells[1].Description);
                _spellSpecialButtonPresenterThird.SetValue(specialSpells[2].Icon, specialSpells[2].Name, specialSpells[2].Description);
            }
        }

        private void OnSpecialFirstButtonCliked()
        {
            _spellButton.OnButtonSelectSpecialSpellClicked(0);
            _gameStateWnindowsUI.ShowDefaultSpellSelectMenu();
        }

        private void OnSpecialSecondButtonCliked()
        {
            _spellButton.OnButtonSelectSpecialSpellClicked(1);
            _gameStateWnindowsUI.ShowDefaultSpellSelectMenu();
        }

        private void OnSpecialThirdButtonCliked()
        {
            _spellButton.OnButtonSelectSpecialSpellClicked(2);
            _gameStateWnindowsUI.ShowDefaultSpellSelectMenu();
        }

        private void OnDefaultFirstButtonCliked()
        {
            _spellButton.OnButtonSelectDefaultSpellClicked(0);
            _gameStateWnindowsUI.HideSpellSelectMenu();
        }

        private void OnDefaultSecondButtonCliked()
        {
            _spellButton.OnButtonSelectDefaultSpellClicked(1);
            _gameStateWnindowsUI.HideSpellSelectMenu();
        }

        private void OnDefaultThirdButtonCliked()
        {
            _spellButton.OnButtonSelectDefaultSpellClicked(2);
            _gameStateWnindowsUI.HideSpellSelectMenu();
        }
    }
}
