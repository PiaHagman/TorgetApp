## 24/5
<br>**Sprint Planning**
<br>**Timebox: 3 timmar**
<br><br>
Summering: Lagets första sprintplanering och trots att jag var magsjuk så var jag ju tvungen att närvara. 😁 På grund av mitt mående fick jag tyvärr lämna över ansvaret 
som ordförande till Kim. Jag försökte dock axla min roll som PO genom att vara tydlig med mina tankar kring prioritering. Vi hade ett bra inledande möte. 
Alla var aktiva i att gå igenom alla issues och göra de redo för sprint enligt vår "Definition of Ready", 
dvs att de är tydligt vad issuen ska lösa, samt att de är tidsestimerade. Efter det så bestämde vi vilka issues som ska hanteras i den kommande sprinten. Det var ganska 
svårt att bedöma hur mycket vi skulle hinna. Lite ledighet i slutet på veckan pga kristi himmelsfärd samt en del tid till lektioner påverkade vår estimering. Vi kände 
alla att vi borde lösa _minst_ det vi tog med i sprinten, och skulle kanske kunnat lägga in ännu fler issues, men ville samtidigt inte ta oss vatten över huvudet. 
Vi landade i 15 issues med följande sprintmål:
- Startsida där annonser visas
- Möjlighet att klicka fram en annons för mer info
- Möjlighet för en användare att skapa en användare
- Möjlihet att logga in 
- Möjlighet att lägga upp annonser
- Möjlighet att skicka meddelande till en annonsör
- Möjlighet att kunna söka fram annonser genom olika typer av filtrering
<br><br>
## 25/5
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering: Idag hade vi vår första dagliga standup som vi höll kort. Eftersom vi inte börjat jobba ännu så hade vi inte så mycket att rapportera från igår utan 
mötet handlade mest om vad var och en ville ta sig an till en början. Vi assignade oss på så sätt för våra första issues. Vi bestämde att vi endast skulle flytta en 
issue per person till Pågående åt gången. Först när den issuen som påbörjats är färdig kan nästa issue flyttas dit. Däremot kan var och assigna sig på fler issues som 
ligger i To Do. Vi bestämde att vi skulle göra modellerna för användare och annons tillsammans direkt efter mötet, liksom kika tillsammans på de wireframes på
layout som Idris hade jobbat med under gårdagen. 

## 26/5
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering:
- Genomgång av allas arbete so far
- Alla namn på och i filer och commits i VS sker på engelska
- Ny kolumn i Sprint 1 blackloggen: "Inväntar review" mellan "Pågående" och "Done"
- Under Definition of Done har vi lagt till att kod ska testköras av minst en kollega innan merge
- Readme-commits kan göras direkt i masterbranch


## 30/5
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering:
- Endast jag, Johan och Kim närvarande
- Inte så mycket har hänt sedan förra veckan. 
- Jag har byggt klart UI för en annons, lite problem med font awesome, men får hjälp efter mötet.
- Fortsätter med lite logik och Javascript för sidan under dagen.

## 31/5
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering:
- Gustav sjuk idag igen. 
- Vi pratade om att det kan vara kul att låta vissa issues ta lite extra tid. Att denna kurs passar ganska bra för fördjupning 
samtidigt som vi lär oss det agila arbetssättet. Kanske viktigare än att komma långt med projektet. 
- Jag klar med logik och javascript för "en annons" - mergat med issue1 lokalt för att få ihop med Idris meny. Lite små konflikter som vi ska diskutera under dagen.
- Vi ska gå igenom Kims search-funktion direkt efter mötet för att se om vi förstår den. :)
- Eftersom sprint 1 blev lite kortare än vi trodde och vi redan imorgon eftermiddag ska slutföra inkrementet för sprint1 så är det lite oklart om vi kommer hinna färdigt. 


## 1/6
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering:
- Idris och Kim icke närvarande. Vart är Idris? Inget meddelande om frånvaro eller svar på chatt 
- Gustav vill på grund av sjukdom skjuta sin issue till nästa sprint och önskar få lite vägledning 
eftersom han inte gjort inloggning via Identity i något tidigare projekt ännu.
- Jag och Johan tog varsin ny issue trot att det är oklart om vi kommer hinna gör klart dem med tanke på den "kortare" veckan
- Möte för att få ihop allas issues och merga in i sprint 1 inför review-möte imorgon sker idag klockan 14.30
- Jag och Johan sätter oss direkt efter mötet och godkänner hans PR (Skapa annons).


