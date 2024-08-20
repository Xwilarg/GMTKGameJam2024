using Gmtk.Manager;
using Gmtk.Map;
using Gmtk.SO;
using Gmtk.SO.Part;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gmtk.Menu
{
    public class DispenserItemSelector : MonoBehaviour
    {
        private int _index;

        private Dispenser _target;

        private APartInfo[] _parts;

        [SerializeField]
        private TMP_Text _name, _description;

        [SerializeField]
        private Image _icon;

        public void Init(Dispenser dispenser)
        {
            _target = dispenser;
            _index = 0;
            _parts = ResourcesManager.Instance.AllAvailableParts.OrderBy(x =>
            {
                string cmp;
                if (x is CPUInfo) cmp = "1_";
                else if (x is WheelInfo) cmp = "2_";
                else cmp = "3_";
                return cmp + x.Name;
            }).ToArray();
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
            _description.text = _parts[_index].Description;
            _icon.sprite = _parts[_index].Icon;
        }

        public void OnConfirm()
        {
            _target.SetPart(_parts[_index]);
        }
    }
}
