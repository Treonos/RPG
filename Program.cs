using System;
class Program
{
    static void Main()
    {
        WriteColor("WELCOME TO PABLO MUSHROOMS\n", ConsoleColor.Magenta);

        while (true)
        {
            int enemyNum = 1;
            Player player = new Player();
            int hpRange = 5;
            int strRange = 5;
            int spdRange = 5;

            while (player.Hp > 0) // Bitwa
            {
                Enemy enemy = new Enemy(hpRange, strRange, spdRange, enemyNum); // Tworzenie nowego przeciwnika
                while (enemy.Hp > 0 && player.Hp > 0)
                {
                    bool playerAttacksFirst = player.Spd >= enemy.Spd;
                    Thread.Sleep(1000);
                    if (playerAttacksFirst) // Jeżeli gracz jest szybszy od przeciwnika to atakuje jako pierwszy
                    {
                        enemy.TakeDamage(player.username, player.Str, player.critChance, player.critMultiplier);
                        if (enemy.Hp > 0)
                        {
                            player.TakeDamage(enemy.Str);
                        }
                    }
                    else // Jeżeli gracz jest wolniejszy od przeciwnika to atakuje jako drugi
                    {
                        player.TakeDamage(enemy.Str);
                        if (player.Hp > 0)
                        {
                            enemy.TakeDamage(player.username, player.Str, player.critChance, player.critMultiplier);
                        }
                    }
                    Console.WriteLine();
                }

                if (enemy.Hp <= 0) // Jeżeli zdrowie przeciwnika spadnie do 0, wygrywasz bitwe
                {
                    ConsoleKeyInfo input;
                    Thread.Sleep(1000);
                    Console.WriteLine("You defeated the enemy!\n");
                    player.Heal(); // Leczenie gracza
                    player.GainExp(Convert.ToInt32((enemy.maxHp + enemy.Str + enemy.Spd) / 3));// Funkcja definiująca ile gracz otrzyma doświadczenia
                    if (enemyNum++ % 3 == 0) player.GetItem();

                    while (true)
                    {
                        Console.Write("\nPress Enter to continue or press I to view your inventory ");
                        input = Console.ReadKey();
                        if (input.Key == ConsoleKey.Enter) break;
                        
                        else if (input.Key == ConsoleKey.I) player.ViewInventory(); // Otwieranie ekwipunku
                        
                        else WriteColor("\nInvalid input!", ConsoleColor.Red);
                    }

                    hpRange += 1 + Convert.ToInt32(hpRange * 0.10);
                    strRange += 1 + Convert.ToInt32(strRange * 0.10);
                    spdRange += 1 + Convert.ToInt32(spdRange * 0.10); // Zwiększenie zakresu statystyk przeciwnika
                }
                else if (player.Hp <= 0) // Jeżeli zdrowie gracza spadnie do 0 to gracz przegrywa gre
                {
                    WriteColor("\nGAME OVER\n", ConsoleColor.Red);
                    enemyNum = 0;
                }
            }
        }
    }

    static void WriteColor(string message, ConsoleColor color = ConsoleColor.White) // Funkcja do wypisywania w konsoli kolorem
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }
}

class Player
{
    // Statystyki ekwipunku
    public int WeaponStr { get; private set; }
    public int WeaponSpd { get; private set; }
    public int ArmorHp { get; private set; }
    public int ArmorStr { get; private set; }
    public int BootsHp { get; private set; }
    public int BootsSpd { get; private set; }
    // Statystyki gracza
    public int Hp { get; private set; }
    public int Str { get; private set; }
    public int Spd { get; private set; }
    // Szansa na cios krytyczny 
    public float critChance = 12.5f;
    // Mnożnik obrażeń krytycznych
    public float critMultiplier = 2f;

    public string username = "";
    public int AvailableStatPoints = 0;
    private int expNeeded = 10;
    private int currentExp = 0;
    private int maxHp;
    public int level = 1;
    public int maxItemStat = 1;

    public int pastWeaponStr = 0, pastWeaponSpd = 0;
    public int pastArmorStr = 0, pastArmorHp = 0;
    public int pastBootsHp = 0, pastBootsSpd = 0;

