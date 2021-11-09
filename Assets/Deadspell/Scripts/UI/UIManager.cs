using Deadspell.Services;
using Sirenix.OdinInspector;
using TMPro;

namespace Deadspell.UI
{
    public class UIManager : SerializedMonoBehaviour, IUIService
    {
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Location;
        public TextMeshProUGUI Stats;

        public void SetCharacterDetails(IUIService.CharacterDetails details)
        {
            Name.text = $"{details.Name} <color=grey>Lvl: {details.Level}</color>";
            Stats.text =
                $"HP: <color=red>{details.CurrentHealth}/{details.MaxHealth}</color> Mana: <color=#0080ff>{details.CurrentMana}/{details.MaxMana}</color>";
        }

        public void SetCharacterLocation(string location)
        {
            Location.text = location;
        }
    }
}