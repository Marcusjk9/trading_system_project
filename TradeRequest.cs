namespace App;

// Klassen här för att kunna skicka tradeoffers mellan varandra.
// Den lagrar information om vem som skickat traden, vem som mottar den, vilka items som erbjuds samt efterfrågas och statusen på traden.
class TradeRequest
{
  public User FromUser { get; set; }
  public User ToUser { get; set; }

  public List<Items> OfferedItems { get; set; } = new List<Items>();
  public List<Items> RequestedItems { get; set; } = new List<Items>();

  public bool IsCompleted { get; set; } = false;
  public bool IsAccepted { get; set; } = false;

  // Tom konstruktor som behövs när man laddar från fil
  public TradeRequest() { }

  // Konstruktor som används när man skapar en ny trade i programmet
  public TradeRequest(User fromUser, User toUser, List<Items> offeredItems)
  {
    FromUser = fromUser;
    ToUser = toUser;
    OfferedItems = offeredItems;
    RequestedItems = new List<Items>();
  }

  // Visar info om traden i konsolen
  public void ShowRequest()
  {
    Console.WriteLine($"{FromUser.Username} wants to trade: ");
    foreach (var item in OfferedItems)
    {
      Console.WriteLine($" {item.Weapon} | {item.Skin}  ({item.Wear})");
    }

    Console.WriteLine("For your item(s):");
    foreach (var item in RequestedItems)
    {
      Console.WriteLine($" {item.Weapon} | {item.Skin}  ({item.Wear})");
    }
  }

  // acceptar trade
  public void Accept()
  {
    // Om traden redan hanterats, så gör inget
    if (IsCompleted)
      return;

    // Flytta dom items som erbjöds från FromUser till ToUser
    var yourItems = new List<Items>(OfferedItems);
    foreach (var item in yourItems)
    {
      if (FromUser.GetInventory().Contains(item))
      {
        FromUser.GetInventory().Remove(item);
        ToUser.AddItem(item);
      }
    }

    // Flytta de items som mottagaren gick med på att byta
    var theirItems = new List<Items>(RequestedItems);
    foreach (var item in theirItems)
    {
      if (ToUser.GetInventory().Contains(item))
      {
        ToUser.GetInventory().Remove(item);
        FromUser.AddItem(item);
      }
    }

    // Markera traden som slutförd och accepterad
    IsAccepted = true;
    IsCompleted = true;
  }

  // Om mottagaren nekar traden
  public void Deny()
  {
    if (IsCompleted)
      return;

    // Markera den som avslutad men inte accepterad
    IsAccepted = false;
    IsCompleted = true;
  }
}