    private void WriteColor(string message, ConsoleColor color = ConsoleColor.White) // Funkcja do wypisywania w konsoli kolorem
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }


    public Player() // Tworzenie gracza
    {
        while (true)
        {
            Console.Write("\nEnter your name: ");
            username = Console.ReadLine();
            if (ConfirmInput())
            {
                ChooseClass(); // Wybór klasy
                ChoosePath();  // Wybór ścieżki
                break;
            }
        }
    }

    private void ChooseClass()
    {
        Console.WriteLine($"\nChoose your class, {username}: ");
        Console.WriteLine("1. Knight");
        Console.WriteLine("2. Mage");
        Console.WriteLine("3. Archer");

        while (true)
        {
            Console.Write("\nI choose option number: ");
            ConsoleKeyInfo optNum = Console.ReadKey();

            if (optNum.Key == ConsoleKey.D1)
            {
                maxHp = 15; Str = 8; Spd = 3;
            }
            else if (optNum.Key == ConsoleKey.D2)
            {
                maxHp = 8; Str = 13; Spd = 6;
            }
            else if (optNum.Key == ConsoleKey.D3)
            {
                maxHp = 6; Str = 9; Spd = 11;
            }
            else
            {
                WriteColor("\nIncorrect input!", ConsoleColor.Red);
                continue;
            }
            Hp = maxHp;

            if (ConfirmInput())
            {
                Display();
                break;
            }
        }
    }

    private void ChoosePath()
    {
        Console.WriteLine("\nChoose your path: ");
        Console.WriteLine("1. Desert");
        Console.WriteLine("2. Castle");
        Console.WriteLine("3. Swamp");
        Console.WriteLine("4. Mountains");

        while (true)
        {
            Console.Write("\nI choose option number: ");
            ConsoleKeyInfo optNum = Console.ReadKey();

            //string optName;

            if (optNum.Key == ConsoleKey.D1)
            {
                //optName = "Desert";

            }
            else if (optNum.Key == ConsoleKey.D2)
            {
                //optName = "Castle";

            }
            else if (optNum.Key == ConsoleKey.D3)
            {
                //optName = "Swamp";

            }
            else if (optNum.Key == ConsoleKey.D4)
            {
                //optName = "Mountains";

            }
            else
            {
                WriteColor("\nIncorrect input!", ConsoleColor.Red);
                continue;
            }

            if (ConfirmInput())
            {
                break;
            }
        }
    }

    private bool ConfirmInput()
    {
        while (true)
        {
            Console.Write("\nPress Y to confirm or X to change ");
            ConsoleKeyInfo confirm = Console.ReadKey();
            Console.WriteLine();

            if (confirm.Key == ConsoleKey.Y)
            {
                return true;
            }
            else if (confirm.Key == ConsoleKey.X)
            {
                return false;
            }
            else
            {
                WriteColor("\nInvalid input!", ConsoleColor.Red);
            }
        }
    }

    public void AssignStatPoints(string stat, int points) // Przypisywanie statystyk
    {
        if (points > AvailableStatPoints)
        {
            WriteColor("Not enough points!", ConsoleColor.Red); // Nie wystarczająca ilość punktów
            return;
        }

        else if (points < 0)
        {
            WriteColor("You cannot assign negative points!", ConsoleColor.Red); // Nie można przypisać ujemnej liczby punktów
            return;
        }

        switch (stat.ToLower()) // Dodawanie statystyk
        {
            case "hp":
            case "health":
                maxHp += points;
                Hp += points;
                break;
            case "str":
            case "strength":
                Str += points;
                break;
            case "spd":
            case "speed":
                Spd += points;
                break;
            default:
                WriteColor("Invalid stat!", ConsoleColor.Red); // Niepoprawna statystyka
                return;
        }

        AvailableStatPoints -= points;
        Console.WriteLine($"{points} Points assigned to {stat}.");
        WriteColor($"\nPoints left: {AvailableStatPoints}", ConsoleColor.DarkMagenta);
    }
    public void Display() // Wyświetlanie aktualnych statystyk gracza
    {
        Console.WriteLine($"\nCurrent stats:\nHealth: {Hp}/{maxHp}  |  Strength: {Str}  |  Speed: {Spd}");
    }
    public void TakeDamage(int damage) // Otrzymywanie obrażeń
    {
        Random rand = new Random();
        int damageTaken = rand.Next(Convert.ToInt32(0.9 * damage), Convert.ToInt32(1.1 * damage));
        Thread.Sleep(1000);
        Console.WriteLine($"\nThe enemy attacks {username}!");
        Hp -= damageTaken;
        Console.Write("You take ");
        WriteColor(Convert.ToString(damageTaken), ConsoleColor.Red);
        Console.Write($" damage, {username}'s health: ");
        if (Hp < 0) Hp = 0;
        WriteColor($"{Convert.ToString(Hp)}/{Convert.ToString(maxHp)}", ConsoleColor.Green);
    }
    public void Heal() // Leczenie gracza
    {
        if (Hp < maxHp)
        {
            int heal = Convert.ToInt32(0.75 * maxHp);
            if (Hp + heal <= maxHp)
            {
                Hp += heal;
                Thread.Sleep(1000);
                WriteColor($"You healed by {heal}. Current health: {Hp}\n", ConsoleColor.Green);
            }
            else
            {
                Hp = maxHp;
                Thread.Sleep(1000);
                WriteColor($"You healed by {heal}. Current health: {Hp}\n", ConsoleColor.Green);
            }
        }
    }
    public void GainExp(int exp) // Zdobywanie expa
    {
        Random rand = new Random();
        exp = rand.Next(exp, Convert.ToInt32(1.2 * exp));
        currentExp += exp;
        Thread.Sleep(1000);
        WriteColor($"{exp} experience points earned. Current exp: {currentExp}/{expNeeded}\n", ConsoleColor.Blue);

        if (currentExp >= expNeeded)
        {
            LevelUp();
            currentExp -= expNeeded;
            expNeeded = Convert.ToInt32(expNeeded * 1.6);
        }
    }
    public void LevelUp() // Level up
    {
        level++;
        Thread.Sleep(1000);
        WriteColor($"\nLevel Up! You gain {Convert.ToInt32(level * 1.6)} points to assign.\n", ConsoleColor.DarkMagenta);
        AvailableStatPoints += Convert.ToInt32(level * 1.6);

        while (AvailableStatPoints > 0)
        {
            Display();
            string stat = GetStatInput();

            if (IsValidStat(stat))
            {
                AssignPointsToStat(stat);
            }
            else
            {
                WriteColor("Invalid stat!", ConsoleColor.Red);
            }
        }
        Display();
    }
    private string GetStatInput()
    {
        Console.Write("Which stat do you want to assign points to (hp, str, spd)? ");
        return Console.ReadLine();
    }

    private bool IsValidStat(string stat)
    {
        return stat.Equals("hp", StringComparison.OrdinalIgnoreCase) ||
               stat.Equals("health", StringComparison.OrdinalIgnoreCase) ||
               stat.Equals("str", StringComparison.OrdinalIgnoreCase) ||
               stat.Equals("strength", StringComparison.OrdinalIgnoreCase) ||
               stat.Equals("spd", StringComparison.OrdinalIgnoreCase) ||
               stat.Equals("speed", StringComparison.OrdinalIgnoreCase);
    }
    private void AssignPointsToStat(string stat) // Przypisywanie punktów do statystyk
    {
        Console.Write("How many points do you want to assign? ");
        try
        {
            int points = Convert.ToInt32(Console.ReadLine());

            AssignStatPoints(stat, points);
        }
        catch (FormatException)
        {
            WriteColor("Invalid input!", ConsoleColor.Red); // Niepoprawny wybrany numer
        }
        catch (OverflowException)
        {
            WriteColor("Invalid input!", ConsoleColor.Red); // Niepoprawny wybrany numer
        }
    }
    public void GetItem() // Otrzymanie przedmiotu
    {
        Random rand = new Random();
        int typeNum = rand.Next(1, 4);

        switch (typeNum)
        {
            case 1: Weapon(); break;
            case 2: Armor(); break;
            case 3: Boots(); break;
        }
    }
    private void Weapon() // Tworzenie broni
    {
        Random rand = new Random();
        displayItem("Weapon");
        WeaponStr = (1 + rand.Next(Convert.ToInt32(maxItemStat / 2), Convert.ToInt32(maxItemStat * 2))) * 2;
        WeaponSpd = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), Convert.ToInt32(maxItemStat * 2));

        Console.WriteLine($"New weapon stats:\nMaxHp:    0    |   Str:    {WeaponStr}   |   Spd:    {WeaponSpd}\n");
        Console.Write($"Equipped weapon stats:\n");
        if (pastWeaponStr > 0)
        {
            Console.Write($"MaxHp:    0    |   Str:    {pastWeaponStr}   |   Spd:    {pastWeaponSpd}\n");
        }
        else
        {
            WriteColor("No weapon equipped\n", ConsoleColor.Red);
        }
        while (true)
        {
            Console.Write("\nPress Y to equip the new item or X to discard it ");
            ConsoleKeyInfo confirm = Console.ReadKey();
            if (confirm.Key == ConsoleKey.Y)
            {
                Str += WeaponStr;
                Spd += WeaponSpd;

                Str -= pastWeaponStr;
                Spd -= pastWeaponSpd;

                pastWeaponStr = WeaponStr;
                pastWeaponSpd = WeaponSpd;

                Display();
                break;
            }
            else if (confirm.Key == ConsoleKey.X)
            {
                WriteColor("\nItem discarded!", ConsoleColor.Red);
                break;
            }
            else
            {
                WriteColor("Invalid input", ConsoleColor.Red);
                continue;
            }
        }

    }
    private void Armor() // Tworzenie zbroi
    {
        Random rand = new Random();
        displayItem("Armor");
        ArmorHp = (1 + rand.Next(Convert.ToInt32(maxItemStat / 2), Convert.ToInt32(maxItemStat * 2))) * 2;
        ArmorStr = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), Convert.ToInt32(maxItemStat * 2));

        Console.WriteLine($"New armor stats:\nMaxHp:    {ArmorHp}    |   Str:    {ArmorStr}   |   Spd:    0\n");
        Console.WriteLine($"Equipped armor stats:\n");
        if (pastArmorHp > 0)
        {
            Console.Write($"MaxHp:    {pastArmorHp}    |   Str:    {pastArmorStr}   |   Spd:    0\n");
        }
        else
        {
            WriteColor("No armor equipped\n", ConsoleColor.Red);
        }

        while (true)
        {
            Console.Write("\nPress Y to equip the new item or N to discard it ");
            ConsoleKeyInfo confirm = Console.ReadKey();
            if (confirm.Key == ConsoleKey.Y)
            {
                maxHp += ArmorHp;
                Str += ArmorStr;

                maxHp -= pastArmorHp;
                Str -= pastArmorStr;

                pastArmorHp = ArmorHp;
                pastArmorStr = ArmorStr;

                Display();
                break;
            }
            else if (confirm.Key == ConsoleKey.X)
            {
                WriteColor("\nItem discarded!", ConsoleColor.Red);
                break;
            }
            else
            {
                WriteColor("Invalid input", ConsoleColor.Red);
                continue;
            }
        }
    }
    private void Boots() // Tworzenie butów
    {
        Random rand = new Random();
        displayItem("Boots");

        BootsSpd = (1 + rand.Next(Convert.ToInt32(maxItemStat / 2), Convert.ToInt32(maxItemStat * 2))) * 2;
        BootsHp = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), Convert.ToInt32(maxItemStat * 2));
        
        Console.WriteLine($"New boots stats:\nMaxHp:    {BootsHp}    |   Str:    0   |   Spd:    {BootsSpd}\n");
        Console.WriteLine($"Equipped boots stats:\n");
        if (pastBootsSpd > 0)
        {
            Console.Write($"MaxHp:    {pastBootsHp}    |   Str:    0   |   Spd:    {pastBootsSpd}\n");
        }
        else
        {
            WriteColor("No boots equipped\n", ConsoleColor.Red);
        }

        while (true)
        {
            Console.Write("\nPress Y to equip the new item or N to discard it ");
            ConsoleKeyInfo confirm = Console.ReadKey();
            if (confirm.Key == ConsoleKey.Y)
            {
                maxHp += BootsHp;
                Spd += BootsSpd;

                maxHp -= pastBootsHp;
                Spd -= pastBootsSpd;

                pastBootsHp = BootsHp;
                pastBootsSpd = BootsSpd;

                Display();
                break;
            }
            else if (confirm.Key == ConsoleKey.X)
            {
                WriteColor("\nItem discarded!", ConsoleColor.Red);
                break;
            }
            else
            {
                WriteColor("Invalid input", ConsoleColor.Red);
                continue;
            }
        }
    }
    public void displayItem(string Type) // Wyświetlanie otrzymanego przedmiotu
    {
        Random rand = new Random();
        WriteColor("\nYou got ", ConsoleColor.DarkYellow);
        int rarityNum = rand.Next(1, 100);
        string Rarity = " ";
        switch (rarityNum) // Losowanie rzadkości przedmiotu
        {
            case var _ when rarityNum >= 50:
                Rarity = "Common";
                WriteColor($"{Rarity}", ConsoleColor.DarkGray);
                break;
            case var _ when rarityNum >= 25:
                Rarity = "Uncommon";
                WriteColor($"{Rarity}", ConsoleColor.DarkGreen);
                break;
            case var _ when rarityNum >= 10:
                Rarity = "Rare";
                WriteColor($"{Rarity}", ConsoleColor.DarkBlue);
                break;
            case var _ when rarityNum >= 4:
                Rarity = "Epic";
                WriteColor($"{Rarity}", ConsoleColor.Magenta);
                break;
            case var _ when rarityNum >= 1:
                Rarity = "Mythical";
                WriteColor($"{Rarity}", ConsoleColor.DarkRed);
                break;
        }
        WriteColor($" {Type}\n", ConsoleColor.DarkYellow);

        switch (Rarity) // Ulepszanie przedmiotu zgodnie z jego rzadkością
        {
            case "Common":
                maxItemStat = Convert.ToInt32(level * 1.2);
                break;
            case "Uncommon":
                maxItemStat = Convert.ToInt32(level * 2.5);
                break;
            case "Rare":
                maxItemStat = Convert.ToInt32(level * 3.4);
                break;
            case "Epic":
                maxItemStat = Convert.ToInt32(level * 5.5);
                break;
            case "Mythical":
                maxItemStat = Convert.ToInt32(level * 8);
                break;
        }
    }

    public void ViewInventory() // Wyświetlanie ekwipunku
    {
        Console.Write($"\nEquipped weapon stats: ");
        if (pastWeaponStr > 0)
        {
            Console.Write($"MaxHp:    0    |   Str:    {pastWeaponStr}   |   Spd:    {pastWeaponSpd}\n");
        }
        else
        {
            WriteColor("No weapon equipped\n", ConsoleColor.Red);
        }

        Console.Write($"Equipped armor stats: ");
        if (pastArmorHp > 0)
        {
            Console.Write($"MaxHp:    {pastArmorHp}    |   Str:    {pastArmorStr}   |   Spd:    0\n");
        }
        else
        {
            WriteColor("No armor equipped\n", ConsoleColor.Red);
        }

        Console.Write($"Equipped boots stats: ");
        if (pastBootsSpd > 0)
        {
            Console.Write($"MaxHp:    {pastBootsHp}    |   Str:    0   |   Spd:    {pastBootsSpd}\n");
        }
        else
        {
            WriteColor("No boots equipped\n", ConsoleColor.Red);
        }
    }
}

