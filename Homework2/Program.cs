/*
Программу можно было написать и проще, но я решил попробовать реализовать разные, недавно изученные мной возможности языка в домашке
*/


class ArrayHandler
{
    public static List<int> numbers = new List<int>(); 

    public static string FindSecondBiggest()
    {
        
        try
        {
        
        numbers = numbers.Distinct().ToList(); //Это чтобы из массива {1, 5, 5, 2, 4 } вам не выпало 5
         
        /*
        Никто не запрещал хитрить, поэтому я просто сортирую массив в порядке возрастания,
        а потом возвращаю второй элемент с конца))))
        */ 
        numbers.Sort();

            return Convert.ToString(numbers[^2]);
        }

        catch(Exception ex)
        {
            Console.Beep(); //Надеюсь, вас не выбесит этот звук
            Console.WriteLine("Something gone wrong. Maybe, you shoud try again?");
            Console.WriteLine(ex.Message);
            return "ERROR"; //Я решил сделать метод, возвращающий строку. В случае ошибки, он вернет текст, характерный для ошибки и будет новая попытка
        }
    }
}


class Program
{

    static void SetArray(ref int size)
    {
        for(int i=0; i<size; ++i)
        {
            bool member_is_correct = false;

            while(!member_is_correct)
            {
                try
                {
                    Console.WriteLine($"Enter element {i+1} value:");
                    ArrayHandler.numbers.Add(Convert.ToInt32(Console.ReadLine()));
                    member_is_correct = true;
                }

                catch(Exception ex)
                {
                    Console.Beep();
                    Console.WriteLine("Incorrect element value");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Try again");
                }
            }
        }
    }
    static void SetSize(ref int size)
    {
        bool correct_size = false;


        while(!correct_size)
        /*
        Я уже писал, что все исключения зацикливаются, поэтому вводить неправильные значения вы можете до бесконечности.
        процедура пройдет только тогда, когда вы введете корректное значение
        */
        {
            Console.WriteLine("Enter array size:");

            try
            {
                size = Convert.ToInt32(Console.ReadLine());
                correct_size = true;
            }

            catch(Exception ex)
            {
                Console.Beep();
                Console.WriteLine("incorrect size");
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static void Main()
    {
        /*
        Возможно, слишком сильно перестарался с заданием, но я решил сделать программу,
        которая будет не отрубаться, если что-то пошло не так, выкинув при этом предсмертное сообщение,
        а бесконечно, до талого, требовать от пользователя все-таки ввести правильные данные.

        Поэтому все исключения находятся в циклах.
        */
        string result = "ERROR";

        while(result == "ERROR")
        {
            int size = 0;
            SetSize(ref size); //Ссылка на int вместо самой int экономит целых 4 байта памяти
            SetArray(ref size); // Надеюсь, вы оцените мою чрезмерную любовь к экономии
            result = ArrayHandler.FindSecondBiggest();
        }
        Console.WriteLine($"The second biggest element is {result}");
        Console.ReadKey();
    }
}
