using Gmtk.Manager;
using Gmtk.Map;
using Gmtk.SO.Part;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Gmtk.Menu
{
    public class DispenserItemSelector : MonoBehaviour
    {
        private int _index;

        private Dispenser _target;

        private APartInfo[] _parts;

        [SerializeField]
        private TMP_Text _name;

        public void Init(Dispenser dispenser)
        {
            _target = dispenser;
            _index = 0;
            _parts = ResourcesManager.Instance.AllAvailableParts.OrderBy(x => x.Name).ToArray();
            UpdateUI();
        }

        public void OnNext()
        {
            _index++;
            if (_index == _parts.Length) _index = 0;
            UpdateUI();
        }

        public void OnPrev()
        {
            _index--;
            if (_index == -1) _index = _parts.Length - 1;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _name.text = _parts[_index].Name;
        }

        public void OnConfirm()
        {
            _target.SetPart(_parts[_index]);
        }
    }
}
