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
}
