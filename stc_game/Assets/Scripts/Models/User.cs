using System;

[Serializable]
public class User
{
    public string _id;
    public string CharacterName;
    public CharacterBaseModel Character;
    public string UserAuthenticationId;

    public User()
    {
        _id = "";
        CharacterName = "";
        UserAuthenticationId = "";
        Character = new CharacterBaseModel();
    }
}