using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics
{
    public class SpawnerStatisticsPanel: MonoBehaviour
    {
        private CubeSpawnerPresenter _cubeSpawnerPresenter;
        private BombSpawnerPresenter _bombSpawnerPresenter;

        [Inject]
        private void Construct(CubeSpawnerPresenter cubeSpawnerPresenter, BombSpawnerPresenter bombSpawnerPresenter) 
        {
            _cubeSpawnerPresenter = cubeSpawnerPresenter;
            _bombSpawnerPresenter = bombSpawnerPresenter;
        }
    }
}
