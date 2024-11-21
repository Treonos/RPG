using System;
using System.IO;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading;

class Program
{
    public static string enemyFile = "enemyInfo.txt";
    public static string playerFile = "playerInfo.txt";
    public static string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    public static int enemyNum = 1;
    public static int enemyStatRange = 5;
    public static int enemyStatRangeIncrease = 3;

    public static ConsoleKeyInfo input;

    static void Main()
    {
        WriteColor("WELCOME TO PABLO MUSHROOMS\n", ConsoleColor.Magenta);
        while (true)
        {
            Player player = new();

            UpdatePlayerFile(player);
            UpdateEnemyFile();

            while (player.Hp >= 0) // Bitwa
            {
                Enemy enemy = new(enemyStatRange, enemyNum, player.Biome); // Tworzenie nowego przeciwnika

                while (enemy.Hp > 0 && player.Hp > 0)
                    {
                        bool playerAttacksFirst = player.Spd >= enemy.Spd;
                        Thread.Sleep(800);
                        if (playerAttacksFirst) // Jeżeli gracz jest szybszy od przeciwnika to atakuje jako pierwszy
                        {
                            enemy.TakeDamage(player.Username, player.Str, player.critChance, player.critMultiplier);
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
                                enemy.TakeDamage(player.Username, player.Str, player.critChance, player.critMultiplier);
                            }
                        }
                        Console.WriteLine();
                    }

                    if (enemy.Hp <= 0) // Jeżeli zdrowie przeciwnika spadnie do 0, wygrywasz bitwe
                    {
                        BattleWon(player, enemy);
                    }
            }
            BattleLost();
            Console.Write("\nPress Y to play again or X to exit ");
            input = Console.ReadKey();
            if (input.Key == ConsoleKey.X) break;

            else if (input.Key == ConsoleKey.Y) continue;

            else WriteColor("\nInvalid input!", ConsoleColor.Red);
        }
    }
    public static void BattleWon(Player player, Enemy enemy)
    {
        enemyStatRange += enemyStatRangeIncrease; // Zwiększenie zakresu statystyk przeciwnika
        enemyStatRangeIncrease += enemyStatRangeIncrease;
        enemyNum++;

        UpdateEnemyFile();

        Thread.Sleep(800);
        if (!(enemyNum % 10 == 0)) 
            Console.WriteLine("You defeated the enemy!\n");
        else 
            Console.WriteLine("You defeated the boss!\n");
        player.Heal(); // Leczenie gracza
        int expGained = Convert.ToInt32((enemy.MaxHp + enemy.Str + enemy.Spd) / 3);
        player.GainExp(expGained);
        if (!(enemyNum % 10 == 0))
        {
            if (enemyNum % 3 == 0) player.GetItem();
        }
        else
        {
            player.GetItem();
        }
        while (true)
        {
            Console.Write("\nPress Enter to continue or press I to view your inventory ");
            input = Console.ReadKey();
            if (input.Key == ConsoleKey.Enter) break;

            else if (input.Key == ConsoleKey.I) player.ViewInventory(); // Otwieranie ekwipunku

            else WriteColor("\nInvalid input!", ConsoleColor.Red);
        }
        if (enemyNum % 10 == 0) player.ChoosePath();
        UpdatePlayerFile(player);
    }

    public static void BattleLost()
    {
        WriteColor("\nGAME OVER\n", ConsoleColor.Red);
        enemyNum = 1;
    }

    public static void UpdateEnemyFile()
    {
        using StreamWriter outputFile = new(Path.Combine(docPath, enemyFile));
            outputFile.WriteLine(enemyNum);
            outputFile.WriteLine(enemyStatRange);
            outputFile.WriteLine(enemyStatRangeIncrease);
    }

    public static void UpdatePlayerFile(Player player)
    {
        using StreamWriter outputFile = new(Path.Combine(docPath, playerFile));
            outputFile.WriteLine(player.Username);
            outputFile.WriteLine(player.Level);
            outputFile.WriteLine(player.Class);
            outputFile.WriteLine(player.Biome);
            outputFile.WriteLine(player.currentExp);
            outputFile.WriteLine(player.expNeeded);
            outputFile.WriteLine(player.Hp);
            outputFile.WriteLine(player.MaxHp);
            outputFile.WriteLine(player.Str);
            outputFile.WriteLine(player.Spd);
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
    // Biomy
    public string Biome { get; private set; }
    public string Class { get; private set; }

    public float critChance = 12.5f; // Szansa na cios krytyczny 
    public float critMultiplier = 1.5f; // Mnożnik obrażeń krytycznych

    public string Username = "";
    public int AvailableStatPoints = 0;
    public int expNeeded = 25;
    public int currentExp = 0;
    public int MaxHp;
    public int Level = 1;
    public int maxItemStat;

    public int pastWeaponStr = 0, pastWeaponSpd = 0;
    public int pastArmorStr = 0, pastArmorHp = 0;
    public int pastBootsHp = 0, pastBootsSpd = 0;

    public string WeaponRarity = "";
    public string pastWeaponRarity = "";
    public string ArmorRarity = "";
    public string pastArmorRarity = "";
    public string BootsRarity = "";
    public string pastBootsRarity = "";

    public bool isDesertUsed = false;
    public bool isCastleUsed = false;
    public bool isSwampUsed = false;
    public bool isMountainsUsed = false;

    static void WriteColor(string message, ConsoleColor color = ConsoleColor.White) // Funkcja do wypisywania w konsoli kolorem
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
            Username = Console.ReadLine();
            if (ConfirmInput())
            {
                ChooseClass(); // Wybór klasy
                ChoosePath();  // Wybór ścieżki
                break;
            }
        }
    }

    private void ChooseClass() // Wybieranie klasy
    {
        Console.WriteLine($"\nChoose your class, {Username}: ");
        Console.WriteLine("1. Knight");
        Console.WriteLine("2. Mage");
        Console.WriteLine("3. Archer");

        while (true)
        {
            Console.Write("\nI choose option number: ");
            ConsoleKeyInfo optNum = Console.ReadKey();

            if (optNum.Key == ConsoleKey.D1)
            {
                MaxHp = 105; Str = 11; Spd = 6;
            }
            else if (optNum.Key == ConsoleKey.D2)
            {
                MaxHp = 55; Str = 17; Spd = 10;
            }
            else if (optNum.Key == ConsoleKey.D3)
            {
                MaxHp = 40; Str = 10; Spd = 17;
            }
            else
            {
                WriteColor("\nIncorrect input!", ConsoleColor.Red);
                continue;
            }
            Hp = MaxHp;

            if (ConfirmInput())
            {
                DisplayPlayerStats();
                break;
            }
        }
    }

    public void ChoosePath() // Wybieranie ścieżki
    {
        var paths = new List<string>();
        if (!isDesertUsed)
        {
            paths.Add("Desert");
        }
        if (!isCastleUsed)
        {
            paths.Add("Castle");
        }
        if (!isSwampUsed)
        {
            paths.Add("Swamp");
        }
        if (!isMountainsUsed)
        {
            paths.Add("Mountains");
        }
        Console.WriteLine("\nChoose your path: ");

        for (int i = 0; i < paths.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {paths[i]}");
        }

        while (true)
        {
            Console.Write("\nI choose option number: ");
            ConsoleKeyInfo optNum = Console.ReadKey();

            if (paths.Count == 4)
            {
                if (optNum.Key == ConsoleKey.D1)
                {
                    Biome = paths[0];
                }
                else if (optNum.Key == ConsoleKey.D2)
                {
                    Biome = paths[1];
                }
                else if (optNum.Key == ConsoleKey.D3)
                {
                    Biome = paths[2];
                }
                else if (optNum.Key == ConsoleKey.D4)
                {
                    Biome = paths[3];
                }
                else
                {
                    WriteColor("\nIncorrect input!", ConsoleColor.Red);
                    continue;
                }
            }

            else if (paths.Count == 3)
            {
                if (optNum.Key == ConsoleKey.D1)
                {
                    Biome = paths[0];
                }
                else if (optNum.Key == ConsoleKey.D2)
                {
                    Biome = paths[1];
                }
                else if (optNum.Key == ConsoleKey.D3)
                {
                    Biome = paths[2];
                }
                else
                {
                    WriteColor("\nIncorrect input!", ConsoleColor.Red);
                    continue;
                }
            }
            else if (paths.Count == 2)
            {
                if (optNum.Key == ConsoleKey.D1)
                {
                    Biome = paths[0];
                }
                else if (optNum.Key == ConsoleKey.D2)
                {
                    Biome = paths[1];
                }
                else
                {
                    WriteColor("\nIncorrect input!", ConsoleColor.Red);
                    continue;
                }
            }
            else if (paths.Count == 1)
            {
                if (optNum.Key == ConsoleKey.D1)
                {
                    Biome = paths[0];
                }
                else
                {
                    WriteColor("\nIncorrect input!", ConsoleColor.Red);
                    continue;
                }
            }
            if (ConfirmInput())
            {
                if (paths.Count > 1)
                {
                    if (Biome == "Desert")
                    {
                        isDesertUsed = true;
                    }
                    else if (Biome == "Castle")
                    {
                        isCastleUsed = true;
                    }
                    else if (Biome == "Swamp")
                    {
                        isSwampUsed = true;
                    }
                    else
                    {
                        isMountainsUsed = true;
                    }
                }
                break;
            }
        }
    }

    static bool ConfirmInput() // Potwierdzenie wyboru
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
                MaxHp += points * 3;
                Hp += points * 3;
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
    public void DisplayPlayerStats() // Wyświetlanie aktualnych statystyk gracza
    {
        Console.WriteLine($"\nCurrent stats:\nHealth: {Hp}/{MaxHp}  |  Strength: {Str}  |  Speed: {Spd}");
    }
    public void TakeDamage(int damage) // Otrzymywanie obrażeń
    {
        Random rand = new ();
        int damageTaken = rand.Next(Convert.ToInt32(0.9 * damage), Convert.ToInt32(1.1 * damage));
        Thread.Sleep(800);
        Console.WriteLine($"\nThe enemy attacks {Username}!");
        Hp -= damageTaken;
        Console.Write("You take ");
        WriteColor(Convert.ToString(damageTaken), ConsoleColor.Red);
        Console.Write($" damage, {Username}'s health: ");
        if (Hp < 0) Hp = 0;
        WriteColor($"{Convert.ToString(Hp)}/{Convert.ToString(MaxHp)}", ConsoleColor.Green);
    }
    public void Heal() // Leczenie gracza
    {
        if (Hp < MaxHp)
        {
            int heal = Convert.ToInt32(0.33 * MaxHp);
            if (Hp + heal <= MaxHp)
            {
                Hp += heal;
                Thread.Sleep(800);
                WriteColor($"You healed by {heal}. Current health: {Hp}\n", ConsoleColor.Green);
            }
            else
            {
                Hp = MaxHp;
                Thread.Sleep(800);
                WriteColor($"You healed by {heal}. Current health: {Hp}\n", ConsoleColor.Green);
            }
        }
    }
    public void GainExp(int exp) // Zdobywanie expa
    {
        currentExp += exp;
        Thread.Sleep(800);
        WriteColor($"{exp} experience points earned. Current exp: {currentExp}/{expNeeded}\n", ConsoleColor.Blue);

        if (currentExp >= expNeeded)
        {
            LevelUp();
            currentExp -= expNeeded;
            expNeeded = Convert.ToInt32(expNeeded * 1.8);
        }
    }
    public void LevelUp() // Level up
    {
        Level++;
        Thread.Sleep(800);
        WriteColor($"\nLevel Up! You gain {Convert.ToInt32(Level * 2.5)} points to assign.\n", ConsoleColor.DarkMagenta);
        AvailableStatPoints += Convert.ToInt32(Level * 2.5);

        while (AvailableStatPoints > 0)
        {
            DisplayPlayerStats();
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
        DisplayPlayerStats();
    }
    static string GetStatInput()
    {
        Console.Write("Which stat do you want to assign points to (hp, str, spd)? ");
        return Console.ReadLine();
    }

    static bool IsValidStat(string stat)
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
        Random rand = new();

        switch (rand.Next(1, 4))
        {
            case 1: WeaponRarity = SetRarity(); DisplayItem("weapon"); Weapon(); break;
            case 2: ArmorRarity = SetRarity(); DisplayItem("armor"); Armor(); break;
            case 3: BootsRarity = SetRarity(); DisplayItem("boots"); Boots(); break;
        }
    }
    private void Weapon() // Tworzenie broni
    {
        Random rand = new();
        WeaponStr = 1 + rand.Next(Convert.ToInt32(maxItemStat), Convert.ToInt32(maxItemStat * 5));
        WeaponSpd = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), Convert.ToInt32(maxItemStat * 2));

        Console.Write($"New weapon stats:");
        DisplayItemStats(WeaponRarity, 0, WeaponStr, WeaponSpd);
        DisplayEquippedItem(pastWeaponRarity, 0, pastWeaponStr, pastWeaponSpd, "weapon");

        while (true)
        {
            Console.Write("Press Y to equip the new item or X to discard it ");
            ConsoleKeyInfo confirm = Console.ReadKey();
            if (confirm.Key == ConsoleKey.Y)
            {
                UpdateItemStats(0, WeaponStr, WeaponSpd, 0, pastWeaponStr, pastWeaponSpd);

                pastWeaponStr = WeaponStr;
                pastWeaponSpd = WeaponSpd;
                pastWeaponRarity = WeaponRarity;

                DisplayPlayerStats();
                break;
            }
            else if (confirm.Key == ConsoleKey.X)
            {
                WriteColor("\nItem discarded!", ConsoleColor.Red);
                break;
            }
            else
            {
                WriteColor("Invalid input!\n", ConsoleColor.Red);
                continue;
            }
        }
    }
    private void Armor() // Tworzenie zbroi
    {
        Random rand = new();
        ArmorHp = (1 + rand.Next(Convert.ToInt32(maxItemStat), Convert.ToInt32(maxItemStat * 5))) * 3;
        ArmorStr = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), Convert.ToInt32(maxItemStat * 2));

        Console.Write($"New armor stats:");
        DisplayItemStats(ArmorRarity, ArmorHp, ArmorStr, 0);
        DisplayEquippedItem(pastArmorRarity, pastArmorHp, pastArmorStr, 0, "armor");
        while (true)
        {
            Console.Write("Press Y to equip the new item or X to discard it ");
            ConsoleKeyInfo confirm = Console.ReadKey();
            if (confirm.Key == ConsoleKey.Y)
            {
                UpdateItemStats(ArmorHp, ArmorStr, 0, pastArmorHp, pastArmorStr, 0);

                pastArmorHp = ArmorHp;
                pastArmorStr = ArmorStr;
                pastArmorRarity = ArmorRarity;

                DisplayPlayerStats();
                break;
            }
            else if (confirm.Key == ConsoleKey.X)
            {
                WriteColor("\nItem discarded!", ConsoleColor.Red);
                break;
            }
            else
            {
                WriteColor("Invalid input!\n", ConsoleColor.Red);
                continue;
            }
        }
    }
    private void Boots() // Tworzenie butów
    {
        Random rand = new();
        BootsSpd = 1 + rand.Next(Convert.ToInt32(maxItemStat), Convert.ToInt32(maxItemStat * 5));
        BootsHp = (1 + rand.Next(Convert.ToInt32(maxItemStat / 2), Convert.ToInt32(maxItemStat * 2))) * 3;

        Console.Write($"New boots stats:");
        DisplayItemStats(BootsRarity, BootsHp, 0, BootsSpd);
        DisplayEquippedItem(pastBootsRarity, pastBootsHp, 0, pastBootsSpd, "boots");
        while (true)
        {
            Console.Write("Press Y to equip the new item or X to discard it ");
            ConsoleKeyInfo confirm = Console.ReadKey();
            if (confirm.Key == ConsoleKey.Y)
            {
                UpdateItemStats(BootsHp, 0, BootsSpd, pastBootsHp, 0, pastBootsSpd);

                pastBootsHp = BootsHp;
                pastBootsSpd = BootsSpd;
                pastBootsRarity = BootsRarity;

                DisplayPlayerStats();
                break;
            }
            else if (confirm.Key == ConsoleKey.X)
            {
                WriteColor("\nItem discarded!", ConsoleColor.Red);
                break;
            }
            else
            {
                WriteColor("Invalid input!\n", ConsoleColor.Red);
                continue;
            }
        }
    }

    public void UpdateItemStats(int itemMaxHpIncrease, int itemStrIncrease, int itemSpdIncrease, int itemMaxHpDecrease, int itemStrDecrease, int itemSpdDecrease)
    {
        MaxHp += itemMaxHpIncrease;
        Str += itemStrIncrease;
        Spd += itemSpdIncrease;

        MaxHp -= itemMaxHpDecrease;
        if (MaxHp > Hp)
        {
            Hp = MaxHp;
        }
        Str -= itemStrDecrease;
        Spd -= itemSpdDecrease;
    }
    static void DisplayItem(string Type) // Wyświetlanie otrzymanego przedmiotu
    {
        WriteColor($"\nYou got a new {Type}!\n", ConsoleColor.DarkYellow);
    }

    static void DisplayItemStats(string rarity, int itemMaxHp, int itemStr, int itemSpd)
    {
        Console.Write(" "); DisplayRarity(rarity); Console.WriteLine($" | MaxHp: {itemMaxHp} | Str: {itemStr} | Spd: {itemSpd}");
    }

    public string SetRarity()
    {
        Random rand = new Random();
        int rarityNum = rand.Next(1, 100);
        string Rarity = "";
        switch (rarityNum) // Losowanie rzadkości przedmiotu
        {
            case var _ when rarityNum >= 51:
                Rarity = "Common";
                maxItemStat = Convert.ToInt32(Level * 1.5);
                break;
            case var _ when rarityNum >= 26:
                Rarity = "Uncommon";
                maxItemStat = Convert.ToInt32(Level * 2);
                break;
            case var _ when rarityNum >= 14:
                Rarity = "Rare";
                maxItemStat = Convert.ToInt32(Level * 3);
                break;
            case var _ when rarityNum >= 7:
                Rarity = "Epic";
                maxItemStat = Convert.ToInt32(Level * 6);
                break;
            case var _ when rarityNum >= 4:
                Rarity = "Mythical";
                maxItemStat = Convert.ToInt32(Level * 10);
                break;
            case var _ when rarityNum >= 2:
                Rarity = "Legendary";
                maxItemStat = Convert.ToInt32(Level * 15);
                break;
            case var _ when rarityNum >= 1:
                Rarity = "Divine";
                maxItemStat = Convert.ToInt32(Level * 25);
                break;
        }
        return Rarity;
    }

    static void DisplayRarity(string rarity) // Rzadkości i ich kolory
    {
        switch (rarity)
        {
            case "Common":
                WriteColor($"{rarity}", ConsoleColor.DarkGray);
                break;
            case "Uncommon":
                WriteColor($"{rarity}", ConsoleColor.DarkGreen);
                break;
            case "Rare":
                WriteColor($"{rarity}", ConsoleColor.Blue);
                break;
            case "Epic":
                WriteColor($"{rarity}", ConsoleColor.Magenta);
                break;
            case "Mythical":
                WriteColor($"{rarity}", ConsoleColor.DarkRed);
                break;
            case "Legendary":
                WriteColor($"{rarity}", ConsoleColor.Yellow);
                break;
            case "Divine":
                WriteColor($"{rarity}", ConsoleColor.Cyan);
                break;
        }
    }

    public static void DisplayEquippedItem(string rarity, int itemMaxHp, int itemStr, int itemSpd, string itemType) // Wyświetlanie aktualnego przedmiotu
    {
        Console.Write($"\nEquipped {itemType} stats: ");
        if (rarity != "")
        {
            DisplayItemStats(rarity, itemMaxHp, itemStr, itemSpd);
        }
        else
        {
            WriteColor($"No {itemType} equipped\n", ConsoleColor.Red);
        }
    }
    public void ViewInventory() // Wyświetlanie ekwipunku
    {
        DisplayEquippedItem(pastWeaponRarity, 0, pastWeaponStr, pastWeaponSpd, "weapon");

        DisplayEquippedItem(pastArmorRarity, pastArmorHp, pastArmorStr, 0, "armor");

        DisplayEquippedItem(pastBootsRarity, pastBootsHp, 0, pastBootsSpd, "boots");
    }
}

