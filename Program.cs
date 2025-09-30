/*
Ni ska ha arbetat aktivt med uppgiften
Ni ska ha användt git aktivt under utvecklingsprocessen
Ni ska dokumentera, med kommentarer i er kod, vad koden gör och varför ni
gjort vissa val.
Ni ska endast använda de koncept vi gått igenom hitils i kursen. Om ni undrar
ifall ni får använda något annat, fråga utbildaren.
Ni ska skriva, i en README.md fil, om hur man använder programmet och
resonera kring era implementationsval. t.ex. varför ni användt eller inte
användt komposition, inheritance, osv.
Ni ska kunna svara på “Varför?” gällande er kod.
*/

/*
    A user needs to be able to register an account  (__)
    A user needs to be able to log out. (__)
    A user needs to be able to log in. (__)
    A user needs to be able to upload information about the item they wish to trade. (__)
    A user needs to be able to browse a list of other users items. (__)
    A user needs to be able to request a trade for other users items. (__)
    A user needs to be able to browse trade requests. (__)
    A user needs to be able to accept a trade request. (__)
    A user needs to be able to deny a trade request. (__)
    A user needs to be able to browse completed requests. (__)
    */

/*registrera user, sak att tradea, login/out, ska kunna hålla information om itemet, se andras inventory, trade request, browse trade request, accept, deny, browse historik
*/

using App;

bool loggedIn = false;

while (!loggedIn)
{
  Console.Clear();
  Console.WriteLine("===== Login Menu =====");
  foreach (var option in Enum.GetValues(typeof(Login)))
  {
    Console.WriteLine($"{(int)option}. {option}");
  }
  Console.WriteLine("\nEnter the NUMBER of the place you want to go...");
  switch (Convert.ToInt32(Console.ReadLine()))
  {
    case 0:
      Console.Clear();
      Console.WriteLine("===== Login Menu ====");
      Console.WriteLine("username:");
      Console.ReadLine();
      Console.WriteLine("password:");
      Console.ReadLine();
      break;

    case 1:
      Console.Clear();
      Console.WriteLine("===== Register Menu ====");
      Console.WriteLine("username:");
      Console.ReadLine();
      Console.WriteLine("password:");
      Console.ReadLine();
      break;

    case 2:
      Console.Clear();
      Console.WriteLine("exiting....");
      return;
  }
}


bool running = true;


while (running)
{
  Console.Clear();
  Console.WriteLine("===== Main Menu ====");
  foreach (var option in Enum.GetValues(typeof(Menu)))
  {
    Console.WriteLine($"{(int)option}. {option}");
  }
  Console.WriteLine("\nEnter the NUMBER of the place you want to go...");
  switch (Convert.ToInt32(Console.ReadLine()))
  {
    case 0:
      Console.Clear();
      Console.WriteLine("===== Welcome to your inventory ====");
      Console.ReadLine();
      break;

    case 1:
      Console.Clear();
      Console.WriteLine("===== Peak on others inventory ====");
      Console.ReadLine();
      break;
    case 2:
      Console.Clear();
      Console.WriteLine("===== Send trade offers ====");
      Console.ReadLine();
      break;

    case 3:
      Console.Clear();
      Console.WriteLine("===== All active traderequests ====");
      Console.ReadLine();
      break;
    case 4:
      Console.Clear();
      Console.WriteLine("===== Your trade history ====");
      Console.ReadLine();
      break;
    case 5:
      Console.Clear();
      running = false;
      break;
  }
}

