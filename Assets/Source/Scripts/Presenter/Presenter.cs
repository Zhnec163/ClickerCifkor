using System;
using UniRx;

namespace Scripts.Presenter
{
    public abstract class Presenter : IDisposable
    {
        protected readonly CompositeDisposable CompositeDisposable = new();

        public virtual void Dispose() =>
            CompositeDisposable.Dispose();
    }
}