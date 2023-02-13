namespace Assets.Source
{
    public class GameState : ISpawnerGameState, IPlayerGameState
    {
        private IEnemySpawner _enemySpawner;
        private IPlayerTransition _playerStateTransition;
        private IGameStateWindowsUI _gameStateWindowsUI;
        private bool _isPlayerDead = false;

        public void Initialize(IEnemySpawner enemySpawner, IPlayerTransition playerStateTransition, IGameStateWindowsUI gameStateWindowsUI)
        {
            _enemySpawner = enemySpawner;
            _playerStateTransition = playerStateTransition;
            _gameStateWindowsUI = gameStateWindowsUI;
        }

        public void OnPlayerWin()
        {
            if(_isPlayerDead == false)
            {
                _playerStateTransition.OnWin();
                _gameStateWindowsUI.ShowWin();
            }
        }

        public void OnPlayerDead()
        {
            _isPlayerDead = true;
            _enemySpawner.OnPlayerDead();
            _gameStateWindowsUI.ShowLose();
        }
    }
}
