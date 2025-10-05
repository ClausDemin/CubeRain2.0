using Assets.CubeRain.CodeBase.Common.BombFeature;
using Assets.CubeRain.CodeBase.Common.CubeFeature;
using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics
{
    public class SpawnerStatisticsPanel: MonoBehaviour
    {
        private PooledInstanceSpawnerPresenter<Cube> _cubeSpawnerPresenter;
        private PooledInstanceSpawnerPresenter<Bomb> _bombSpawnerPresenter;

        [Inject]
        private void Construct(PooledInstanceSpawnerPresenter<Cube> cubeSpawnerPresenter, PooledInstanceSpawnerPresenter<Bomb> bombSpawnerPresenter) 
        {
            _cubeSpawnerPresenter = cubeSpawnerPresenter;
            _bombSpawnerPresenter = bombSpawnerPresenter;
        }

        private void OnDestroy()
        {
            _cubeSpawnerPresenter.Dispose();
            _bombSpawnerPresenter.Dispose();
        }
    }
}
