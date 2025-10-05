namespace App;

class TradeRequest
{
  public User FromUser { get; set; }
  public User ToUser { get; set; }
  public List<Items> OfferedItems { get; set; } = new List<Items>();
  public List<Items> RequestedItems { get; set; } = new List<Items>();
  public bool IsCompleted { get; set; } = false;
  public bool IsAccepted { get; set; } = false;

  public TradeRequest() { }
  public TradeRequest(User fromUser, User toUser, List<Items> offeredItems)
  {
    FromUser = fromUser;
    ToUser = toUser;
    OfferedItems = offeredItems;
    RequestedItems = new List<Items>();
  }

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
  public void Accept()
  {
    if (IsCompleted)

    {
      return;
    }

    var yourItems = new List<Items>(OfferedItems);
    foreach (var item in yourItems)
    {
      if (FromUser.GetInventory().Contains(item))
      {
        FromUser.GetInventory().Remove(item);
        ToUser.AddItem(item);

      }
    }

    var theirItems = new List<Items>(RequestedItems);
    foreach (var item in theirItems)
    {
      if (ToUser.GetInventory().Contains(item))
      {
        ToUser.GetInventory().Remove(item);
        FromUser.AddItem(item);

      }
    }
    IsAccepted = true;
    IsCompleted = true;
  }

  public void Deny()
  {
    if (IsCompleted)
    {
      return;
    }

    IsAccepted = false;
    IsCompleted = true;
  }
}