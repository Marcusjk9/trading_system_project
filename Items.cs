namespace App;

class Items
{
  public string Weapon { get; set; }
  public string Skin { get; set; }
  public string Wear { get; set; }

  public Items() { }
  public override string ToString()
  {
    return $"{Weapon} | {Skin} ({Wear})";
  }
}