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
    A user needs to be able to register an account  (**)
    A user needs to be able to log out. (**)
    A user needs to be able to log in. (**)
    A user needs to be able to upload information about the item they wish to trade. (**)
    A user needs to be able to browse a list of other users items. (**)
    A user needs to be able to request a trade for other users items. (**)
    A user needs to be able to browse trade requests. (**)
    A user needs to be able to accept a trade request. (**)
    A user needs to be able to deny a trade request. (**)
    A user needs to be able to browse completed requests. (**)
    */

/*registrera user*, sak att tradea*, login/out*, ska kunna hålla information om itemet*, se andras inventory*, trade request*, browse trade request*, accept*, deny*, browse historik*
*/

/* tillägg att kunna spara tex profiler vi skapar i terminalen OBS ska göra en README ang vad jag använt och vad jag inte använt och varför*/

using System.Reflection.Metadata;
using System.Text;
using App;

// Filnamn för sparning av användare och trades.
string usersFile = "users.txt";
string tradesFile = "trades.txt";

// Listor som håller alla användare och alla trades.
List<User> users = new List<User>();
List<TradeRequest> tradeRequests = new List<TradeRequest>();

//För att spara användare även när man stängt ner terminalen
if (File.Exists(usersFile))
{
  var lines = File.ReadAllLines(usersFile);
  User? currentUser = null;

  foreach (var line in lines)
  {
    // Varje användare i filen börjar med "User:"
    if (line.StartsWith("User:"))
    {
      // Splittar username och lösenord
      var parts = line.Substring(5).Split(',');
      currentUser = new User(parts[0], parts[1]);
      users.Add(currentUser);
    }
    // Om raden börjar med "Item:" är det föregående användare
    else if (line.StartsWith("Item:") && currentUser != null)
    {
      var parts = line.Substring(5).Split('|');
      currentUser.AddItem(new Items
      {
        Weapon = parts[0],
        Skin = parts[1],
        Wear = parts[2]
      });
    }
  }
}
// För att ladda request från filen
if (File.Exists(tradesFile))
{
  TradeRequest? currentRequest = null;
  foreach (var line in File.ReadAllLines(tradesFile))
  {
    if (line.StartsWith("Trade:"))
    {
      // Parsar raden för att hitta vilka användare det gäller
      var parts = line.Substring(6).Split(',');
      var fromUser = users.Find(u => u.Username == parts[0]);
      var toUser = users.Find(u => u.Username == parts[1]);

      // Skapar nytt tradeobjekt om båda användarna finns
      if (fromUser != null && toUser != null)
      {
        currentRequest = new TradeRequest(fromUser, toUser, new List<Items>());
        currentRequest.IsCompleted = bool.Parse(parts[2]);
        currentRequest.IsAccepted = bool.Parse(parts[3]);
        tradeRequests.Add(currentRequest);
      }
    }
    // Lägger till de föremålen från traden
    else if (line.StartsWith("Offered:") && currentRequest != null)
    {
      var parts = line.Substring(8).Split('|');
      currentRequest.OfferedItems.Add(new Items
      {
        Weapon = parts[0],
        Skin = parts[1],
        Wear = parts[2]
      });
    }
    // Lägger till dom föremål som önskades i traden
    else if (line.StartsWith("Requested:") && currentRequest != null)
    {
      var parts = line.Substring(10).Split('|');
      currentRequest.RequestedItems.Add(new Items
      {
        Weapon = parts[0],
        Skin = parts[1],
        Wear = parts[2]
      });
    }
  }
}

//För att ha statiska profiler från början när det inte finns någon som man skapat
if (users.Count == 0)
{
  var userA = new User("a", "a");
  users.Add(userA);
  userA.AddItem(new Items { Weapon = "AWP", Skin = "Dragon Lore", Wear = "FN" });
  userA.AddItem(new Items { Weapon = "AK-47", Skin = "Redline", Wear = "FT" });
  var userS = new User("s", "s");
  users.Add(userS);
  userS.AddItem(new Items { Weapon = "M4A1-S", Skin = "Nightmare", Wear = "BS" });
  userS.AddItem(new Items { Weapon = "AK-47", Skin = "Slate", Wear = "FT" });
  var userD = new User("d", "d");
  users.Add(userD);
  userD.AddItem(new Items { Weapon = "USP-S", Skin = "Jawbreaker", Wear = "FN" });
  userD.AddItem(new Items { Weapon = "Glock", Skin = "Fade", Wear = "BS" });
  // Sparar dom till filen
  SaveUsers();
}
//Variabler som håller koll på status och så att man är inloggad
User activeUser = null;
bool loggedIn = false;

