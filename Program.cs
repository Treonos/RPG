using System;
using System.Net.Security;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    {
        Output("WELCOME TO PABLO MUSHROOMS\n", ConsoleColor.Magenta);

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
                    Thread.Sleep(2000);
                    Console.WriteLine("You defeated the enemy!\n");
                    player.Heal();
                    player.GainExp(Convert.ToInt32((hpRange + strRange + spdRange) / 3));
                    if(enemyNum++ % 3 == 0) player.Equip();
                    Console.Write("\nPress Enter to continue");
                    Console.ReadLine();
                    hpRange += 1 + Convert.ToInt32(hpRange * 0.10);
                    strRange += 1 + Convert.ToInt32(strRange * 0.10);
                    spdRange += 1 + Convert.ToInt32(spdRange * 0.10);
                }
                else if (player.Hp <= 0)
                {
                    Output("\nGAME OVER\n", ConsoleColor.Red);
                    enemyNum = 0;
                }
            }
        }
    }

    static void Output(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }
}

class Player
{
    public int itemHp { get; private set; }
    public int itemStr { get; private set; }
    public int itemSpd { get; private set; }
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

    public int pastItemHp = 0, pastItemStr = 0, pastItemSpd = 0;

    private void Output(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }
    public Player()
    {
        maxHp = 1;
        Hp = maxHp;
        Str = 1;
        Spd = 1;
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
                        Hp = 15;
                        Str = 8;
                        Spd = 3;
                        break;
                    case 2:
                        optName = "Mage";
                        Hp = 8;
                        Str = 13;
                        Spd = 5;
                        break;
                    case 3:
                        optName = "Archer";
                        Hp = 6;
                        Str = 9;
                        Spd = 11;
                        break;
                    default:
                        Console.WriteLine("Choose a correct number!");
                        continue;
                }

                Console.Write($"So, you choose {optName}? y/n: ");
                confirm = Console.ReadLine();

