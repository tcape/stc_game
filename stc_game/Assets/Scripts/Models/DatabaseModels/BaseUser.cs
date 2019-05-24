using System;
using System.Collections.Generic;

[Serializable]
public class BaseUser
{
    public string Name;
    public string UserAuthenticationId;
    public List<CharacterBaseModel> Characters;
    public GameState GameState;
    
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
}