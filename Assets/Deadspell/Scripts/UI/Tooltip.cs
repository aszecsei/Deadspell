using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace Deadspell.UI
{
    public class Tooltip : SerializedMonoBehaviour
    {
        public string Header;
        [TextArea]
        public string Content;

        public TextMeshProUGUI HeaderText;
        public TextMeshProUGUI ContentText;

        public void UpdateText()
        {
            HeaderText.text = Header;
            ContentText.text = Content;
        }
        
        void Update()
        {
            UpdateText();
        }
    }
}
