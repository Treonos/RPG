# "PABLO MUSHROOMS"
Matalowe płuca:
KK11, FG6, SK10, PB3
## 1. OPIS PROJEKTU
Aplikacja "Pablo Mushrooms" to gra tekstowa RPG, która ma na celu zapewnienie rozrywki graczowi.

## 2. Architektura aplikacji
~~ diagram ~~
- Na początku rozgrywki gracz wybiera jedną z trzech dostępnych klas: rycerza, maga lub łucznika. Bohater ma 3 statystyki (punkty zdrowia, siła, szybkość), których wartość podstawowa zależy od wybranej klasy:
  - rycerz ma najwięcej punktów zdrowia, a najmniej szybkości;
  - mag ma największą siłę, ale podobnie jak rycerz ma najmniejszą szybkość;
  - łucznik jest najszybszy, ale ma najmniej punktów zdrowia,
    - punkty zdrowia determinują to, ile obrażeń gracz może przyjąć zanim umrze;
    - siła oznacza jak dużo punktów zdrowia bohater zabierze przeciwnikowi. Zadane przeciwnikowi obrażenia mogą zostać zwiększone poprzez trafienie obrażeń krytycznych. Szansa na trafienie krytyczne wynosi 12,5%;
    -  od szybkości zależy, która z postaci - bohater czy wróg - zaatakuje pierwsza,
- Po wybraniu klasy bohatera, gracz wybiera jedną z czterech dróg:
  - pustynia;
  - zamek;
  - bagna;
  - góry,
- Po wyborze ścieżki rozpoczyna się właściwa rozgrywka - walka z przeciwnikami, którzy mają losowe statystyki, których zakres zwiększa się z każdym pokonanym przeciwnikiem.
- Gra kończy się, gdy gracz zostanie pokonany przez przeciwnika;
- Po porażce można zagrać kolejny raz.

## 3. Opis funkcji i funkcjonalności
- Po pokonaniu przeciwnika gracz otrzymuje punkty doświadczenia, których liczba jest średnią statystyk przeciwnika z kawałkiem losowości;
- Po osiągnięciu wymaganej ilości punktów doświadczenia, poziom bohatera zostaje zwiększony, podobnie jak ilość punktów doświadczenia wymagana do kolejnego poziomu;
- Po zwiększeniu poziomu bohatera, gracz otrzymuje punkty, dzięki którym może ulepszyć swoje statystyki. Liczba tych punktów jest równa iloczynowi poziomu bohatera i liczby 1.6;
- W grze są dostępne 3 rodzaje przedmiotów, które zwiększają bohaterowi statystyki:
  - broń, która zwiększa ilość zadawanych obrażeń i szybkość, ale nie zwiększa maksymalnej ilości punktów zdrowia;
  - zbroja, która zwiększa maksymalną liczbę punktów zdrowia i ilość zadawanych obrażeń, ale nie zwiększa szybkości;
  - buty, które zwiększają maksymalną liczbę punktów zdrowia i szybkość, ale nie zwiększają ilości zadawanych obrażeń,
- Przedmioty mają rzadkość i szanse na trafienie danej rzadkości:
  - zwykła, 51%;
  - niezwykła, 25%;
  - rzadka, 16%;
  - epicka, 7%;
  - mityczna, 1%.
- Zwiększanie odpowiednich dla danego przedmiotu statystyk bohatera zależy od rzadkości przedmiotu:
  - zwykła zwiększa statystyki o iloczyn poziomu bohatera i liczby 1.1;
  - niezwykła zwiększa statystyki o iloczyn poziomu bohatera i liczby 1.5;
  - rzadka zwiększa statystyki o iloczyn poziomu bohatera i liczby 2;
  - epicka zwiększa statystyki o iloczyn poziomu bohatera i liczby 2.5;
  - mityczna zwiększa statystyki o iloczyn poziomu bohatera i liczby 3,
