using System;

[Serializable]
public class User: NewUser
{
    public string _id;

    public User()
    {
        _id = "";
        CharacterName = "";
        UserAuthenticationId = "";
        Character = new CharacterBaseModel();
    }
}
