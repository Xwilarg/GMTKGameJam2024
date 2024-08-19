using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gmtk.VN
{
    public class VNManager : MonoBehaviour
    {
        public static VNManager Instance { private set; get; }

        [SerializeField]
        private TextAsset _storyText;

        [SerializeField]
        private TextDisplay _display;

        [SerializeField]
        private GameObject _area1Built, _area2Built, _area1Old, _area2Old;

        private Story _story;

        private bool _isWaitingForNext;

        public bool IsShowingIntro { private set; get; } = true;

        public TutorialProgress Progress { set; get; }

        private Dictionary<string, string[]> scenarios = new()
        {
            { "mayor", new[] { "host_event" } },
            { "fire_chief", new[] { "forest_fire", "zoo_escape" } },
            { "mafia", new[] { "turf_war", "wedding" } },
            { "rancher", new[] { "lost", "fence" } }
        };

        public void NextMission()
        {
            if (Progress == TutorialProgress.SingleBot)
            {
                _area1Built.SetActive(true);
                _area1Old.SetActive(false);
            }
            else if (Progress == TutorialProgress.MultiBots)
            {
                _area2Built.SetActive(true);
                _area2Old.SetActive(false);
            }

            if (Progress != TutorialProgress.Game)
            {
                Progress++;
            }
            if (Progress == TutorialProgress.MultiBots)
            {
                _story.ChoosePathString("guyd_bot.tutorial");
            }
            else
            {
                _story.ChoosePathString("customer.kitty");
            }
            IsShowingIntro = true;
        }

        private void Awake()
        {
            Instance = this;

            _area1Built.SetActive(false);
            _area2Built.SetActive(false);
            _area1Old.SetActive(true);
            _area2Old.SetActive(true);
        }

        public void StartTutorial()
        {
            _story = new(_storyText.text);
            DisplayStory(_story.Continue());
        }

        private void DisplayStory(string text)
        {
            foreach (var tag in _story.currentTags)
            {
                var s = tag.Split(' ');
                var content = string.Join(' ', s.Skip(1));
                switch (s[0])
                {
                    case "speaker":
                        // TODO
                        break;
                }
            }
            _display.ToDisplay = text;
        }

        private void Update()
        {
            if (IsShowingIntro && _story != null && !_isWaitingForNext)
            {
                if (_display.IsDisplayDone)
                {
                    _isWaitingForNext = true;
                    StartCoroutine(WaitAndDisplay());
                }
                else if (_display.IsOngoingDone)
                {
                    StartCoroutine(WaitAndForceDisplay());
                    _isWaitingForNext = true;
                }
            }
        }

        public int Objective => (int)_story.variablesState["bot_number"];

        private IEnumerator WaitAndForceDisplay()
        {
            yield return new WaitForSeconds(1f);
            _display.ForceDisplay();
            _isWaitingForNext = false;
        }

        private IEnumerator WaitAndDisplay()
        {
            yield return new WaitForSeconds(1f);
            if (_story.canContinue)
            {
                DisplayStory(_story.Continue());
            }
            else
            {
                IsShowingIntro = false;
            }
            _isWaitingForNext = false;
        }
    }

    public enum TutorialProgress
    {
        SingleBot,
        MultiBots,
        Game
    }
}