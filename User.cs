namespace App;

class Items
{
  public string Weapon { get; set; }
  public string Skin { get; set; }
  public string Wear { get; set; }


}

class User
{
  public string Username;
  public string _password;


  public User(string username, string password)
  {
    Username = username;
    _password = password;
  }
  public bool TryLogin(string username, string password)
  {
    return username == Username && password == _password;
  }

}