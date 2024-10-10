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

            while (player.Hp > 0)
            {
                Enemy enemy = new Enemy(player.level, hpRange, strRange, spdRange);

                while (enemy.Hp > 0 && player.Hp > 0)
                {
                    if (player.Spd >= enemy.Spd)
                    {
                        Thread.Sleep(2000);
                        Console.WriteLine($"\n{player.username} attacks the enemy!");
                        enemy.TakeDamage(player.Str);
                        if (enemy.Hp > 0)
                        {
                            player.TakeDamage(enemy.Str);
                        }
                    }
                    else
                    {
                        player.TakeDamage(enemy.Str);
                        if (player.Hp > 0)
                        {
                            Thread.Sleep(2000);
                            Console.WriteLine($"\n{player.username} attacks the enemy!");
                            enemy.TakeDamage(player.Str);
                        }
                    }
                    Console.WriteLine();
                }

                if (enemy.Hp <= 0)
                {
                    string input = " ";
                    Thread.Sleep(2000);
                    Console.WriteLine("You defeated the enemy!\n");
                    player.Heal();
                    player.GainExp(Convert.ToInt32((hpRange + strRange + spdRange) / 3));
                    if(enemyNum++ % 3 == 0) player.Equip();
                    Console.Write("\nPress Enter to continue or press I to view your inventory ");
                    input = Console.ReadLine();
                    if(input.Equals("I", StringComparison.OrdinalIgnoreCase))
                    {
                        player.ViewInventory();
                    }
                    
                    
                    hpRange += 1 + Convert.ToInt32(hpRange * 0.10);
                    strRange += 1 + Convert.ToInt32(strRange * 0.10);
                    spdRange += 1 + Convert.ToInt32(spdRange * 0.10);
                }
                else if (player.Hp <= 0)
                {
                    WriteColor("\nGAME OVER\n", ConsoleColor.Red);
                    enemyNum = 0;
                }
            }
        }
    }

    static void WriteColor(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }
}

class Player
{
    public int WeaponStr { get; private set; }
    public int WeaponSpd { get; private set; }
    public int ArmorHp { get; private set; }
    public int ArmorStr { get; private set; }
    public int BootsHp { get; private set; }
    public int BootsSpd { get; private set; }
    public int Hp  { get; private set; }
    public int Str { get; private set; }
    public int Spd { get; private set; }

    public string username;
    public int AvailableStatPoints { get; private set; }
    private int expNeeded = 10;
    private int currentExp = 0;
    private int maxHp;
    public int level = 1;
    public int maxItemStat = 1;

    public int pastWeaponStr = 0, pastWeaponSpd = 0;
    public int pastArmorStr = 0, pastArmorHp = 0;
    public int pastBootsHp = 0, pastBootsSpd = 0;

