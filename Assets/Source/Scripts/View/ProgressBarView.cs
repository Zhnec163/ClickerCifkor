using UnityEngine;
using UnityEngine.UI;

namespace Scripts.View
{
    [RequireComponent(typeof(Slider))]
    public class ProgressBarView : TextView
    {
        private Slider _slider;

        public void Init(float maxValue)
        {
            _slider = GetComponent<Slider>();
            _slider.maxValue = maxValue;
        }

        public void UpdateSlider(float value) =>
            _slider.value = value;
    }
}