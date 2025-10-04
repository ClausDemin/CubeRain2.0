using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Common.Spawners;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics
{
    public class CubeCounter : MonoBehaviour
    {
        private const string _initialValue = "0";

        [SerializeField] private TextMeshProUGUI _overallCubesText;
        [SerializeField] private TextMeshProUGUI _createdCubesText;
        [SerializeField] private TextMeshProUGUI _activeCubesText;

        private void Start()
        {
            _overallCubesText.text = _initialValue;
            _createdCubesText.text = _initialValue;
            _activeCubesText.text= _initialValue;
        }

        public void UpdateOverallCubesCount(string count) 
        {
            _overallCubesText.text = count;
        }

        public void UpdateCreatedCubesCount(string count) 
        {
            _createdCubesText.text = count;
        }

        public void UpdateActiveCubesCount(string count) 
        { 
            _activeCubesText.text = count;
        }
    }
}
