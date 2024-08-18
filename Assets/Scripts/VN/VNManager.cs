using Ink.Runtime;
using System.Linq;
using UnityEditor.VersionControl;
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

        private void Awake()
        {
            Instance = this;
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
            if (_display.IsDisplayDone &&
                _story.canContinue && // There is text left to write
                !_story.currentChoices.Any())
            {
                DisplayStory(_story.Continue());
            }
        }
    }
}