using Scripts.View;
using UnityEngine;
using UnityEngine.Pool;

namespace Scripts.Spawner
{
    public class FlyingTextSpawner : MonoBehaviour
    {
        [SerializeField] private FlyingTextView _prefab;

        private ObjectPool<FlyingTextView> _pool;
        private Vector2 _spawnPosition;

        public void Init(Vector2 spawnPosition)
        {
            _spawnPosition = spawnPosition;
            _pool = new ObjectPool<FlyingTextView>(
                createFunc: OnCreate,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease);
        }

        public void Spawn(string text)
        {
            var flyingTextView = _pool.Get();
            flyingTextView.ResetPosition();
            flyingTextView.UpdateText(text);
            flyingTextView.Animate(OnAnimationEnded);
        }

        private void OnAnimationEnded(FlyingTextView flyingTextView) =>
            _pool.Release(flyingTextView);

        private FlyingTextView OnCreate()
        {
            var flyingTextView = Instantiate(_prefab, transform);
            flyingTextView.Init(_spawnPosition);
            return flyingTextView;
        }

        private void OnGet(FlyingTextView flyingTextView) =>
            flyingTextView.gameObject.SetActive(true);

        private void OnRelease(FlyingTextView flyingTextView) =>
            flyingTextView.gameObject.SetActive(false);
    }
}