//Huvudloop
while (true)
{
  //Loginloop
  while (!loggedIn)
  {
    Console.Clear();
    //Om ingen är inloggad visas detta
    if (activeUser == null)
    {
      Console.WriteLine("===== Login Menu =====");
      foreach (var option in Enum.GetValues(typeof(Login)))
      {
        Console.WriteLine($"{(int)option}. {option}");
      }
      Console.WriteLine("\nEnter the NUMBER of the place you want to go...");
      switch (Convert.ToInt32(Console.ReadLine()))
      {
        case 0: //Login
          Console.Clear();
          Console.WriteLine("===== Login Menu =====");
          Console.WriteLine("username:");
          string? username = Console.ReadLine();
          Console.WriteLine("password:");
          string? password = Console.ReadLine();

          //Ser så att kontot finns
          foreach (User user in users)
          {
            if (user.TryLogin(username, password))
            {
              activeUser = user;
              loggedIn = true;
              break;
            }
          }

          if (!loggedIn)
          {
            Console.WriteLine("\nworng password and/or username...\n---Press Enter to go back---");
            Console.ReadLine();
          }
          break;

        case 1://Registrera
          Console.Clear();
          Console.WriteLine("===== Register Menu =====");
          Console.WriteLine("username:");
          string? Username = Console.ReadLine();
          Console.WriteLine("password:");
          string? _password = Console.ReadLine();
          users.Add(new User(Username, _password));

          //Sparar användare
          SaveUsers();

          Console.Clear();
          Console.WriteLine("register done, now go and login");
          Console.ReadLine();
          break;

        case 2://stänger program
          Console.Clear();
          Console.WriteLine("exiting....");
          return;
      }
    }
    //Inloggad meny
    while (activeUser != null && loggedIn)
    {
      Console.Clear();
      Console.WriteLine("===== Main Menu =====");
      foreach (var option in Enum.GetValues(typeof(Menu)))
      {
        Console.WriteLine($"{(int)option}. {option}");
      }
      Console.WriteLine("\nEnter the NUMBER of the place you want to go...");
      switch (Convert.ToInt32(Console.ReadLine()))
      {
        case 0: //Inventory
          Console.Clear();
          Console.WriteLine("===== Welcome to your inventory =====");
          activeUser.ShowInventory();
          Console.WriteLine("\nWould you like to add a new item? y/n");
          if (Console.ReadLine().ToLower() == "y")
          {
            // skapar ett item
            Console.WriteLine("Write what kind of weapon type: [AWP] [AK47] [M4A4]");
            string weapon = Console.ReadLine();

            Console.WriteLine("What kind of skin on the weapon: [PAW] [BOOM] [Nightmare]");
            string skin = Console.ReadLine();

            Console.WriteLine("What kind of wear on the skin: [BS] [FT] [FN]");
            string wear = Console.ReadLine();

            //Sparar item
            activeUser.AddItem(new Items { Weapon = weapon, Skin = skin, Wear = wear });
            SaveUsers();

            Console.WriteLine("New item added");

          }
          Console.WriteLine("\n---Press Enter to go back---");
          Console.ReadLine();
          break;

        case 1://se andras inventory
          Console.Clear();
          Console.WriteLine("===== Peak on others inventory =====");
          foreach (var user in users)
          {
            if (user != activeUser)
            {
              Console.WriteLine($"{user.Username} inventory:");
              user.ShowInventory();
            }
          }
          Console.WriteLine("\n---Press Enter to go back---");
          Console.ReadLine();
          break;
        case 2: //Skicka tradeoffer
          Console.Clear();
          Console.WriteLine("===== Send trade offers =====");
          Console.WriteLine("\nChoose a user to send a trade offer to:");
          int i = 1;
          List<User> otherUsers = new List<User>();
          foreach (var user in users)
          {
            if (user != activeUser)
            {
              Console.WriteLine($"{i}. {user.Username}");
              otherUsers.Add(user);
              i++;
            }
          }
          if (otherUsers.Count == 0)
          {
            Console.WriteLine("You got no friends...");
            break;
          }

          int choice = Convert.ToInt32(Console.ReadLine());
          if (choice < 1 || choice > otherUsers.Count)
          {
            Console.WriteLine("Wrong input, try again...");
            Console.ReadLine();
            break;
          }

          User targetUser = otherUsers[choice - 1];

          //Items man vill tradea
          List<Items> chooseItems = new List<Items>();
          bool addingItems = true;

          while (addingItems)
          {
            Console.Clear();
            Console.WriteLine("Your inventory: ");
            activeUser.ShowInventory();

            Console.WriteLine("\n Enter the number of the item you would like to add (0 = done):");
            int itemChoice = Convert.ToInt32(Console.ReadLine());

            if (itemChoice == 0)
            {
              addingItems = false;
              break;
            }

            List<Items> myItems = activeUser.GetInventory();
            if (itemChoice < 1 || itemChoice > myItems.Count)
            {
              Console.WriteLine("Wrong, try again..");
              Console.ReadLine();
            }
            else
            {
              chooseItems.Add(myItems[itemChoice - 1]);
              Console.WriteLine("Item added to your trade.");
              Console.ReadLine();
            }
          }
          //items man vill tradea för
          List<Items> requestedItems = new List<Items>();
          bool activeTrade = true;

          while (activeTrade)
          {
            Console.Clear();
            Console.WriteLine($"{targetUser.Username} inventory: ");
            targetUser.ShowInventory();

            Console.WriteLine("\nEnter the number of the item you want and then '0' when doen");
            int itemChoice = Convert.ToInt32(Console.ReadLine());

            if (itemChoice == 0)
            {
              activeTrade = false;
              break;
            }

            List<Items> theirItems = targetUser.GetInventory();
            if (itemChoice < 1 || itemChoice > theirItems.Count)
            {
              Console.WriteLine("Wrong, try again...");
            }
            else
            {
              requestedItems.Add(theirItems[itemChoice - 1]);
              Console.WriteLine("item added to the request");
              Console.ReadLine();
            }
          }
          //skapar och sparar traderequest
          if (chooseItems.Count > 0 || requestedItems.Count > 0)
          {
            TradeRequest newTrade = new TradeRequest(activeUser, targetUser, chooseItems);
            newTrade.RequestedItems = requestedItems;
            tradeRequests.Add(newTrade);
            SaveTrades();
            Console.WriteLine($"\nTrade request sent to {targetUser.Username} with the item(s) {chooseItems.Count} for {requestedItems.Count}");
          }
          else
          {
            Console.WriteLine("Nothing picked...");
          }

          Console.WriteLine("\n---Press Enter to go back---");
          Console.ReadLine();
          break;

        case 3: //active tradeoffers
          Console.Clear();
          Console.WriteLine("===== All active traderequests =====");

          // Skapar en lista för inkommande trade requests
          List<TradeRequest> incoming = new List<TradeRequest>();

          // Loopar igenom alla tradeRequests och visar dom som är kopplade till den inloggade användaren
          foreach (var request in tradeRequests)
          {
            if (!request.IsCompleted && request.ToUser == activeUser)
            {
              incoming.Add(request);
            }
          }
          // Om inga requests finns
          if (incoming.Count == 0)
          {
            Console.WriteLine("No trade traderequest here....");
            Console.WriteLine("\n---Press Enter to go back---");
            Console.ReadLine();
            break;
          }
          // Lista upp alla inkommande requests 
          for (int j = 0; j < incoming.Count; j++)
          {
            var request2 = incoming[j];
            Console.WriteLine($"{j + 1}. From: {request2.FromUser.Username} - Items: {request2.OfferedItems.Count}");
          }

          Console.WriteLine("\nChoose a request to handle (o = back):");
          int request2Choice = Convert.ToInt32(Console.ReadLine());

          // Felhantering om man väljer ett nummer som inte finns
          if (request2Choice < 1 || request2Choice > incoming.Count)
          {
            Console.WriteLine("wrong, go back");
            Console.ReadLine();
            break;
          }
          else if (request2Choice == 0)
          {
            break;
          }
          // Hämtar vald trade
          var selected = incoming[request2Choice - 1];
          Console.Clear();
          selected.ShowRequest(); // Visar requests
          Console.WriteLine("Wanna accept or nah? write: y/n");

          var decision = Console.ReadLine().ToLower();

          // Om användaren accepterar traden
          if (decision == "y")
          {
            selected.Accept(); //acceptar trade
            SaveTrades(); //sparar trade
            Console.WriteLine("Trade accepted, enjoy your beuts of new items!");
          }
          else
          {
            selected.Deny(); //nekar trade
            SaveTrades(); //sparar nekade trade
            Console.WriteLine("Trade denied, that greedy bastard.....");
          }

          Console.WriteLine("\n---press Enter to go back---");
          Console.ReadLine();
          break;
        case 4: //Trade historik
          Console.Clear();
          Console.WriteLine("===== Your trade history =====");

          bool history = false;
          // Loopar igenom alla trades och visar dom som är färdiga
          foreach (var request in tradeRequests)
          {
            if (request.IsCompleted && (request.FromUser == activeUser || request.ToUser == activeUser))
            {
              history = true;

              Console.WriteLine("------------------------------");
              Console.WriteLine($"From: {request.FromUser.Username}. To: {request.ToUser.Username}");

              if (request.IsAccepted)
              {
                Console.WriteLine("--Accepted trade--");
              }
              else
              {
                Console.WriteLine("--Declined trade--");
              }

              Console.WriteLine("Offered:");
              foreach (var item in request.OfferedItems)
              {
                Console.WriteLine($" {item.Weapon} | {item.Skin} ({item.Wear})");
              }
              Console.WriteLine("Requested:");
              foreach (var item in request.RequestedItems)
              {
                Console.WriteLine($" {item.Weapon} | {item.Skin} ({item.Wear})");
              }

              if (request.IsAccepted && (request.FromUser == activeUser || request.ToUser == activeUser))
              {
                Console.WriteLine("You traded these items");
              }
            }
          }
          // Om inga trades finns
          if (!history)
          {
            Console.WriteLine("No trades ever happend, are you delulu?");
          }
          Console.WriteLine("\n---Press Enter to go back---");
          Console.ReadLine();
          break;
        case 5: //Logga ut
          Console.Clear();
          loggedIn = false;
          activeUser = null;
          break;
      }
    }
  }
}

//Sparar användare och inventories
void SaveUsers()
{
  List<string> lines = new List<string>();

  foreach (var user in users)
  {
    //sparar Username och Lösenord
    lines.Add($"User:{user.Username},{user._password}");

    //sparar användarens items
    foreach (var item in user.GetInventory())
    {
      lines.Add($"Item:{item.Weapon}|{item.Skin}|{item.Wear}");
    }
  }
  //skriver texten till users.txt
  File.WriteAllLines(usersFile, lines);
}

//Sparar traderequests
void SaveTrades()
{
  List<string> lines = new List<string>();
  foreach (var tr in tradeRequests)
  {
    //sparar infon ang dom som byter
    lines.Add($"Trade:{tr.FromUser.Username},{tr.ToUser.Username},{tr.IsCompleted},{tr.IsAccepted}");
    foreach (var item in tr.OfferedItems)
    {
      lines.Add($"Offered:{item.Weapon}|{item.Skin}|{item.Wear}");
    }
    //sparar items som ska bytas
    foreach (var item in tr.RequestedItems)
    {
      lines.Add($"Requested:{item.Weapon}|{item.Skin}|{item.Wear}");
    }
  }
  //skriver texten till trades.txt
  File.WriteAllLines(tradesFile, lines);
}