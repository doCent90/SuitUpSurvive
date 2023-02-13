using UnityEngine;

namespace Assets.Source
{
    public abstract class Bar : IBar
    {
        public void OnValueChanged(float currentValue, float maxValue)
            => SetValue(GetNormalizeValue(currentValue, maxValue), currentValue, maxValue);

        protected abstract void SetValue(float normilizeValue, float currentValue, float maxValue);

        private float GetNormalizeValue(float currentValue, float maxValue)
        {
            float normilize = 0;
            return Mathf.InverseLerp(normilize, maxValue, currentValue);
        }
    }
}
