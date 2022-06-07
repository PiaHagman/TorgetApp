# Grupp 1 Göteborg Agile

## Gruppmedlemar

 - Kim Björnsen Åklint (Scrum master)
 - Pia Hagman (Product Owner)
 - Johan Fahlgren 
 - Idris Hargaaya (Stakeholder)
 - Gustav Alvérus

## Blocket app

Core funktionalitet som speglar blocket. 

- Filtrering 
  - Kategorier
  - Plats

- Sortering
  - Tid
  - Kategori
  - Pris

- Meddelande funktion
- Kunna lägga upp annons med bild/er
- Bevakningar
- Sökfunktion
- Profil
  - Kontoinställningar
  - Mina annonser
  - Sparade annonser

## Kodspecifika beslut
- Vi bygger vårt projekt i ASP .NET Core
- Vi använder razorpages framför controllers
- Bootstrap, MudBlazor eller andra bibliotek är ok att använda om man vill för specifika komponenter, exempelvis carousel, card, meny eller liknande.
- Vi bygger mobile first
- Commits görs på engelska

## Gruppkontrakt

### Daily standup

Helgfria vardagar 9:00-9:15

### Tillgänglighet

#### Kim Björnsen Åklint
  - Kan arbeta varje vardag 8 - 17. Sitter också många kvällar.
  - Dagar jag vet att jag är borta:
    - 25/5 10-13 är jag borta
    - 27/5 Jobbar jag på mitt gamla jobb
    - 3/6 Bröllopsdag, så stor planering här ;)

#### Johan Fahlgren
  - Kan arbeta varje vardag 8~9 - 17.
  - Dagar jag vet att jag är borta:
    - 24/5 borta 11~14
    - 26/5 hela dagen
    - 27/5 hela dagen
    - 1/6 vet inte tid än (gissa förmiddag), men borta ca 2 timmar.

#### Hargaaya Idris
  - Kan arbeta vardagar 9-17
  - Tillfällen jag vet att jag är borta: 
    - 25/5 Kl. 12-14

#### Pia Hagman
- Kan arbeta varje vardag 8.30 - 15.30. Kan också sitta flera kvällar om så krävs.
- Frånvaro jag vet om:
  - denna vecka (v.21) lite från och till pga magsjuka och alla barn hemma
  - 9/6 före lunch skolavslutning
  - 10/6 barnen studiedag så endast med på möten
  - 15/6 efter lunch

#### Gustav Alvérus
- Kan arbeta vardagar 9-17
- Frånvaro jag vet om
  - 31/5 efter lunch
  - 10/6 före lunch
 
    

## Branchingstrategi
- Master
 - Sprintbranch: sprint/1
  - Individuell branch namnges issue/(#issue-number)

PR från issue-branch görs till aktuell sprintbranch. <br>
När allas arbeten är mergade in i sprintbranchen, testas denna innan den mergas in i master.
Mötesanteckningar kan commitas i valfri branch eftersom dessa inte påverkar andras arbete.

- Commits görs på engelska

**Tänk på att**
- Man lägger endast till nya commits till sprintbranch genom PR från en issue branch
- Man _mergar_ aldrig _in_ till en master/sprintbranch, bara _från_
- Om det står i PR att det finns en konflikt mellan sprint och en issue branch så löser man det genom att:
1. Uppdatera den lokala master branchen
2. Lokalt merga master till issue branchen
3. Kolla att allt står väl till, lös eventuella konflikter
4. Pusha din issue branch och därmed uppdatera PR automatiskt
5. Om inget nytt har hänt så ska det nu gå att genomföra och avsluta PR 

___

## SCRUM

### Definition of Ready
- **Oberoende/tydligt:** Det ska vara tydligt vad issuen ska lösa/vilket resultat som ska nås
- **Öppen:** Det är upp till utvecklaren att komma med egna lösningar, issuen är inte detaljplanerad.
- **Estimerad:** Fibonaccis sekvens, estimerad tillsammans under sprintplanering.  
- Issuen ska knytas till aktuell user story om den har en.

### Definition of Done
- **Reviewer:** Minst en kollega har tittat igenom, **testkört** och godkänt koden.
- **Körbar:** Alla punkter i issuen ska vara implementerade och körbara.
- **Kund-klar:** Redo att presenteras/testas av kund.
- **Merge:** Issuen är mergad med sprintbranschen 
