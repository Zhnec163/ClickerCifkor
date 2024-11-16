using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Scripts.Model
{
    public class IncomeRecorder : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly Dictionary<float, float> _incomes = new();

        public void Init(Wallet wallet)
        {
            wallet.Increased.Subscribe(Record)
                .AddTo(_compositeDisposable);
        }

        public void Dispose() =>
            _compositeDisposable.Dispose();

        private void Record(float value) =>
            _incomes.Add(Time.time, value);

        public float GetIncomeSumFor(int time)
        {
            return _incomes
                .Where(entry => entry.Key > Time.time - time)
                .Sum(entry => entry.Value);
        }
    }
}