class Enemy // Tworzenie przeciwnika
{
    public int Hp { get; private set; }
    public int maxHp { get; private set; }
    public int Str { get; private set; }
    public int Spd { get; private set; }
    private Random rand;
    private void WriteColor(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }

    public Enemy(int hpRange, int strRange, int spdRange, int enemyNum) // Losowanie przeciwnikowi statystyk
    {
        rand = new Random();
        Hp = GenerateStat(hpRange);
        maxHp = Hp;
        Str = GenerateStat(strRange);
        Spd = GenerateStat(spdRange);

        WriteColor($"\nAn enemy [lvl {enemyNum}] appears with {Hp} hp, {Str} str and {Spd} spd\n", ConsoleColor.Yellow);
    }

    private int GenerateStat(int range)
    {
        return rand.Next(Convert.ToInt32(range * 0.5), range * 2);
    }

    public void TakeDamage(string playerUsername, int damage, float playerCritChance, float playerCritMutliplier) // Otrzymywanie obrażeń
    {
        float damageTaken = rand.Next(Convert.ToInt32(0.9 * damage), Convert.ToInt32(1.1 * damage));
        if (rand.Next(1, 100) <= playerCritChance)
        {
            Convert.ToInt32(damageTaken *= playerCritMutliplier);
            Console.WriteLine($"\n{playerUsername} attacks the enemy with a critical hit!");
        }
        else
        {
            Console.WriteLine($"\n{playerUsername} attacks the enemy!");
        }
        Hp -= Convert.ToInt32(damageTaken);
        Thread.Sleep(1000);
        Console.Write("The enemy takes ");
        WriteColor($"{Convert.ToInt32(damageTaken)}", ConsoleColor.Red);
        if (Hp < 0) Hp = 0;
        Console.Write($" damage, enemy's health: ");
        WriteColor($"{Hp}/{maxHp}\n", ConsoleColor.Green);
    }
}