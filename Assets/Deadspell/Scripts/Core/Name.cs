using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Deadspell.Core
{
    public class Name : SerializedMonoBehaviour
    {
        public enum Obfuscations
        {
            None,
            Spell,
            Potion,
        }
        
        [LabelText("Name")]
        public string DName;

        public Obfuscations Obfuscation;
    }
}