using UniRx;

namespace Scripts.Model
{
    public class Wallet
    {
        public readonly ReactiveCommand<float> Increased = new();

        private readonly ReactiveProperty<float> _balance = new();

        public IReadOnlyReactiveProperty<float> Balance => _balance;

        public void AddBalance(float amount)
        {
            _balance.Value += amount;
            Increased.Execute(amount);
        }
    }
}