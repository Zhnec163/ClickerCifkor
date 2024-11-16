using Scripts.Model;
using Scripts.View;
using UniRx;

namespace Scripts.Presenter
{
    public class EnergyPresenter : Presenter
    {
        private readonly Energy _energy;

        public EnergyPresenter(Energy energy, ProgressBarView energyView)
        {
            _energy = energy;
            _energy.Point.ObserveEveryValueChanged(point => point.Value)
                .Subscribe(value =>
                {
                    energyView.UpdateText(value.ToString());
                    energyView.UpdateSlider(value);
                })
                .AddTo(CompositeDisposable);
        }

        public override void Dispose()
        {
            base.Dispose();
            _energy.Dispose();
        }
    }
}