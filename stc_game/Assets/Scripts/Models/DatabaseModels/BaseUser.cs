using System;
using System.Collections.Generic;

[Serializable]
public class BaseUser
{
    public string Name;
    public string UserAuthenticationId;
    public List<CharacterBaseModel> Characters;
    
    public BaseUser()
    {
        Name = "";
        UserAuthenticationId = "";
        Characters = new List<CharacterBaseModel>();
    }

    public BaseUser(BaseUser user)
    {
        Name = user.Name;
        UserAuthenticationId = user.UserAuthenticationId;
        Characters = user.Characters;
    }

    public BaseUser(string name, string userAuthenticationId)
    {
        Name = name;
        UserAuthenticationId = userAuthenticationId;
        Characters = new List<CharacterBaseModel>();
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