## 2/6
<br>**Sprint Review**
<br>**Timebox: 1,5h**
<br><br>
Summering:
- På detta viktiga möte var samtliga deltagare närvarande, härligt!
- Jag som PO höll i mötet, närvarande var också Stakeholdern (i form av Idris).
- Vi sammanställde vårt inkrement och såg till att vår sprint 1 branch var updaterad under vårt avlsutande möte igår. Så vi började med att demonstrera vårt inkrement 
live för Stakeholdern. 
- Stakeholdern var nöjd med det mesta, men hade lite små funderingar och påtalade några krav på designändringar från hans sida. Samtlig respons nedtecknades som _review notes_ i vår backlog för att åtgärda i kommande sprinter. Exempel:
    - Hur kategorier ska visas och vilka huvudkategorier som kunden vill ha
    - Att huvudmenyn i överkant ska kunna fällas in och ut
    - Att Ikonen "Ny annons" ska ligga i mitten (och tydligare) i menyn i underkant - detta var tydligen VÄLDIGT viktigt för stakeholdern. :)
- I övrigt tyckte stakeholdern att projektet är på god väg och i rätt riktning. 

## 2/6
<br>**Sprint Retrospective**
<br>**Timebox: 1h**
<br><br>
Summering:
- Vi skapade en projekt-kanban som vi kallar för Retrospective 1
- Vi tog en stund där alla fick skriva kort utifrån egna tankar
- Överlag bra agilt arbete. God kommunikation och bra plan gjorde det lätt att arbeta. 
- Vi pratade om vad som gäller vi frånvaro - att frånvaro är ok, men att det måste meddelas så att resten av teamet kan ta vid dennes arbete om så krävs. 
- Vi lade till 3 actions som vi ska jobba med från nästa sprint:
    - hur vi hanterar och märker upp kommentarer i koden
    - att vi ska ta fram en eller flera issue templates
    - att vi ska bli bättre på att lära av varandra och uppmuntra till att titta på varandras kod och gå igenom den

## 2/6
<br>**Sprint Planning**
<br>**Timebox: 2,5h**
<br><br>
Summering:
- Skapade issue template
- Vi hade lagt in ganska många backlog items under förra sprinten som nu behövde göras till issues enligt vår mall. 
- Vi satt först var och en och skapade issues utifrån dessa och sedan tillsammans för att kontrollera att de var tydliga nog och 
innehöll det vi kommit överens om enligt vår Defintion of Done. Detta tog tid! 
- Sedan tidsestimering där vi var lite mer generösa i skalan än tidigare eftersom vi insåg att vi kanske var för snåla förra sprinten. 
- På en sista kvart bestämde vi sen vilka issues som skulle vara med i Sprint 2 och det blev nästan alla. Vi bedömde att tiden vi har att jobba på 
ungefär samma som förra sprinten efetrsom det även nu försvinner en del tid med röda dagar, frånvaro och redovisningar. 
- 51 poäng (vi klarade 27 förra sprinten). Frågan är om vi än en gång tog oss vatten över huvudet...

## 3/6
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering:
- Inget direkt att rapportera från gårdagen
- Skapat brancher utifrån tilldelade issues
- Kommer dra igång ordentligt på tisdag, annat jobb idag pch nationaldag på måndag. Eventuellt lite jobb i helgen.

## 7/6
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering:
- Förbättringar av postnummerbegränsningar och spara ner orten för Kim - titta på PR under fm
- Idris ej närvarande men två liggande PR
- Jag och Gustav ska kolla på log in tillsammans då han inte tidigare implementerat detta. 
- Stämde av kommande veckan lite. Något hattigt för mig med skolavslutning och studiedag för kidsen, men jobabr på så gott det går.

## 8/6
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering: 
- Inte så supermycket att rapportera eftersom vi satt tillsammans hela dagen igår. Mest jobba vidare och godkänna PR så snabbt som möjligt.
- Jag, Kim och Idris närvarande.
- Idris har kommit långt med sin chatt-funktion. Grymt! 
- Imorgon är jag frånvarande på fm pga skolavslutning

## 10/6
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering: 
- Jag, Kim och Johan närvarande.
- Klar med PR igår. Påbörjade issue #50 - kanske hinner fortsätta lite idag. Annars måndag.
- Jag rapporterade lite för Gustav som sitter med logga in och länkningar till registrering samt ev logga ut
- Kim på studiebesök idag, inte så mycket jobb. 
- Johan försökte med mudblazor igår, men gick tillbaka till html när det inte fungerade som han ville. Ska kika vidare på kategorier idag.

## 13/6
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering: 
- Kim: tittat mest på PR och MudBlazor för kategorier. Fortsätter i övrigt med filtrering.
- Johan: Jobbat vidare med kategorier och taggar - ska försöka få klart issuen idag
- Gustav: Ska fokusera lite på annat än detta projekt från nu. Fixa issue/PR under dagen.
- Idris: Skapat funktionell chat-funktion - både frontend och backend. Kolla PR under dagen tillsammans. 
- Jag forsätter med begränsning till fyra bilder under dagen.
- Vi planerar att ha vår avslutande sprint review på onsdag klockan nio.

## 14/6
<br>**Daily standup**
<br>**Timebox: 15 min**
<br><br>
Summering: 
- Jag färdig med min issue från igår.
- Jag och Idris sätter oss med lite sista designgrejer under dagen, issue #48.
- Kim och Johan slutför kategorier-arbetet

