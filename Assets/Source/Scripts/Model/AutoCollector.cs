using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Scripts.Model
{
    public class AutoCollector : IDisposable
    {
        private const int IncrementTimeStep = 1;

        public readonly ReactiveCommand TimePassed = new();

        private readonly float _collectTimeStep;
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly ReactiveProperty<int> _progress = new();

        public AutoCollector(float collectTimeStep)
        {
            _collectTimeStep = collectTimeStep;
            StatTimeCounting();
            TimePassed.Subscribe(_ => StatTimeCounting());
        }

        public IReadOnlyReactiveProperty<int> Progress => _progress;

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            TimePassed.Dispose();
        }

        private void StatTimeCounting() =>
            _ = TimeCounting();

        private async UniTask TimeCounting()
        {
            _progress.Value = 0;

            while (_progress.Value < _collectTimeStep)
            {
                await UniTask.WaitForSeconds(IncrementTimeStep, false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                _progress.Value++;
            }

            TimePassed.Execute();
        }
    }
}