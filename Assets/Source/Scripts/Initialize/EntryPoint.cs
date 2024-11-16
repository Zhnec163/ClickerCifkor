using System;
using System.Collections.Generic;
using Scripts.Config;
using Scripts.Model;
using Scripts.Presenter;
using Scripts.Spawner;
using Scripts.View;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.Initialize
{
    public class EntryPoint : MonoBehaviour
    {
        private readonly List<IDisposable> _disposables = new();

        [SerializeField] private BalanceConfig _balanceConfig;
        [SerializeField] private FlyingTextSpawner _flyingTextSpawner;
        [SerializeField] private TextView _walletView;
        [SerializeField] private ProgressBarView _energyView;
        [SerializeField] private ProgressBarView _autoCollectView;
        [SerializeField] private Button _clickButton;

        private void Awake()
        {
            var wallet = new Wallet();
            var incomeRecorder = new IncomeRecorder();

            incomeRecorder.Init(wallet);
            _autoCollectView.Init(_balanceConfig.CollectTimeStep);
            _energyView.Init(_balanceConfig.MaxEnergy);
            _flyingTextSpawner.Init(_clickButton.transform.position);

            var energy = new Energy(_balanceConfig.CurrentEnergy, _balanceConfig.MaxEnergy);
            var autoCollector = new AutoCollector(_balanceConfig.CollectTimeStep);

            _disposables.Add(new WalletPresenter(wallet, _walletView, _clickButton, energy, _flyingTextSpawner, incomeRecorder, _balanceConfig));
            _disposables.Add(new EnergyPresenter(energy, _energyView));
            _disposables.Add(new AutoCollectorPresenter(autoCollector, _autoCollectView, wallet, _balanceConfig));
        }

        private void OnDestroy() =>
            _disposables.ForEach(disposable => disposable.Dispose());
    }
}