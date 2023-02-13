using UnityEngine;
using Source.Extensions;

namespace Assets.Source
{
    public class GameStateWnindowsUI : IGameStateWindowsUI, ISpellMenuWindow
    {
        private readonly CanvasGroup _aim;
        private readonly CanvasGroup _barsGroup;
        private readonly CanvasGroup _winWindow;
        private readonly CanvasGroup _loseWindow;
        private readonly CanvasGroup _defaultSpellWindow;
        private readonly CanvasGroup _specialSpellWindow;
        private readonly IInputHadler _inputHadler;

        public GameStateWnindowsUI(CanvasGroup winWindow, CanvasGroup loseWindow, CanvasGroup barsGroup, CanvasGroup aim, IInputHadler inputHadler, CanvasGroup defaultSpellWindow, CanvasGroup specialSpellWindow)
        {
            _aim = aim;
            _barsGroup = barsGroup;
            _winWindow = winWindow;
            _loseWindow = loseWindow;
            _inputHadler = inputHadler;
            _defaultSpellWindow = defaultSpellWindow;
            _specialSpellWindow = specialSpellWindow;
        }

        public void OnStarted()
        {
            _inputHadler.UnlockControlKeys();
            _barsGroup.EnableFade();
            ChangeCursor(isAimShow: true);
        }

        public void ShowSpecialSpellSelectMenu()
        {
            SwitchWindows(isOpen: true);
            _specialSpellWindow.EnableGroup();
        }

        public void ShowDefaultSpellSelectMenu()
        {
            _specialSpellWindow.DisableGroup();
            _defaultSpellWindow.EnableGroup();
        }

        public void HideSpellSelectMenu()
        {
            SwitchWindows(isOpen: false);
            _defaultSpellWindow.DisableGroup();
        }

        public void ShowWin()
        {
            SwitchWindows(isOpen: true);
            _winWindow.EnableGroup();
        }

        public void ShowLose()
        {
            SwitchWindows(isOpen: true);
            _loseWindow.EnableGroup();
        }

        private void SwitchWindows(bool isOpen)
        {
            if (isOpen)
            {
                ChangeCursor(isAimShow: false);
                _barsGroup.DisableGroup();
                _inputHadler.LockControlKeys();
            }
            else
            {
                ChangeCursor(isAimShow: true);
                _barsGroup.EnableFade();
                _inputHadler.UnlockControlKeys();
            }
        }

        private void ChangeCursor(bool isAimShow)
        {
            if (isAimShow)
            {
                _aim.EnableFade();
                Cursor.visible = false;
            }
            else
            {
                _aim.DisableGroup();
                Cursor.visible = true;
            }            
        }
    }
}
