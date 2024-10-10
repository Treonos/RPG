# "PABLO MUSHROOMS"

## 1. OPIS PROJEKTU
Aplikacja "Pablo Mushrooms" to gra tekstowa RPG, która ma na celu zapewnienie rozrywki graczowi.

## 2. Architektura aplikacji
~~ diagram ~~
- Na początku rozgrywki gracz wybiera jedną z trzech dostępnych klas: rycerza, maga lub łucznika. Bohater ma 3 statystyki (punkty zdrowia, siła, szybkość), których wartość podstawowa zależy od wybranej klasy:
  - rycerz ma najwięcej punktów zdrowia, a najmniej szybkości;
  - mag ma największą siłę, ale podobnie jak rycerz ma najmniejszą szybkość;
  - łucznik jest najszybszy, ale ma najmniej punktów zdrowia,
    - punkty zdrowia determinują to, ile obrażeń gracz może przyjąć zanim umrze;
    - siła oznacza jak dużo punktów zdrowia bohater zabierze przeciwnikowi;
    -  od szybkości zależy, która z postaci - bohater czy wróg - zaatakuje pierwsza,
- Po wybraniu klasy bohatera, gracz wybiera jedną z czterech dróg:
  - pustynia;
  - zamek;
  - bagna;
  - góry,
- Po wyborze ścieżki rozpoczyna się właściwa rozgrywka - walka z przeciwnikami, którzy mają losowe statystyki, adekwatne do poziomu gracza.
- Gra kończy się, gdy gracz zostanie pokonany przez przeciwnika;
- Po porażce można zagrać kolejny raz.

## 3. Opis funkcji i funkcjonalności
- Po pokonaniu przeciwnika gracz otrzymuje punkty doświadczenia, których liczba jest średnią statystyk przeciwnika;
- Po osiągnięciu wymaganej ilości punktów doświadczenia, poziom bohatera zostaje zwiększony, podobnie jak ilość punktów doświadczenia wymagana do kolejnego poziomu;
- Po zwiększeniu poziomu bohatera, gracz otrzymuje punkty, za które może ulepszyć swoje statystyki. Liczba tych punktów jest równa iloczynowi poziomu bohatera i liczby 1.6;
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
- 
