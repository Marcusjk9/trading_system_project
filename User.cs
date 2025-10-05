namespace App;

class User
{
  public string Username;
  public string _password;
  List<Items> inventory = new List<Items>();

  public User() { }
  public User(string username, string password)
  {
    Username = username;
    _password = password;
  }

  public bool TryLogin(string username, string password)
  {
    return username == Username && password == _password;
  }

  public void AddItem(Items item)
  {
    inventory.Add(item);
  }

  public void ShowInventory()
  {
    if (inventory.Count == 0)
    {
      Console.WriteLine("Inventory is empty...");
      return;
    }

    int i = 1;
    foreach (var item in inventory)
    {
      Console.WriteLine($"{i++}. {item.Weapon} | {item.Skin} ({item.Wear})");
    }
  }

  public List<Items> GetInventory()
  {
    return inventory;
  }
}