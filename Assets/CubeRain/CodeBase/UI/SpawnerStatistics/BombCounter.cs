using TMPro;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics
{
    public class BombCounter : MonoBehaviour
    {
        private const string _initialValue = "0";

        [SerializeField] private TextMeshProUGUI _overallBombsText;
        [SerializeField] private TextMeshProUGUI _createdBombsText;
        [SerializeField] private TextMeshProUGUI _activeBombsText;

        private void Start()
        {
            _overallBombsText.text = _initialValue;
            _createdBombsText.text = _initialValue;
            _activeBombsText.text = _initialValue;
        }

        public void UpdateOverallBombsCount(string count)
        {
            _overallBombsText.text = count;
        }

        public void UpdateCreatedBombsCount(string count)
        {
            _createdBombsText.text = count;
        }

        public void UpdateActiveBombsCount(string count)
        {
            _activeBombsText.text = count;
        }
    }
}
