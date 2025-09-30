# RetroTapes

## Syfte
Syftet med RetroTapes är att utveckla ett internt system för hantering av uthyrning av VHS-filmer.
Systemet ska stödja personalen i deras dagliga arbete genom att tillhandahålla funktioner för att söka,
filtrera och administrera filmer,
samt följa upp aktiva uthyrningar och tidigare historik.

## Mål

- Tillhandahålla ett användarvänligt gränssnitt där personalen enkelt kan:    
  -   Lista, söka och filtrera filmer  
  -   Se aktiva uthyrningar  
  -   Få överblick över uthyrningshistorik  
-   Implementera en strukturerad och modulär arkitektur som är lätt att underhålla och vidareutveckla.  
-   Använda etablerade ramverk och verktyg inom .NET-miljön för att säkerställa kvalitet och stabilitet.  
      
    

## Avgränsningar
-   Systemet är ett internt verktyg och kommer inte att ha traditionell användarregistrering eller inloggning.  
-   Istället finns en dropdown-meny där personalen väljer vilken medarbetare som är aktiv.  
-   Systemet är i nuläget inte tänkt att vara publikt tillgängligt för kunder.  
-   Funktioner som avancerad administration (t.ex. fakturering, automatiska påminnelser) ingår inte i denna version.  
      
    

## Tekniska val
-  **Språk och ramverk**: .NET (C#)  
-  **Frontend**: Razor Pages för att bygga användargränssnittet  
-  **Stilmall och layout**: Bootstrap för responsiv design och färdiga UI-komponenter  
-  **Databashantering**: Entity Framework (EF) för att generera datamodeller och kommunicera med databasen  
-  **Data Access Layer (DAL)**: Infört som ett separat lager för att abstrahera dataåtkomst och minska kopplingen mellan EF och Razor  
-  **Databas**: Sakila - en exempeldatabas med realistisk struktur för filmbutik (film, customer, rental, staff, inventory m.m.)  
      
    

## Arkitektur

RetroTapes följer en lagerbaserad arkitektur med tydlig separation mellan presentation, logik och datahantering.

- **Presentation (Razor Views)**: Användargränssnittet som personalen interagerar med.  
- **Controller**: Hanterar logik, tar emot input från användaren och anropar DAL.  
- **DAL (Data Access Layer)**: Ansvarar för dataåtkomst, affärslogik kopplad till databasanrop och abstraktion mot EF.  
- **Entity Framework (EF)**: ORM-verktyg som mappas mot Sakila-databasen.  
- **Databas (Sakila)**: Hanterar uthyrningsinformation, kunder, filmer och personal.
    

## Hosting
Eftersom RetroTapes är ett internt system och inte är avsett att användas av kunder externt,
är den bästa lösningen att hosta applikationen lokalt i organisationens nätverk. Detta ger flera fördelar:

- **Enkel åtkomst för personalen**: systemet kan köras direkt från interna servrar eller en utvecklingsdator.  
- **Låg komplexitet**:  ingen extern hosting eller molntjänst behövs i nuläget, vilket minskar både kostnader och tekniska beroenden.  
- **Datasäkerhet**: all information lagras inom organisationens nätverk och lämnar inte interna miljön.  

För framtida versioner kan man överväga att hosta systemet i Azure App Service eller annan molnplattform om behovet uppstår att skala upp,
tillgängliggöra systemet utanför det interna nätverket, eller dra nytta av DevOps-verktyg för CI/CD.

  
  

## Användarflöde
1.  Personal väljer sin roll via dropdown-menyn.  
2.  Filmlistor kan listas, filtreras eller sökas fram.  
3.  När en uthyrning registreras kopplas den till vald kund och film.  
4.  Systemet uppdaterar databasen och visar aktiv uthyrning.  
5.  Historik kan granskas för att se tidigare hyror.  

## Framtida utveckling
-   Förbättrade sök och filterfunktioner (t.ex. baserade på skådespelare, kategori eller språk).  
-   Statistik och rapportmodul (mest hyrda filmer, populära kategorier).  
-   Utökat administrationsstöd för kundinformation.  
-   Vidareutveckling av DAL för mer avancerad affärslogik.  
-   Möjlighet till integration med externa system (t.ex. fakturering eller e-postnotifieringar).