    private void WriteColor(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }
    public Player()
    {
        AvailableStatPoints = 0;

        string confirm = "";

        do
        {
            Console.Write("\nEnter your name: ");
            username = Console.ReadLine();
            Console.Write("Are you sure? y/n: ");
            confirm = Console.ReadLine();
        } while (!(confirm.Equals("Y", StringComparison.OrdinalIgnoreCase)));
        confirm = "";

        string optName;
        int optNum = 0;

        Console.WriteLine($"\nChoose your class, {username}: ");
        Console.WriteLine("1. Knight");
        Console.WriteLine("2. Mage");
        Console.WriteLine("3. Archer");

        do
        {
            Console.Write("\nI choose option number: ");
            try
            {
                optNum = Convert.ToInt32(Console.ReadLine());
                switch (optNum)
                {
                    case 1:
                        optName = "Knight";
                        maxHp = 15;
                        Str = 8;
                        Spd = 3;
                        break;
                    case 2:
                        optName = "Mage";
                        maxHp = 8;
                        Str = 13;
                        Spd = 6;
                        break;
                    case 3:
                        optName = "Archer";
                        maxHp = 6;
                        Str = 9;
                        Spd = 11;
                        break;
                    default:
                        WriteColor("Incorrect number!", ConsoleColor.Red);
                        continue;
                }
                Hp = maxHp;
                Console.Write($"So, you choose {optName}? y/n: ");
                confirm = Console.ReadLine();

                Display();
            }
            catch(FormatException)
            {
                Console.WriteLine("Please, choose a number from 1 to 3.");
            }
            catch (OverflowException)
            {
                WriteColor("Incorrect number!", ConsoleColor.Red);
            }

        } while (!(confirm.Equals("Y", StringComparison.OrdinalIgnoreCase)));
        confirm = "";

        optNum = 0;

        Console.WriteLine("\nChoose your path: ");
        Console.WriteLine("1. Desert");
        Console.WriteLine("2. Castle");
        Console.WriteLine("3. Swamp");
        Console.WriteLine("4. Mountains");
        do
        {
            Console.Write("\nI choose option number: ");
            try
            {
                optNum = Convert.ToInt32(Console.ReadLine());
                switch (optNum)
                {
                    case 1:
                        optName = "Desert";
                        break;
                    case 2:
                        optName = "Castle";
                        break;
                    case 3:
                        optName = "Swamp";
                        break;
                    case 4:
                        optName = "Mountains";
                        break;
                    default:
                        WriteColor("Incorrect number!", ConsoleColor.Red);
                        continue;
                }

                Console.Write($"So, you choose {optName}? y/n: ");
                confirm = Console.ReadLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Please, choose a number.");
            }

        } while (!(confirm.Equals("Y", StringComparison.OrdinalIgnoreCase)));
        confirm = "";
    }
    public void AssignStatPoints(string stat, int points)
    {
        if (points > AvailableStatPoints)
        {
            WriteColor("Not enough points!", ConsoleColor.Red);
            return;
        }

        if (points < 0)
        {
            WriteColor("You cannot assign negative points!", ConsoleColor.Red);
            return;
        }

        switch (stat.ToLower())
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
                WriteColor("Invalid stat!", ConsoleColor.Red);
                return;
        }

