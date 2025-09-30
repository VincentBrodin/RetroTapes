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
- **Bootstrap**: Används för layout och styling av applikationen


### Arkitektur

Projektet följer en klassisk MVC-struktur (Model–View–Controller) med tydlig separation mellan logik, datahantering och presentation.

- **Model**: Datamodeller för filmer, kunder, uthyrningar m.m. (genererade från Sakila-databasen).
- **View**: Razor-sidor för att visa listor, detaljer och historik.
- **Controller**: Logik för att hantera hyror, sökningar och filtrering.


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
