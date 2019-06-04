using System;

[Serializable]
public class User: BaseUser
{
    // Make all changes to the super BaseUser Class
    public string _id;

    public User() : base()
    {
        _id = "";
    }

    public User(BaseUser user) : base(user)
    {
        _id = "";
    }

    public User(string name, string userAuthId) : base(name, userAuthId)
    {
        _id = "";
    }

    public bool Exists()
    {
        if (UserAuthenticationId != "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SelectCharacter(HeroClass heroClass)
    {
        GetActiveCharacter().IsActive = false;
        GetCharacter(heroClass).IsActive = true;
    }

    private CharacterBaseModel GetCharacter(HeroClass heroClass)
    {
        
        return Characters.Find(character => character.HeroClass == heroClass);
    }

    public CharacterBaseModel GetActiveCharacter()
    {
        if (Characters.Count == 0)
        {
            InstantiateCharacters();
        }
        return Characters.Find(character => character.IsActive == true);
    }

    private void InstantiateCharacters()
    {
        CharacterBaseModel warrior = new CharacterBaseModel("Warrior", HeroClass.Warrior, true);
        CharacterBaseModel mage = new CharacterBaseModel("Mage", HeroClass.Mage);
        Characters.Add(warrior);
        Characters.Add(mage);
    }
}
