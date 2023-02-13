namespace Assets.Source
{
    public class HeathlsBar : Bar
    {
        readonly IHealthsBarsView _healthsBarView;

        public HeathlsBar(IHealthsBarsView healthsBarView) => _healthsBarView = healthsBarView;

        protected override void SetValue(float normilizeValue, float currentValue, float maxValue) => _healthsBarView.OnHealthsValueChanged(normilizeValue, currentValue, maxValue);
    }
}
