# "PABLO MUSHROOMS"
Metalowe płuca:
KK11, FG6, SK10, PB3
## 1. OPIS PROJEKTU
Aplikacja "Pablo Mushrooms" to gra RPG, która ma na celu zapewnienie rozrywki graczowi.

## 2. Architektura aplikacji
- Na początku rozgrywki gracz wybiera jeden z 3 dostępnych zapisów. Jeśli zapis nie istnieje albo gracz chce rozpocząć nową grę, to wpisuje on imię dla swojej postaci oraz wybiera jedną z trzech dostępnych klas: rycerza, maga lub łucznika. Bohater ma 3 statystyki (punkty zdrowia, siła, szybkość), których wartość podstawowa zależy od wybranej klasy:
  - rycerz ma najwięcej punktów zdrowia, a najmniej szybkości;
  - mag ma największą siłę, ale podobnie jak rycerz ma najmniejszą szybkość;
  - łucznik jest najszybszy, ale ma najmniej punktów zdrowia,
    - punkty zdrowia determinują to, ile obrażeń gracz może przyjąć zanim umrze;
    - siła oznacza jak dużo punktów zdrowia bohater zabierze przeciwnikowi. Zadane przeciwnikowi obrażenia mogą zostać zwiększone poprzez trafienie obrażeń krytycznych. Szansa na trafienie krytyczne wynosi 12,5%;
    -  od szybkości zależy, która z postaci - bohater czy wróg - zaatakuje pierwsza,
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
- Potem rozpoczyna się właściwa rozgrywka - walka z przeciwnikami, którzy mają losowe statystyki, których zakres zwiększa się z każdym pokonanym przeciwnikiem.
- Gra kończy się, gdy gracz zostanie pokonany przez przeciwnika;
- Po porażce można zagrać kolejny raz.

## 3. Opis funkcji i funkcjonalności
- Po pokonaniu przeciwnika gracz otrzymuje punkty doświadczenia, których liczba jest średnią statystyk przeciwnika;
- Po osiągnięciu wymaganej ilości punktów doświadczenia, poziom bohatera zostaje zwiększony, podobnie jak ilość punktów doświadczenia wymagana do kolejnego poziomu;
- Po zwiększeniu poziomu bohatera, gracz otrzymuje punkty, dzięki którym może ulepszyć swoje statystyki. Liczba tych punktów jest równa iloczynowi poziomu bohatera i liczby 2.5;
- W grze są dostępne 3 rodzaje przedmiotów, które zwiększają bohaterowi statystyki:
  - broń;
  - zbroja;
  - amulet,
- Przedmioty mają rzadkość i szanse na trafienie danej rzadkości:
  - zwykła, 50%;
  - niezwykła, 25%;
  - rzadka, 12%;
  - epicka, 7%;
  - mityczna, 3%;
  - legendarna, 2%;
  - boska, 1%.
- Zwiększanie odpowiednich dla danego przedmiotu statystyk bohatera zależy od iloczynu poziomu bohatera i stałej liczby przypisanej do rzadkości przedmiotu:
  - dla zwykłej stała wynosi 1,5;
  - dla niezwykłej 2;
  - dla rzadkiej 3;
  - dla epickiej 6;
  - dla mitycznej 10;
  - dla legendarnej 15;
  - dla boskiej 25.
- Zarówno jak przed podjęciem rozgrywki, tak jak po pokonaniu przeciwnika gracz może wyświetlić swoje statystyki, ulepszyć je poprzez punkty zdobywane po osiągnięciu kolejnego poziomu, a także zobaczyć swój ekwipunek i wybrać przedmioty, które chce założyć. Można je także niszczyć,
- Po zakończeniu walki z przeciwnikiem gracz leczy się 75% maksymalnej ilości życia;
- Wszelkie dane z gry zapisywane są do bazy danych.

## 4. Instrukcja instalacji i uruchomienia
- Instalacja Microsoft Visual Studio i uruchomienie gry:
  - pobierz program Microsoft Visual Studio https://visualstudio.microsoft.com/pl/;
  - podczas pobierania Microsoft Visual Studio pobierz pakiet .NET MAUI;
  - po pobraniu Microsoft Visual Studio pobierz plik Program.cs z najnowszą datą;
- Wymagania systemowe i programowe:
  - gra kompatybilna jest z systemem operacyjnym Windows (10 i 11);
  - wersja .NET: 8.0.

## 5. Dokumentacja kodu
- Klasa Player - jedna z dwóch klas w kodzie gry, dzięki której możliwe jest uruchomienie i poprawne działanie gry. Zawarte w klasie metody odpowiadają za wybór klasy bohatera, stworzenie jego ekwipunku, przyjęcie wyboru biomu, na który chce iść gracz i przydzielanie punktów doświadczenia oraz zwiększanie statystyk gracza po osiągnięciu kolejnego poziomu. Jej konstruktor o tej samej nazwie wyświetla kreator postaci i odpowiada za zapisanie imienia gracza.
- Klasa Enemy - ostatnia z dwóch klas w kodzie gry, dzięki której możliwe jest poprawne działanie gry. Jej konstruktor o tej samej nazwie rozpoczyna tworzenie przeciwnika i losuje mu imię na podstawie biomu wybranego wcześniej przez gracza. Metody tej klasy pozwalają na generowanie statystyk przeciwnika i odbieranie mu punktów zdrowia po ataku gracza.
  
## 6. Błędy i ich obsługa
- Program wyrzuca okienko z informacją, gdy gracz pozostawi puste pole z imieniem. Gracz musi więc te dane wpisać. 
  
## 7.Wnioski i przyszłe usprawnienia
- Wnioski z pracy nad projektem:
  - dobrze działała komunikacja w grupie, dzięki czemu możliwe było sprawne napisanie gry i ograniczenie błędów w kompilacji do minimum;
  - ddało się napisać prosty i czytelny kod oraz stworzyć interesującą i podatną na rozbudowę fabułę​;
  - napotkane zostały także pewne trudności:
    - nieprzemyślane pisanie kodu, a przez to trudności z modyfikacją i rozbudową gry w późniejszym czasie.
- Elementy do przyszłej poprawy i rozbudowy:
  - zbalansowanie zwiększania statystyk gracza przez przedmioty oraz dodanie dodatkowych umiejętności pasywnych dla rzadszych broni;
  - dodanie waluty i sklepu, w którym za zgromadzone pieniądze można będzie można nabywać przedmioty;
