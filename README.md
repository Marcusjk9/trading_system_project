Trading System Project


-Syfte- 

Att skapa ett konsolprogram där användare kan logga in och tradea skins med varandra.


-Sammanfattning-

I detta projekt så har jag skapat ett program som gör så att användare registrera sig, logga in, lägga till vapen med skins i sitt inventory och byta dom med varandra.
Konton, items och trades sparas automatiskt i textfiler så att informationen finns kvar även efter att programmet stängts ner.


-Funktioner-

Registrera konto och logga in
Skapa items och kunna se dom i sitt inventory
Se abdras inventory.
Skapa traderequests mellan användare.
Accept eller Deny traderequests.
Se historik på traderequest
Logga ut.


-Implementation och teknik-

Jag har valt att bygga programmet med flera klasser för att separera logiken:
User – hanterar användarnamn, lösenord och inventory
Items – representerar ett föremål (vapen, skin, wear)
TradeRequest – hanterar byten mellan användare
Program – innehåller huvudmenyn, inloggning, och logiken för att spara och läsa data

Jag har använt listor för att lagra flera användare, items och trade requests.
För att spara och läsa data använder .txt-filer (users.txt och trades.txt).


-Mina val-
Klasser och objekt har jag använt för att dela upp koden i tydliga delar.
Listor har jag använt för att lagra flera användare, items och trades
Enums	som skapar strukturerade menyer
.txt Filhantering så att jag kan spara användare och trades permanent

Arv valde bort eftersom klasserna inte delar egenskaper.
Interfaces kändes överdrivet för projektets nivå
JSON/databas lärde jag mig lite om medans jag gjorde detta programmet men det är inget vi lärt oss på lektionerna
