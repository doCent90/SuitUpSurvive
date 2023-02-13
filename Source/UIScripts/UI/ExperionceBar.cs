namespace Assets.Source
{
    public class ExperionceBar : Bar
    {
        readonly IExperionceBarView _experionceBarView;

        public ExperionceBar(IExperionceBarView healthsBarView) => _experionceBarView = healthsBarView;

        protected override void SetValue(float normilizeValue, float currentValue, float maxValue) => _experionceBarView.OnExperionceValueChanged(normilizeValue, currentValue, maxValue);
    }
}