class Enemy // Tworzenie przeciwnika
{
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }
    public int Str { get; private set; }
    public int Spd { get; private set; }
    public string Name { get; private set; }
    private Random rand;
    static void WriteColor(string message, ConsoleColor color = ConsoleColor.White) // Wypisywanie kolorem
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }

    public Enemy(int enemyStatRange, int enemyNum, string biome) // Losowanie przeciwnika i jego statystyk
    {
        if (!(enemyNum % 10 == 0))
        {
            string[] names = new string[5];
            switch (biome)
            {
                case "Desert":
                    names = ["Scorpion", "Sand Warrior", "Raging Cobra", "Sand Strider", "Dune Dweller"];
                    break;
                case "Castle":
                    names = ["Undead Knight", "Ironclad Sentinel", "Jester", "King's Advisor", "Cursed Baron"];
                    break;
                case "Swamp":
                    names = ["Witch", "Frogfolk", "Swamp Stalker", "Gnarled Root Goliath", "Bog Beast"];
                    break;
                case "Mountains":
                    names = ["Yeti", "Glacial Golem", "Frost Wraith", "Thunder Bird", "Deformed Ogre"];
                    break;
            }
            rand = new();
            switch (rand.Next(1, 5))
            {
                case 1:
                    Name = names[0];
                    break;
                case 2:
                    Name = names[1];
                    break;
                case 3:
                    Name = names[2];
                    break;
                case 4:
                    Name = names[3];
                    break;
            }
            Hp = (GenerateStat(enemyStatRange)) * 3;
            MaxHp = Hp;
            Str = GenerateStat(enemyStatRange);
            Spd = GenerateStat(enemyStatRange);

            WriteColor($"\n{Name} [lvl {enemyNum}] appears with {Hp} hp, {Str} str and {Spd} spd\n", ConsoleColor.Yellow);
        }
        else
        {
            switch (biome)
            {
                case "Desert":
                    Name = "Rock Golem";
                    break;
                case "Castle":
                    Name = "Fallen King";
                    break;
                case "Swamp":
                    Name = "Swamp Leviathan";
                    break;
                case "Mountains":
                    Name = "Frost Titan";
                    break;
            }
            rand = new();
            Hp = (GenerateStat(enemyStatRange)) * 6;
            MaxHp = Hp;
            Str = GenerateStat(enemyStatRange);
            Spd = GenerateStat(enemyStatRange);

            WriteColor($"\n{Name} [lvl {enemyNum}] appears with {Hp} hp, {Str} str and {Spd} spd\n", ConsoleColor.Red);
        }
    }

    private int GenerateStat(int range) // Zakres statystyk
    {
        return rand.Next(range, Convert.ToInt32(range * 1.5));
    }

    public void TakeDamage(string playerUsername, int damage, float playerCritChance, float playerCritMutliplier) // Otrzymywanie obrażeń
    {
        float damageTaken = rand.Next(damage, Convert.ToInt32(1.1 * damage));
        if (rand.Next(1, 100) <= playerCritChance)
        {
            Convert.ToInt32(damageTaken *= playerCritMutliplier);
            WriteColor($"\n{playerUsername} attacks the enemy with a critical hit!", ConsoleColor.Yellow);
        }
        else
        {
            Console.WriteLine($"\n{playerUsername} attacks the {Name}!");
        }
        Hp -= Convert.ToInt32(damageTaken);
        Thread.Sleep(800);
        Console.Write($"The {Name} takes ");
        WriteColor($"{Convert.ToInt32(damageTaken)}", ConsoleColor.Red);
        if (Hp < 0) Hp = 0;
        Console.Write($" damage, {Name}'s health: ");
        WriteColor($"{Hp}/{MaxHp}\n", ConsoleColor.Green);
    }
}