                Console.WriteLine($"\nCurrent stats:\nHealth: {Hp}  |  Strength: {Str}  |  Speed: {Spd}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Please, choose a number from 1 to 3.");
            }

        } while (!(confirm.Equals("Y", StringComparison.OrdinalIgnoreCase)));
        confirm = "";

        optNum = 0;

        Console.WriteLine("\nChoose your path: ");
        Console.WriteLine("1. Desert");
        Console.WriteLine("2. Forest");
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
                        optName = "Forest";
                        break;
                    case 3:
                        optName = "Swamp";
                        break;
                    case 4:
                        optName = "Mountains";
                        break;
                    default:
                        Console.WriteLine("Choose a correct number!");
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
            Console.WriteLine("Not enough points!");
            return;
        }

        if (points < 0)
        {
            Console.WriteLine("You cannot assign negative points!");
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
                Console.WriteLine("Invalid stat!");
                return;
        }

        AvailableStatPoints -= points;
        Console.WriteLine($"{points} Points assigned to {stat}.");
    }

    public void Display()
    {
        Console.WriteLine($"\nCurrent stats:\nHealth: {Hp}  |  Strength: {Str}  |  Speed: {Spd}");
        Console.WriteLine($"\nPoints left: {AvailableStatPoints}\n");
    }

    public void TakeDamage(int damage)
    {
        Console.WriteLine($"\nThe enemy attacks {username}!");
        Hp -= damage;
        Thread.Sleep(2000);
        Console.Write("You take ");
        Output(Convert.ToString(damage), ConsoleColor.Red);
        Console.Write(" damage, remaining health: ");
        if (Hp < 0) Hp = 0;
        Output(Convert.ToString(Hp), ConsoleColor.Green);
    }

    public void Heal()
    {
        if (Hp < maxHp)
        {
            int heal = (int)(0.9 * Hp);
            Hp = Math.Min(maxHp, Hp + heal);
            Thread.Sleep(2000);
            Output($"You healed by {heal}. Current health: {Hp}\n", ConsoleColor.Green);
        }
    }

    public void GainExp(int exp)
    {
        currentExp += exp;
        Thread.Sleep(2000);
        Output($"{exp} experience points earned. Current exp: {currentExp}/{expNeeded}\n", ConsoleColor.Blue);

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
        Console.WriteLine($"Level Up! You gain {Convert.ToInt32(level * 1.6)} points to assign.");
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
                Console.WriteLine("Invalid stat! Please choose from (hp, str, spd).");
            }
        }
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
    }

    public void Equip()
    {
        string Type = "";
        string Rarity = "";
    
        Random rand = new Random();
        int typeNum = rand.Next(1, 3);

        switch (typeNum)
        {
            case 1: Type = "Weapon"; break;
            case 2: Type = "Armor"; break;
            case 3: Type = "Boots"; break;
        }

        int rarityNum = rand.Next(1, 100);

        Output("\nYou got ", ConsoleColor.DarkYellow);
        switch (rarityNum)
        {
            case var _ when rarityNum >= 50:
                Rarity = "Common";
                Output($"{Rarity}", ConsoleColor.DarkGray);
                break;
            case var _ when rarityNum >= 25 && rarityNum < 50:
                Rarity = "Uncommon";
                Output($"{Rarity}", ConsoleColor.DarkGreen);
                break;
            case var _ when rarityNum >= 9 && rarityNum < 25:
                Rarity = "Rare";
                Output($"{Rarity}", ConsoleColor.DarkBlue);
                break;
            case var _ when rarityNum >= 2 && rarityNum < 9:
                Rarity = "Epic";
                Output($"{Rarity}", ConsoleColor.Magenta);
                break;
            case var _ when rarityNum == 1:
                Rarity = "Mythical";
                Output($"{Rarity}", ConsoleColor.DarkRed);
                break;
        }

        Output($" {Type}\n", ConsoleColor.DarkYellow);

        maxItemStat = Convert.ToInt32(level * 4);

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

        switch (Type)
        {
            case "Weapon":
                itemHp = 0;
                itemStr = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), maxItemStat);
                itemSpd = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), maxItemStat);
                break;
            case "Armor":
                itemHp = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), maxItemStat);
                itemStr = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), maxItemStat);
                itemSpd = 0;
                break;
            case "Boots":
                itemHp = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), maxItemStat);
                itemStr = 0;
                itemSpd = 1 + rand.Next(Convert.ToInt32(maxItemStat / 2), maxItemStat);
                break;
        }

        Console.WriteLine($"New item stats:\nHp:    {itemHp}    |   Str:    {itemStr}   |   Spd:    {itemSpd}\n");
        Console.WriteLine($"Current item stats:\nHp:    {pastItemHp}    |   Str:    {pastItemStr}   |   Spd:    {pastItemSpd}\n");
        Console.Write("Would you like to equip the item?(Your current item will be lost!) y/n ");
        string confirm = Console.ReadLine();
        if (confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
        {
            Hp += itemHp;
            maxHp += itemHp;
            Str += itemStr;
            Spd += itemSpd;

            Hp -= pastItemHp;
            maxHp -= pastItemHp;
            Str -= pastItemStr;
            Spd -= pastItemSpd;

            pastItemHp = itemHp;
            pastItemStr = itemStr;
            pastItemSpd = itemSpd;

            Console.WriteLine($"\nCurrent stats:\nHealth: {Hp}  |  Strength: {Str}  |  Speed: {Spd}");
        }
    }
}

class Enemy
{
    public int Hp { get; private set; }
    public int Str { get; private set; }
    public int Spd { get; private set; }

    private void Output(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }

    public Enemy(int playerLevel, int hpRange, int strRange, int spdRange)
    {
        Random rand = new Random();
        Hp = rand.Next(playerLevel, hpRange);
        Str = rand.Next(playerLevel, strRange);
        Spd = rand.Next(playerLevel, spdRange);

        Output($"\nAn enemy appears with {Hp} hp, {Str} str and {Spd} spd", ConsoleColor.Yellow);
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        Thread.Sleep(2000);
        Console.Write("The enemy takes ");
        Output($"{damage}", ConsoleColor.Red);
        if (Hp < 0) Hp = 0;
        Console.Write($" damage, remaining health: ");
        Output($"{Hp}\n", ConsoleColor.Green);
    }
}