- Po wybraniu klasy bohatera i po założeniu przedmiotu wyświetlane są statystyki postaci;
- Po zdobyciu nowego przedmiotu wyświetlane jest porównanie nowego przedmiotu i starego, jeśli gracz go posiadał;
- Po zakończeniu walki z przeciwnikiem gracz może wyświetlić swój ekwipunek;
- Po zakończeniu walki z przeciwnikiem gracz leczy się 75% maksymalnej ilości życia;
## 4. Instrukcja instalacji i uruchomienia
- Instalacja Visual Studio i uruchomienie gry:
  - pobierz program Visual Studio https://visualstudio.microsoft.com/pl/;
  - podczas pobierania Visual Studio pobierz aplikacje konsoli C#;
  - po pobraniu Visual Studio pobierz plik PABLO MUSHROOMS i uruchom go w programie,
- Wymagania systemowe i programowe:
  - gra kompatybilna jest z systemem operacyjnym Windows (10 i 11);
  - wersja .NET: 8.0.
## 5. Dokumentacja kodu
- njshfo[
## 6. Przykłady użycia
- Przykładowy scenariusz testowy:

WELCOME TO PABLO MUSHROOMS

Enter your name: Pablo

Press Y to confirm or X to change: y

Choose your class, Pablo:
1. Knight
2. Mage
3. Archer

I choose option number: 2

So, you choose Mage? y/n: y

Current stats:

Health: 8/8  |  Strength: 13  |  Speed: 6

Choose your path:

1. Desert

2. Castle

3. Swamp

4. Mountains

I choose option number: 3

So, you choose Swamp? y/n: y

An enemy appears with 4 hp, 4 str and 2 spd

Pablo attacks the enemy!

The enemy takes 13 damage, enemy's health: 0/4

You defeated the enemy!

5 experience points earned. Current exp: 5/10

Press Enter to continue or press I to view your inventory

An enemy appears with 1 hp, 3 str and 5 spd

Pablo attacks the enemy!

The enemy takes 13 damage, enemy's health: 0/1

You defeated the enemy!

6 experience points earned. Current exp: 11/10

Level Up! You gain 3 points to assign.

Current stats:

Health: 8/8  |  Strength: 13  |  Speed: 6

Which stat do you want to assign points to (hp, str, spd)? hp

How many points do you want to assign? 2

2 Points assigned to hp.

Points left: 1

Current stats:

Health: 10/10  |  Strength: 13  |  Speed: 6

Which stat do you want to assign points to (hp, str, spd)? spd

How many points do you want to assign? 1

1 Points assigned to spd.

Points left: 0

Current stats:

Health: 10/10  |  Strength: 13  |  Speed: 7

Press Enter to continue or press I to view your inventory

An enemy appears with 2 hp, 7 str and 7 spd

Pablo attacks the enemy!

The enemy takes 13 damage, enemy's health: 0/2

You defeated the enemy!

8 experience points earned. Current exp: 9/16

You got Common Weapon

New weapon stats:

MaxHp:    0    |   Str:    2   |   Spd:    1

Equipped weapon stats:

No weapon equipped

Would you like to equip the item?(Your current item will be lost!) y/n y

Current stats:
Health: 10/10  |  Strength: 15  |  Speed: 8

Press Enter to continue or press I to view your inventory i

Equipped weapon stats: MaxHp:    0    |   Str:    2   |   Spd:    1

Equipped armor stats: No armor equipped

Equipped boots stats: No boots equipped

An enemy appears with 16hp, 11str and 9spd

The enemy attacks Pablo!

You take 11 damage, your health: 0/10

GAME OVER!

Would you like to play again? y/n: n

Po wybraniu opcji n następuje koniec gry. W razie wybrania opcji y, rozgrywka rozpoczyna się na nowo.

## 7. Błędy i ich obsługa
- Przy wyborze klasy i drogi w przypadku wybrania złej wartosci program powtarza wybór;
- W przypisywaniu statystyk po wyborze nie poprawnej statystyki lub zbyt dużej wartości program powtarza wybór;
## 8.Wnioski i przyszłe usprawnienia
- Elementy do przyszłej poprawy i rozbudowy:
  - statystyki;
  - przedmioty;
  - sklep z przedmiotami;
