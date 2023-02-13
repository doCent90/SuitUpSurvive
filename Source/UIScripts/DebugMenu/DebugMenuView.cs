using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Source
{
    public class DebugMenuView : MonoBehaviour
    {
        private const float Step = 0.02f;

        private NavMeshAgent _playerAgent;
        [SerializeField] private InputHandler _inputHandler;

        [SerializeField] private TMP_Text _fps;
        [SerializeField] private TMP_Text _playerSpeed;

        private readonly float _hudRefreshRate = 1f;

        private float _timer;

        private void Start()
        {
            Invoke(nameof(Initialize), 0.1f);
        }

        private void Update()
        {
            if(_playerAgent == null)
                return;

            if (Time.unscaledTime > _timer)
            {
                int fps = (int)(1f / Time.unscaledDeltaTime);
                _fps.text = "FPS: " + fps;
                _timer = Time.unscaledTime + _hudRefreshRate;
            }

            _playerSpeed.text = $"Player Speed: {_playerAgent.speed}";

            SetSpeed();
        }

        private void Initialize()
        {
            _playerAgent = FindObjectOfType<PlayerPresenter>().GetComponent<NavMeshAgent>();
        }

        private void SetSpeed()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                _playerAgent.speed += Step;

            if (Input.GetKeyDown(KeyCode.DownArrow))
                _playerAgent.speed -= Step;
        }
    }
}
