namespace Assets.Source
{
    public interface IExperionceBarView
    {
        IBar ExperionceBar { get; }
        void OnExperionceValueChanged(float normilizeValue, float currentValue, float maxValue);
    }
}