        AvailableStatPoints -= points;
        Console.WriteLine($"{points} Points assigned to {stat}.");
        WriteColor($"\nPoints left: {AvailableStatPoints}", ConsoleColor.DarkMagenta);
    }
    public void Display()
    {
        Console.WriteLine($"\nCurrent stats:\nHealth: {Hp}/{maxHp}  |  Strength: {Str}  |  Speed: {Spd}");
    }
    public void TakeDamage(int damage)
    {
        Console.WriteLine($"\nThe enemy attacks {username}!");
        Hp -= damage;
        Thread.Sleep(2000);
        Console.Write("You take ");
        WriteColor(Convert.ToString(damage), ConsoleColor.Red);
        Console.Write(" damage, remaining health: ");
        if (Hp < 0) Hp = 0;
        WriteColor(Convert.ToString(Hp), ConsoleColor.Green);
    }
    public void Heal()
    {
        int heal = Convert.ToInt32(0.75 * maxHp);
        if(Hp < maxHp)
        {
            if(Hp + heal <= maxHp)
            {
                Hp += heal;
                Thread.Sleep(2000);
                WriteColor($"You healed by {heal}. Current health: {Hp}\n", ConsoleColor.Green);
            }
            else
            {
                Hp = maxHp;
                Thread.Sleep(2000);
                WriteColor($"You healed by {heal}. Current health: {Hp}\n", ConsoleColor.Green);
            }
        }
    }
    public void GainExp(int exp)
    {
        currentExp += exp;
        Thread.Sleep(2000);
        WriteColor($"{exp} experience points earned. Current exp: {currentExp}/{expNeeded}\n", ConsoleColor.Blue);

        if (currentExp >= expNeeded)
        {
            LevelUp();
            currentExp -= expNeeded;
            expNeeded = Convert.ToInt32(expNeeded * 1.6);
        }
    }
    public void LevelUp()
    {
        level++;
        Thread.Sleep(2000);
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
    private void AssignPointsToStat(string stat)
    {
        Console.Write("How many points do you want to assign? ");
        try
        {
            int points = Convert.ToInt32(Console.ReadLine());

            AssignStatPoints(stat, points);
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid number.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Please enter a valid number.");
        }
    }
    public void Equip()
    {
        Random rand = new Random();
        int typeNum = rand.Next(1, 3);

        switch (typeNum)
        {
            case 1: Weapon(); break;
            case 2: Armor(); break;
            case 3: Boots(); break;
        }
    }
    private void Weapon()
    {
        Random rand = new Random();
        displayItem("Weapon");
        WeaponStr = 1 + rand.Next(Convert.ToInt32(maxItemStat / 3), maxItemStat);
        WeaponSpd = 1 + rand.Next(Convert.ToInt32(maxItemStat / 3), maxItemStat);

        Console.WriteLine($"New weapon stats:\nMaxHp:    0    |   Str:    {WeaponStr}   |   Spd:    {WeaponSpd}\n");
        Console.Write($"Equipped weapon stats:\n");
        if(pastWeaponStr > 0 || pastWeaponSpd > 0)
        {
            Console.Write($"MaxHp:    0    |   Str:    {pastWeaponStr}   |   Spd:    {pastWeaponSpd}\n");
        }
        else
        {
            Console.Write("No weapon equipped\n");
        }
            
        Console.Write("\nWould you like to equip the item?(Your current item will be lost!) y/n ");
        string confirm = Console.ReadLine();
        if (confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
        {
            Str += WeaponStr;
            Spd += WeaponSpd;

            Str -= pastWeaponStr;
            Spd -= pastWeaponSpd;

            pastWeaponStr = WeaponStr;
            pastWeaponSpd = WeaponSpd;

            Display();
        }
    }
    private void Armor()
    {
        Random rand = new Random();
        displayItem("Armor");
        ArmorHp = 1 + rand.Next(Convert.ToInt32(maxItemStat / 3), maxItemStat);
        ArmorStr = 1 + rand.Next(Convert.ToInt32(maxItemStat / 3), maxItemStat);

        Console.WriteLine($"New armor stats:\nMaxHp:    {ArmorHp}    |   Str:    {ArmorStr}   |   Spd:    0\n");
        Console.WriteLine($"Equipped armor stats:\n");
        if (pastArmorHp > 0 || pastArmorStr > 0)
        {
            Console.Write($"MaxHp:    {pastArmorHp}    |   Str:    {pastArmorStr}   |   Spd:    0\n");
        }
        else
        {
            Console.Write("No armor equipped\n");
        }
        Console.Write("\nWould you like to equip the item?(Your current item will be lost!) y/n ");
        string confirm = Console.ReadLine();
        if (confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
        {
            maxHp += ArmorHp;
            Str += ArmorStr;

            maxHp -= pastArmorHp;
            Str -= pastArmorStr;

            pastArmorHp = ArmorHp;
            pastArmorStr = ArmorStr;

            Display();
        }
    }
    private void Boots()
    {
        Random rand = new Random();
        displayItem("Boots");
        BootsHp = 1 + rand.Next(Convert.ToInt32(maxItemStat / 3), maxItemStat);
        BootsSpd = 1 + rand.Next(Convert.ToInt32(maxItemStat / 3), maxItemStat);

        Console.WriteLine($"New boots stats:\nMaxHp:    {BootsHp}    |   Str:    0   |   Spd:    {BootsSpd}\n");
        Console.WriteLine($"Equipped boots stats:\n");
        if (pastBootsHp > 0 || pastBootsSpd > 0)
        {
            Console.Write($"MaxHp:    {pastBootsHp}    |   Str:    0   |   Spd:    {pastBootsSpd}\n");
        }
        else
        {
            Console.Write("No boots equipped\n");
        }
        Console.Write("\nWould you like to equip the item?(Your current item will be lost!) y/n ");
        string confirm = Console.ReadLine();
        if (confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
        {
            maxHp += BootsHp;
            Spd += BootsSpd;

            maxHp -= pastBootsHp;
            Spd -= pastBootsSpd;

            pastBootsHp = BootsHp;
            pastBootsSpd = BootsSpd;

            Display();
        }
    }
    public void displayItem(string Type)
    {
        Random rand = new Random();
        WriteColor("\nYou got ", ConsoleColor.DarkYellow);
        int rarityNum = rand.Next(1, 100);
        string Rarity = " ";
        switch (rarityNum)
        {
            case var _ when rarityNum >= 50:
                Rarity = "Common";
                WriteColor($"{Rarity}", ConsoleColor.DarkGray);
                break;
            case var _ when rarityNum >= 25 && rarityNum < 50:
                Rarity = "Uncommon";
                WriteColor($"{Rarity}", ConsoleColor.DarkGreen);
                break;
            case var _ when rarityNum >= 9 && rarityNum < 25:
                Rarity = "Rare";
                WriteColor($"{Rarity}", ConsoleColor.DarkBlue);
                break;
            case var _ when rarityNum >= 2 && rarityNum < 9:
                Rarity = "Epic";
                WriteColor($"{Rarity}", ConsoleColor.Magenta);
                break;
            case var _ when rarityNum == 1:
                Rarity = "Mythical";
                WriteColor($"{Rarity}", ConsoleColor.DarkRed);
                break;
        }
        WriteColor($" {Type}\n", ConsoleColor.DarkYellow);

        switch (Rarity)
        {
            case "Common":
                maxItemStat = Convert.ToInt32(level * 1.1);
                break;
            case "Uncommon":
                maxItemStat = Convert.ToInt32(level * 1.5);
                break;
            case "Rare":
                maxItemStat = Convert.ToInt32(level * 2);
                break;
            case "Epic":
                maxItemStat = Convert.ToInt32(level * 2.5);
                break;
            case "Mythical":
                maxItemStat = Convert.ToInt32(level * 3);
                break;
        }
    }

    public void ViewInventory()
    {
        Console.Write($"Equipped weapon stats: ");
        if (pastWeaponStr > 0 || pastWeaponSpd > 0)
        {
            Console.Write($"MaxHp:    0    |   Str:    {pastWeaponStr}   |   Spd:    {pastWeaponSpd}\n");
        }
        else
        {
            Console.Write("No weapon equipped\n");
        }

        Console.Write($"Equipped armor stats: ");
        if (pastArmorHp > 0 || pastArmorStr > 0)
        {
            Console.Write($"MaxHp:    {pastArmorHp}    |   Str:    {pastArmorStr}   |   Spd:    0\n");
        }
        else
        {
            Console.Write("No armor equipped\n");
        }

        Console.Write($"Equipped boots stats: ");
        if (pastBootsHp > 0 || pastBootsSpd > 0)
        {
            Console.Write($"MaxHp:    {pastBootsHp}    |   Str:    0   |   Spd:    {pastBootsSpd}\n");
        }
        else
        {
            Console.Write("No boots equipped\n");
        }
    }
}

class Enemy
{
    public int Hp { get; private set; }
    public int maxHp { get; private set; }
    public int Str { get; private set; }
    public int Spd { get; private set; }

    private void WriteColor(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }

    public Enemy(int playerLevel, int hpRange, int strRange, int spdRange)
    {
        Random rand = new Random();
        Hp = rand.Next(playerLevel, hpRange);
        maxHp = Hp;
        Str = rand.Next(playerLevel, strRange);
        Spd = rand.Next(playerLevel, spdRange);

        WriteColor($"\nAn enemy appears with {Hp} hp, {Str} str and {Spd} spd\n", ConsoleColor.Yellow);
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        Thread.Sleep(2000);
        Console.Write("The enemy takes ");
        WriteColor($"{damage}", ConsoleColor.Red);
        if (Hp < 0) Hp = 0;
        Console.Write($" damage, enemy's health: ");
        WriteColor($"{Hp}/{maxHp}\n", ConsoleColor.Green);
    }
}
