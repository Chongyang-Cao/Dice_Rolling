using System.IO.Pipes;
using System.Reflection.Metadata.Ecma335;

internal class Program
{
    private static async Task Main(string[] args)
    {
        int dice = 2;
        int sides = 6;
        int rolls = 1;
        int ans = 0;
        Message(dice, sides, rolls);
        while  (ans != 5)
        {
            try
            {
                ans = Convert.ToInt16(Console.ReadLine());
                if (ans > 5 || ans < 1)
                {
                    Console.WriteLine("Choose an option between 1 and 5");
                }
                else
                {
                    switch (ans)
                    {
                        case 1:
                            dice = DiceMod();
                            Message(dice, sides, rolls);
                            break;
                        case 2:
                            sides = SideMod();
                            Message(dice, sides, rolls);
                            break;
                        case 3:
                            rolls = RollMod();
                            Message(dice, sides, rolls);
                            break;
                        case 4:
                            await RollDice(dice, sides, rolls);
                            Message(dice, sides, rolls);
                            break;
                    }
                }
            }
            catch 
            {
                Console.WriteLine("Choose an option between 1 and 5");
            }
        }
    }
    public static int DiceMod()
    {
        int ans1 = -1;
        while (ans1 <= 0)
        {
            
            Console.WriteLine("Number of Dice thrown? ");
            try
            {
                ans1 = Convert.ToInt32(Console.ReadLine());
                if (ans1 <= 0)
                {
                    Console.WriteLine("Can't roll 0 or less dice");
                }
            }
            catch
            {
                Console.WriteLine("Error with answer, please try another!");
            }
        }

        return ans1;
    }
    public static int SideMod()
    {
        int ans1 = -1;
        while (ans1 <= 0)
        {
            Console.WriteLine("Number of Sides on Dice? ");
            try
            {
                ans1 = (Convert.ToInt32(Console.ReadLine()));
                if (ans1 <= 0)
                {
                    Console.WriteLine("Can't roll 0 or less dice");
                }
            }
            catch
            {
                Console.WriteLine("Error with answer, please try another!");
            }
        }

        return ans1;
    }
    public static int RollMod()
    {
        int ans1 = -1;
        while (ans1 <= 0)
        {
            Console.WriteLine("Number of times rolled? (Roll all dice this many times) ");
            try
            {
                ans1 = (Convert.ToInt32(Console.ReadLine()));
                if (ans1 <= 0)
                {
                    Console.WriteLine("Can't roll 0 or less dice");
                }
            }
            catch
            {
                Console.WriteLine("Error with answer, please try another!");
            }
        }
        return ans1;
    }
    public static async Task RollDice(int dice, int sides, int rolls)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://dice-roll-simulator.p.rapidapi.com/custom-dice-rolls?dice=" + dice + "&sides=" + sides + "&rolls=" + rolls),
            Headers = { { "X-RapidAPI-Key", "f6980af27emshf5ec8352de33e12p1715a2jsn69b56f628de2" }, { "X-RapidAPI-Host", "dice-roll-simulator.p.rapidapi.com" }, },
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        }
    }
    public static void Message(int dice, int sides, int rolls)
    {

        Console.WriteLine("\nDice:" + dice + "\nSides:" + sides + "\nRolls:" + rolls);
        Console.WriteLine("\nChange:\n  1.Number of dice rolled\n  2.Number of sides on dice\n  3.Number of times rolled\n4.Roll dice\n5.Exit");
    }
}