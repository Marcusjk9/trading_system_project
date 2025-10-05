namespace App;

// Klassen är till att skapa items
class Items
{
  public string Weapon { get; set; }
  public string Skin { get; set; }
  public string Wear { get; set; }

  // Tom konstruktor för laddning och skapande av nya items
  public Items() { }

  // för att skriva ut ett item som text
  public override string ToString()
  {
    return $"{Weapon} | {Skin} ({Wear})";
  }
}