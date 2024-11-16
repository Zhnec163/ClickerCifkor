using System;
using System.Globalization;
using Scripts.Config;
using Scripts.Model;
using Scripts.Spawner;
using Scripts.View;
using UniRx;
using UnityEngine.UI;

namespace Scripts.Presenter
{
    public class WalletPresenter : Presenter
    {
        private const int RoundingDigit = 2;

        public WalletPresenter(
            Wallet wallet,
            TextView walletView,
            Button button,
            Energy energy,
            FlyingTextSpawner flyingTextSpawner,
            IncomeRecorder incomeRecorder,
            BalanceConfig balanceConfig)
        {
            wallet.Balance.ObserveEveryValueChanged(balance => balance.Value)
                .Subscribe(balance => walletView.UpdateText(balance.ToString(CultureInfo.InvariantCulture)))
                .AddTo(CompositeDisposable);

            button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (!energy.TrySubtract(balanceConfig.EnergyPerTap))
                        return;

                    var incomeForTime = incomeRecorder.GetIncomeSumFor(balanceConfig.IncomeTime);
                    var sumModifier = incomeForTime == 0 ? 1 : incomeForTime;
                    var income = (float)Math.Round(
                        balanceConfig.BaseCurrencyPerTap + balanceConfig.Modifier +
                        sumModifier / balanceConfig.Divider, RoundingDigit);
                    wallet.AddBalance(income);
                    flyingTextSpawner.Spawn(income.ToString(CultureInfo.InvariantCulture)); 
                })
                .AddTo(CompositeDisposable);
        }
    }
}