// See https://aka.ms/new-console-template for more information

public class Program
{

    private static void setName(string name = "")
    {
        _name_ = name;
    }

    private static string getName()
    {
        return _name_;
    }

    public static void Main()
    {
        Console.WriteLine("Как тебя зовут?");
        setName(Console.ReadLine());
        Console.WriteLine($"{getName()}, привет!");
    }


    private static string _name_ = "";
}

