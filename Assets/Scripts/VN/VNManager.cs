using Gmtk.Manager;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField]
        private Image _speaker;

        [SerializeField]
        private Sprite _mayor, _fireChief, _mafia, _rancher, _guyd;

        private Story _story;

        private bool _isWaitingForNext;

        public bool IsShowingIntro { private set; get; } = true;

        public TutorialProgress Progress { set; get; }

        private string _sceIndex1,_sceIndex2;

        private Dictionary<string, string[]> _scenarios = new()
        {
            { "mayor", new[] { "host_event" } },
            { "fire_chief", new[] { "forest_fire", "zoo_escape" } },
            { "mafia", new[] { "turf_war", "wedding" } },
            { "rancher", new[] { "lost", "fence" } }
        };

        private Dictionary<string, Job> _jobs = new()
        {
            { "builder", Job.Builder },
            { "cook", Job.Cook },
            { "firefighter", Job.Firefighter },
            { "cat", Job.Cat },
            { "cowboy", Job.Cowboy },
            { "fixer", Job.Fixer },
            { "warrior", Job.Warrior }
        };
        public Job GetJob(string key) => _jobs[key];

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
                _sceIndex1 = _scenarios.Keys.ElementAt(Random.Range(0, _scenarios.Count));
                _sceIndex2 = _scenarios[_sceIndex1][Random.Range(0, _scenarios[_sceIndex1].Count())];
                _story.ChoosePathString($"{_sceIndex1}.{_sceIndex2}");
            }
            IsShowingIntro = true;
        }

        public void DisplayGameover()
        {
            _story.ChoosePathString("guyd_bot.try_again");
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
            _speaker.gameObject.SetActive(false);
            foreach (var tag in _story.currentTags)
            {
                var s = tag.Split(' ');
                var content = string.Join(' ', s.Skip(1));
                switch (s[0])
                {
                    case "speaker":
                        _speaker.gameObject.SetActive(true);
                        _speaker.sprite = content switch
                        {
                            "mayor" => _mayor,
                            "fire_chief" => _fireChief,
                            "mafia" => _mafia,
                            "rancher" => _rancher,
                            "guyd" => _guyd,
                            _ => throw new System.NotImplementedException($"Unknown speaker {content}")
                        };
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
        public string[] PossibleBots => ((string)_story.variablesState["bot_type"]).Split(',').Select(x => x.Trim()).ToArray();

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
                AIManager.Instance.UpdateProgress();
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

    public enum Job
    {
        Builder,
        Cook,
        Firefighter,
        Cat,
        Cowboy,
        Fixer,
        Warrior
    }
}