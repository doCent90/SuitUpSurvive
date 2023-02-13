using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Source
{
    public class SpellButtonPresenter : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        [field: SerializeField] public Button Button { get; private set; }

        public void SetValue(Sprite icon, string name, string description)
        {
            _icon.sprite = icon;
            _name.text = name;
            _description.text = description;
        }
    }
}
