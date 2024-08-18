﻿using Gmtk.Manager;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        [SerializeField]
        private Animator _blastDoor;

        [SerializeField]
        private TMP_Text _timerText;

        private float _timer;
        private const float TimerRef = 10f;

        private GameState _state = GameState.Playing;

        public bool DidRoundEnd => _state != GameState.Playing;

        private void Awake()
        {
            Instance = this;
            _timer = TimerRef;
            _timerText.text = "Press any key to start";
        }

        private void Update()
        {
            if (_state == GameState.RoundEnd || !PlayerManager.Instance.HasJoin) return;

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _timerText.text = "Time Out!";
                switch (_state)
                {
                    case GameState.Playing:
                        _blastDoor.SetTrigger("Open");
                        _state = GameState.RoundEnd;
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
