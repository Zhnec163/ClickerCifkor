using Scripts.Config;
using Scripts.Model;
using Scripts.View;
using UniRx;

namespace Scripts.Presenter
{
    public class AutoCollectorPresenter : Presenter
    {
        private readonly AutoCollector _autoCollector;

        public AutoCollectorPresenter(
            AutoCollector autoCollector,
            ProgressBarView autoCollectorView,
            Wallet wallet,
            BalanceConfig balanceConfig)
        {
            _autoCollector = autoCollector;
            _autoCollector.Progress.ObserveEveryValueChanged(progress => progress.Value)
                .Subscribe(value =>
                {
                    autoCollectorView.UpdateText(value.ToString());
                    autoCollectorView.UpdateSlider(value);
                })
                .AddTo(CompositeDisposable);

            _autoCollector.TimePassed
                .Subscribe(_ => wallet.AddBalance(balanceConfig.CurrencyPerAutoCollect))
                .AddTo(CompositeDisposable);
        }

        public override void Dispose()
        {
            base.Dispose();
            _autoCollector.Dispose();
        }
    }
}