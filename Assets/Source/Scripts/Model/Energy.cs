using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Scripts.Model
{
    public class Energy : IDisposable
    {
        private const int IncrementTimeStep = 1;

        private readonly int _maxPoint;
        private readonly ReactiveProperty<int> _point = new();
        private readonly ReactiveProperty<bool> _isFull = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public Energy(int currentPoint, int maxPoint)
        {
            _point.Value = currentPoint;
            _maxPoint = maxPoint;

            if (_point.Value < _maxPoint)
                StartIncrementing();

            _isFull
                .Where(isFull => !isFull)
                .Subscribe(_ => StartIncrementing());
        }

        public IReadOnlyReactiveProperty<int> Point => _point;

        public bool TrySubtract(int amount)
        {
            int newPoint = _point.Value - amount;

            if (newPoint < 0)
                return false;

            _point.Value = newPoint;
            _isFull.Value = false;
            return true;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _isFull.Dispose();
        }

        private void StartIncrementing() =>
            _ = Incrementing();

        private async UniTask Incrementing()
        {
            while (_point.Value < _maxPoint)
            {
                await UniTask.WaitForSeconds(IncrementTimeStep, false, PlayerLoopTiming.Update,
                    _cancellationTokenSource.Token);
                _point.Value++;
            }

            _isFull.Value = true;
        }
    }
}