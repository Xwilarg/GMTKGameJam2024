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
            if (_display.IsDisplayDone && _story != null &&
                _story.canContinue && // There is text left to write
                !_story.currentChoices.Any())
            {
                _isWaitingForNext = true;
                StartCoroutine(WaitAndDisplay());
            }
        }

        private IEnumerator WaitAndDisplay()
        {
            yield return new WaitForSeconds(1f);
            DisplayStory(_story.Continue());
            _isWaitingForNext = false;
        }
    }
}