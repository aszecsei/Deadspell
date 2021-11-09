namespace Deadspell.Services
{
    public interface IUIService
    {
        struct CharacterDetails
        {
            public string Name;
            public int Level;
            public int CurrentHealth;
            public int MaxHealth;
            public int CurrentMana;
            public int MaxMana;
        }

        void SetCharacterDetails(CharacterDetails details);
        void SetCharacterLocation(string location);
    }
}