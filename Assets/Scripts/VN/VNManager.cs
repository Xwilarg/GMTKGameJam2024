using Ink.Runtime;
using System.Collections;
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

        private Story _story;

        private bool _isWaitingForNext;

        public bool IsShowingIntro { private set; get; } = true;

        public TutorialProgress Progress { set; get; }

        public void NextMission()
        {
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
                _story.ChoosePathString("customer.commission");
            }
            IsShowingIntro = true;
        }

        private void Awake()
        {
            Instance = this;
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
            if (IsShowingIntro && _display.IsDisplayDone && _story != null && !_isWaitingForNext)
            {
                _isWaitingForNext = true;
                StartCoroutine(WaitAndDisplay());
            }
        }

        public int Objective => (int)_story.variablesState["bot_number"];

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