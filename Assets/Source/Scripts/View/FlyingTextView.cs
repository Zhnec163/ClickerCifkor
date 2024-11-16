using System;
using DG.Tweening;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.View
{
    public class FlyingTextView : TextView
    {
        [SerializeField] private float _flyDistance;
        [SerializeField] private float _duration;

        private Vector2 _spawnPosition;

        public void Init(Vector2 spawnPosition) =>
            _spawnPosition = spawnPosition;

        public void ResetPosition() =>
            transform.position = _spawnPosition;

        public void Animate(Action<FlyingTextView> animationEnded)
        {
            transform.DOMove(transform.position + RandomUtils.GetRandomDirection() * _flyDistance, _duration)
                .OnComplete(() => animationEnded?.Invoke(this));
        }
    }
}