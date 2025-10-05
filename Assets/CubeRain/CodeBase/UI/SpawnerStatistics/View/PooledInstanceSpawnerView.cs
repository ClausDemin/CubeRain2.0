using Assets.CubeRain.CodeBase.UI.SpawnerStatistics.View.Interface;
using System;
using TMPro;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics.View
{
    public class PooledInstanceSpawnerView : MonoBehaviour, ICounterView
    {
        private const string _initialValue = "0";

        [SerializeField] private TextMeshProUGUI _overallText;
        [SerializeField] private TextMeshProUGUI _createdText;
        [SerializeField] private TextMeshProUGUI _activeText;

        private void Start()
        {
            _overallText.text = _initialValue;
            _createdText.text = _initialValue;
            _activeText.text = _initialValue;
        }

        public void UpdateOverallCount(string count)
        {
            _overallText.text = count;
        }

        public void UpdateCreatedCount(string count)
        {
            _createdText.text = count;
        }

        public void UpdateActiveCount(string count)
        {
            _activeText.text = count;
        }
    }
}
