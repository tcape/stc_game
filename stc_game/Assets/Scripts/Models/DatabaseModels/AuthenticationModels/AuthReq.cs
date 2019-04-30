using System;

[Serializable]
public class AuthReq
{
    public string email;
    public string password;

    public AuthReq(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}
