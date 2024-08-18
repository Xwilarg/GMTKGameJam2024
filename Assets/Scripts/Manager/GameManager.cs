using Gmtk.Manager;
using Gmtk.VN;
using TMPro;
using UnityEngine;

namespace Gmtk.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        [SerializeField]
        private Animator _blastDoor;

        [SerializeField]
        private TMP_Text _timerText;

        private float _timer;
        private const float TimerRef = 20f;

        private GameState _state = GameState.Playing;

        public bool DidRoundEnd => _state != GameState.Playing;

        private void Awake()
        {
            Instance = this;
            _timer = TimerRef;
            _timerText.text = "Press any key to start";
        }

        public void StartNextRound()
        {
            _state = GameState.Playing;
            _timer = TimerRef;
            _blastDoor.SetTrigger("Close");
        }

        public void StartIntro()
        {
            _timerText.text = "Listen to the mayor!";
        }

        private void Update()
        {
            if (_state == GameState.RoundEnd || VNManager.Instance.IsShowingIntro) return;

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _timerText.text = "Time Out!";
                switch (_state)
                {
                    case GameState.Playing:
                        _state = GameState.RoundEnd;
                        _blastDoor.SetTrigger("Open");
                        AIManager.Instance.EndRound();
                        break;

                    default: throw new System.NotImplementedException();
                }
            }
            else
            {
                _timerText.text = $"{_timer:00}:{Mathf.FloorToInt(_timer % 1 * 10f):0}";
            }
        }
    }

    public enum GameState
    {
        Playing,
        RoundEnd
    }
}
