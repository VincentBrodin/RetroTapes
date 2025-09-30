# RetroTapes
RetroTapes är en intern uthyrningstjänst för VHS-filmer där personal kan:

- Lista, söka och filtrera filmer att hyra  
- Se aktiva uthyrningar  
- Se historik över tidigare uthyrningar  

Projektet är byggt och underhålls av ett team av .NET-utvecklingsstudenter från YH Akademin i Sverige.

## Installation
```bash
git clone https://github.com/VincentBrodin/RetroTapes.git
cd RetroTapes
dotnet restore
```

## Struktur

### Stack

- **Razor**: Används för att bygga användargränssnittet
- **Entity Framework (EF)**: Används för att generera modeller och hantera dataåtkomst från databasen
- **Data Access Layer (DAL)**: Abstraherar och hanterar kommunikationen mellan `EF` och `Razor`, vilket ger en tydligare separation mellan lager
- **Bootstrap**: Används för layout och styling av applikationen


### Arkitektur

Projektet följer en klassisk `MVC`-struktur (Model–View–Controller) men har även utökats med en `DAL` för att förbättra struktur och underhållbarhet.

- **Model**: Datamodeller som representerar tabeller i Sakila-databasen (film, customer, rental m.m.).
- **DAL**: Sköter dataåtkomst och logik kring databasanrop. Detta gör applikationen mer modulär och minskar beroendet mellan EF och Razor.
- **View**: Razor-sidor för att presentera data och erbjuda användarvänligt gränssnitt.
- **Controller**: Anropar DAL för att hämta eller uppdatera data och styr applikationens flöden.


### Användarhantering

Eftersom systemet är internt finns ingen traditionell inloggning.
I stället använder personalen en dropdown-meny för att välja vilken administratör eller medarbetare som är aktiv.
Detta förenklar användarflödet och undviker behovet av konton och lösenord.

### Databas
RetroTapes använder den välkända Sakila-databasen, som innehåller realistisk struktur för en filmbutik:

- **film**: information om filmer (titel, beskrivning, längd, kategori m.m.)
- **actor**: skådespelare kopplade till filmer
- **customer**: kunder som hyr filmer
- **rental**: uthyrningar (koppling mellan kund, film och datum)
- **staff**: personal som hanterar uthyrningarna
- **inventory**: lagerposter som kopplar filmer till butiker
