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
    A user needs to be able to upload information about the item they wish to trade. (__)
    A user needs to be able to browse a list of other users items. (**)
    A user needs to be able to request a trade for other users items. (__)
    A user needs to be able to browse trade requests. (__)
    A user needs to be able to accept a trade request. (__)
    A user needs to be able to deny a trade request. (__)
    A user needs to be able to browse completed requests. (__)
    */

/*registrera user, sak att tradea, login/out, ska kunna hålla information om itemet, se andras inventory, trade request, browse trade request, accept, deny, browse historik
*/

/* tillägg att kunna spara tex profiler vi skapar i terminalen OBS ska göra en README ang vad jag använt och vad jag inte använt och varför*/

using System.Reflection.Metadata;
using App;

List<User> users = new List<User>();

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




List<TradeRequest> tradeRequests = new List<TradeRequest>();


User activeUser = null;
bool loggedIn = false;
while (true)
{

  while (!loggedIn)
  {
    Console.Clear();
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
        case 0:
          Console.Clear();
          Console.WriteLine("===== Login Menu =====");
          Console.WriteLine("username:");
          string? username = Console.ReadLine();
          Console.WriteLine("password:");
          string? password = Console.ReadLine();

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



        case 1:
          Console.Clear();
          Console.WriteLine("===== Register Menu =====");
          Console.WriteLine("username:");
          string? Username = Console.ReadLine();
          Console.WriteLine("password:");
          string? _password = Console.ReadLine();
          Console.Clear();
          users.Add(new User(Username, _password));
          Console.WriteLine("register done, now go and login");
          Console.ReadLine();
          break;

        case 2:
          Console.Clear();
          Console.WriteLine("exiting....");
          return;
      }
    }





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
        case 0:
          Console.Clear();
          Console.WriteLine("===== Welcome to your inventory =====");
          activeUser.ShowInventory();
          Console.WriteLine("\nWould you like to add a new item? y/n");
          if (Console.ReadLine().ToLower() == "y")
          {
            Console.WriteLine("Write what kind of weapon type: [AWP] [AK47] [M4A4]");
            string weapon = Console.ReadLine();

            Console.WriteLine("What kind of skin on the weapon: [PAW] [BOOM] [Nightmare]");
            string skin = Console.ReadLine();

            Console.WriteLine("What kind of wear on the skin: [BS] [FT] [FN]");
            string wear = Console.ReadLine();

            activeUser.AddItem(new Items { Weapon = weapon, Skin = skin, Wear = wear });
            Console.WriteLine("New item added");

          }
          Console.WriteLine("\n---Press Enter to go back---");
          Console.ReadLine();
          break;

        case 1:
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
        case 2:
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

          if (chooseItems.Count > 0 || requestedItems.Count > 0)
          {
            TradeRequest newTrade = new TradeRequest(activeUser, targetUser, chooseItems);
            newTrade.RequestedItems = requestedItems;
            tradeRequests.Add(newTrade);
            Console.WriteLine($"\nTrade request sent to {targetUser.Username} with the item(s) {chooseItems.Count} for {requestedItems.Count}");
          }
          else
          {
            Console.WriteLine("Nothing picked...");
          }

          Console.WriteLine("\n---Press Enter to go back---");
          Console.ReadLine();
          break;

        case 3:
          Console.Clear();
          Console.WriteLine("===== All active traderequests =====");
          List<TradeRequest> incoming = new List<TradeRequest>();

          foreach (var request in tradeRequests)
          {
            if (!request.IsCompleted && request.ToUser == activeUser)
            {
              incoming.Add(request);
            }
          }

          if (incoming.Count == 0)
          {
            Console.WriteLine("No trade traderequest here....");
            Console.WriteLine("\n---Press Enter to go back---");
            Console.ReadLine();
            break;
          }

          for (int j = 0; j < incoming.Count; j++)
          {
            var request2 = incoming[j];
            Console.WriteLine($"{j + 1}. From: {request2.FromUser.Username} - Items: {request2.OfferedItems.Count}");
          }

          Console.WriteLine("\nChoose a request to handle (o = back):");
          int request2Choice = Convert.ToInt32(Console.ReadLine());

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

          var selected = incoming[request2Choice - 1];
          Console.Clear();
          selected.ShowRequest();
          Console.WriteLine("Wanna accept or nah? write: y/n");

          var decision = Console.ReadLine().ToLower();

          if (decision == "y")
          {
            selected.Accept();
            Console.WriteLine("Trade accepted, enjoy your beuts of new items!");
          }
          else
          {
            selected.Deny();
            Console.WriteLine("Trade denied, that greedy bastard.....");
          }

          Console.WriteLine("\n---press Enter to go back---");
          Console.ReadLine();
          break;
        case 4:
          Console.Clear();
          Console.WriteLine("===== Your trade history =====");

          bool history = false;

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
          if (!history)
          {
            Console.WriteLine("No trades ever happend, are you delulu?");
          }
          Console.WriteLine("\n---Press Enter to go back---");
          Console.ReadLine();
          break;
        case 5:
          Console.Clear();
          loggedIn = false;
          activeUser = null;
          break;
      }
    }
  }
}

