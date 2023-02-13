using TMPro;
using System;
using UnityEngine.UI;

namespace Assets.Source
{
    public class PlayerStatesView : IHealthsBarsView, IExperionceBarView, IPlayerLevelView
    {
        private readonly Image _heathlsImage;
        private readonly Image _experionsImage;
        private readonly TMP_Text _heathlsText;
        private readonly TMP_Text _experionsText;
        private readonly TMP_Text _playerLevelText;

        private readonly HeathlsBar _heathlsBar;
        private readonly ExperionceBar _experionceBar;

        public IBar HeathlsBar => _heathlsBar;
        public IBar ExperionceBar => _experionceBar;

        public PlayerStatesView(Image heathlsImage, Image experionsImage, TMP_Text heathlsText, TMP_Text experionsText, TMP_Text playerLevelText)
        {
            _experionsImage = experionsImage;
            _heathlsImage = heathlsImage;
            _heathlsText = heathlsText;
            _experionsText = experionsText;
            _playerLevelText = playerLevelText;

            _heathlsBar = new HeathlsBar(this);
            _experionceBar = new ExperionceBar(this);
        }

        public void OnLevelChanged(int level) => _playerLevelText.text = level.ToString();

        public void OnHealthsValueChanged(float normilizeValue, float currentValue, float maxValue)
        {
            _heathlsImage.fillAmount = normilizeValue;
            _heathlsText.text = GetStrigValue(currentValue, maxValue);
        }

        public void OnExperionceValueChanged(float normilizeValue, float currentValue, float maxValue)
        {
            _experionsImage.fillAmount = normilizeValue;
            _experionsText.text = GetStrigValue(currentValue, maxValue);
        }

        private int GetRoundedValue(float value) => (int)MathF.Round(value);

        private string GetStrigValue(float currentValue, float maxValue)
            => $"{GetRoundedValue(currentValue)}/{GetRoundedValue(maxValue)}";
    }
}
