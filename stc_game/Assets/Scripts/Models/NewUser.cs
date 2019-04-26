using System;

[Serializable]
public class NewUser
{
    public string CharacterName;
    public CharacterBaseModel Character;
    public string UserAuthenticationId;

    public NewUser()
    {
        CharacterName = "";
        UserAuthenticationId = "";
        Character = new CharacterBaseModel();
    }

    public NewUser(string characterName, string userAuthenticationId)
    {
        CharacterName = characterName;
        UserAuthenticationId = userAuthenticationId;
        Character = new CharacterBaseModel();
    }
}