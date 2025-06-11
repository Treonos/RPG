# "PABLO MUSHROOMS"
Metalowe płuca:
KK11, FG6, SK10, PB3
## 1. OPIS PROJEKTU
Aplikacja "Pablo Mushrooms" to gra RPG, która ma na celu zapewnienie rozrywki graczowi.

## 2. Architektura aplikacji
- Na początku rozgrywki gracz wybiera jeden z 3 dostępnych zapisów. Jeśli zapis nie istnieje albo gracz chce rozpocząć nową grę, to wpisuje on imię dla swojej postaci oraz wybiera jedną z trzech dostępnych klas: rycerza, maga lub łucznika. Bohater ma 3 statystyki (punkty zdrowia, atak, szybkość), których wartość podstawowa zależy od wybranej klasy:
  - rycerz ma najwięcej punktów zdrowia, a najmniej szybkości;
  - mag ma największy atak, ale podobnie jak rycerz ma najmniejszą szybkość;
  - łucznik jest najszybszy, ale ma najmniej punktów zdrowia,
    - punkty zdrowia determinują to, ile obrażeń gracz może przyjąć zanim umrze;
    - atak oznacza jak dużo punktów zdrowia bohater zabierze przeciwnikowi. Zadane przeciwnikowi obrażenia mogą zostać zwiększone poprzez trafienie obrażeń krytycznych. Szansa na trafienie krytyczne wynosi 10%;
    -  od szybkości zależy, ile ataków na turę będzie mógł wykonać gracz,
- Po wybraniu klasy bohatera, gracz przenosi się do menu gry,
- Następnie gracz może rozpocząć rozgrywkę poprzez wybranie się na jedną z następujących ścieżek:
  - pustynia;
  - zamek;
  - bagna;
  - góry,
- Po wyborze ścieżki gracz wybiera także poziom trudności:
  - łatwy;
  - średni;
  - trudny;
  - koszmarny,
- poziomy trudności i biomy odblokowywuje się poprzez pokonanie określonej liczby przeciwników na poprzednim poziomie trudności lub biomie
- Potem rozpoczyna się właściwa rozgrywka - walka z przeciwnikami, których statystyki są losowo generowane na podstawie ich poziomu. Poziom przeciwnika jest ograniczony dla danych poziomów trudności i biomów.
- Gra kończy się, gdy gracz zostanie pokonany przez przeciwnika;
- Po porażce gracz przechodzi do menu gracza gdzie może ulepszać swoją postać.

## 3. Opis funkcji i funkcjonalności
- Po pokonaniu przeciwnika gracz otrzymuje punkty doświadczenia, których liczba jest średnią statystyk przeciwnika i jest dodatkowo modyfikowana w zależności od poziomu trudności i biomu;
- Po osiągnięciu wymaganej ilości punktów doświadczenia, poziom bohatera zostaje zwiększony, podobnie jak ilość punktów doświadczenia wymagana do kolejnego poziomu;
- Za każdy zwiększony poziom zwiększają się mnożniki do statystyk postaci oraz gracz otrzymuje jeden punkt do przydzielenia do statystyk bazowych;
- W grze są dostępne 3 rodzaje przedmiotów, które zwiększają bohaterowi statystyki:
  - broń;
  - zbroja;
  - amulet,
- Przedmioty mają rzadkość z przydzieloną szansą na uzyskanie:
  - zwykła, 50%;
  - niezwykła, 25%;
  - rzadka, 12%;
  - epicka, 7%;
  - mityczna, 3%;
  - legendarna, 2%;
  - boska, 1%.
- Mnożniki statystyk przedmiotów zależą od poziomu i rzadkości przedmiotu.

- W czasie walki gracz nie może jej opuścić. Gdy walka się skończy gracz może wrócić do menu gracza gdzie może przydzielać punkty, zwiększać swój poziom, oraz zakładać przedmioty oraz je niszczyć,
- Po zakończeniu walki z przeciwnikiem gracz leczy się;
- Wszelkie dane z gry zapisywane są do bazy danych.

## 4. Instrukcja instalacji i uruchomienia
- Instalacja Microsoft Visual Studio i uruchomienie gry:
  - pobierz program Microsoft Visual Studio https://visualstudio.microsoft.com/pl/;
  - podczas pobierania Microsoft Visual Studio pobierz pakiet .NET MAUI;
  - po pobraniu Microsoft Visual Studio pobierz folder projektu i uruchom plik RPG.sln;
- Wymagania systemowe i programowe:
  - gra kompatybilna jest z systemem operacyjnym Windows (10 i 11) oraz urządzeniami mobilnymi;
  - wersja .NET MAUI: 10.0.

## 5. Dokumentacja kodu
- Klasa Player - Zawarte w klasie metody odpowiadają za wybór klasy bohatera, stworzenie jego ekwipunku, przyjęcie wyboru biomu, na który chce iść gracz i przydzielanie punktów doświadczenia oraz zwiększanie statystyk gracza po osiągnięciu kolejnego poziomu. Jej konstruktor wyświetla kreator postaci i odpowiada za zapisanie imienia gracza.
- Klasa Enemy - Jej konstruktor rozpoczyna tworzenie przeciwnika i losuje mu imię na podstawie biomu wybranego wcześniej przez gracza. Metody tej klasy pozwalają na generowanie statystyk przeciwnika i odbieranie mu punktów zdrowia po ataku gracza.
  
## 6. Błędy i ich obsługa
- Program wyrzuca okienko z informacją, gdy gracz pozostawi puste pole z imieniem. Gracz musi więc te dane wpisać.
- Program blokuje możliwość wczytania zapisu z bazy jeśli nie istnieje
  
## 7.Wnioski i przyszłe usprawnienia
- Wnioski z pracy nad projektem:
  - dobrze działała komunikacja w grupie, dzięki czemu możliwe było sprawne napisanie gry i ograniczenie błędów w kompilacji do minimum;
  - udało się napisać prosty i czytelny kod oraz stworzyć interesującą i łatwą do rozbudowy grę;
  - napotkane zostały także pewne trudności:
    - problemy z implementacją łączenia z bazą co udało się rozwiązać instalując odpowiednie pakiety Nuget.
- Elementy do przyszłej poprawy i rozbudowy:
  - zbalansowanie zwiększania statystyk gracza przez przedmioty oraz dodanie dodatkowych umiejętności pasywnych dla rzadszych broni;
  - dodanie waluty i sklepu, w którym za zgromadzone pieniądze można będzie można nabywać przedmioty;
