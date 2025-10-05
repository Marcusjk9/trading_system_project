namespace App;

// Klassen är för att ha konton.
// Den håller koll på användarens namn, lösenord och inventory
class User
{
  public string Username;
  public string _password;

  List<Items> inventory = new List<Items>();

  // Tom konstruktor (behövs vid laddning från fil)
  public User() { }

  // Konstruktor som används när man gör en ny användare
  public User(string username, string password)
  {
    Username = username;
    _password = password;
  }

  // Kollar om användarnamn och lösenord matchar för inloggning
  public bool TryLogin(string username, string password)
  {
    return username == Username && password == _password;
  }

  // Lägger till ett nytt item i användarens inventory
  public void AddItem(Items item)
  {
    inventory.Add(item);
  }

  // Skriver ut användarens inventory i konsolen
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

  // Returnerar inventory så att andra delar av programmet kan använda den
  public List<Items> GetInventory()
  {
    return inventory;
  }
}