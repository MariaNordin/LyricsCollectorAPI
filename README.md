# LyricsCollectorAPI  

<br/>

Detta projekt är ett .NET Core Web API, skrivet som backend till [LyricsCollectorSPA](https://github.com/MariaNordin/NewLyricsCollectorSPA/blob/main/README.md)  
API:et innehåller endpoints för att söka efter en låttext, registrera användare, logga in, skapa en ny lista, lägga till låttext i en lista mm.

<br/>

#### För att kunna köra applikationen krävs följande steg:
  
- Du behöver ett Client ID och en Client Secret från Spotify:  

  - Gå till [Spotify for Developers, Dashboard](https://developer.spotify.com/dashboard/)
  - Logga in (om du redan har ett spotify-konto behöver du inte skapa ett nytt utan kan logga in med det)
  - När du har loggat in väljer du **CREATE AN APP**
  - Du får upp ett fönster där du fyller i ett namn och en beskrivning på appen  
  (skriv vad som helst, detta är bara för att du ska få ett id och en secret)  
  - Kryssa i rutorna att du förstår och godkänner villkoren och välj **CREATE**  
  - När du godkänt dyker en grön ruta upp som representerar din app, klicka på den och kopiera respektive textsträng för Client Id och Client Secret 

- Klistra in Id och Secret i valfri config json-fil i projektet LyricsCollectorAPI (förslagsvis i User Secrets)  
Använd följande format och samma nyckelord:

```
{
  "SpotifyCredentials": {
    "SpotifyClientId": "**__ditt Client Id här__**",
    "SpotifyClientSecret": "**__din Client Secret här__**"
  }
}
```

- Du behöver även lägga till en connection string till en SQL databas:  
  Öppna appsettings.json-filen och byt ut min connection string till din egen:
  
```
  "ConnectionStrings": {
    "DefaultConnection": "Server= **__din db connection string här__**;"
  }
```
- Nu behöver du köra ```add-migration``` och därefter ```update-database``` i Package Manager Console
- **KLART!** 

Starta programmet och gå till localhost:3000 (react-appen som du också har startat genom att följa instruktioner i [LyricsCollectorSPA](https://github.com/MariaNordin/NewLyricsCollectorSPA) )
  
------------------------------------------------------------------------------------------------------------------------------------------------------
### Beskrivning

Applikationen konsumerar två öppna API:er:  

- [Spotify Web API](https://developer.spotify.com/documentation/web-api/)  
- [lyrics.ovh](https://lyricsovh.docs.apiary.io/#)

Från lyrics.ovh hämtas låttexter och från spotify hämtas skivomslag + länk till den låt vilkens text efterfrågas av användaren.

Jag har försökt följa Dependency Inversion Principle, vilket är lurigt tycker jag. Det blir många interface och jag inser vinsten med dessa när det gäller bla testning och för att kunna vidareutveckla och underhålla koden. Samtidigt har jag läst på lite och förstår det som att om man skapar interface för varenda klass är det inte en bra lösning. Jag har dock för lite erfarenhet för att bedömma när det är rimligt/ok att "new:a" upp faktiska instanser av klasser i koden. Funderar och experimenterar vidare på detta...

Jag har använt mig av *Observer pattern* för att min CollectionController ska veta vilken låt som senast söktes på. LyricsController uppdaterar alltså  CollectionController (via respektive services) varje gång en efterfrågad låttext får en träff. Om användaren efterfrågar "spara låttext till lista", vilken har sin endpoint i CollectionController behöver inte samma data skickas fram och tillbaka mellan klienten och servern, utan CollectionController sparar helt enkelt (via sina services) "current lyrics" till databasen ihop med det collection-id som skickas med från clienten vid anropet.

:heavy_check_mark: Entity Framework - code first  
:heavy_check_mark: Cache-hantering i backend - cachear låt-modellerna från databasen  
:heavy_check_mark: Unit tests - valda test av Controllers funktionalitet, att de skickar tillbaka rätt statuskoder beroende på utfall, att rätt metoder     anropas och att rätt endpoints är märkta med [Authorize] attribut

