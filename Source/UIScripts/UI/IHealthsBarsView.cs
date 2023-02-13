namespace Assets.Source
{
    public interface IHealthsBarsView
    {
        IBar HeathlsBar { get; }
        void OnHealthsValueChanged(float normilizeValue, float currentValue, float maxValue);
    }
}
