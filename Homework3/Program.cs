



///////////////////////////////////Как пользоваться данной программой?//////////////////////
/*
                            Просто запускаете, и жмете клавишу энтер после каждого цикла.
                                Если надоело, то жмете что угодно, кроме энтера
*/
class Program
{
    public static void Main()
    {
        while (true)
        {
            var rndGen = new Random();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Начинается новый день...");
            Doctor doctor1;
            /*Я решил не делать список врачей и проходить по нему, потому что пришлось бы долго придумывать имена и фамилии
            поэтому я просто рандомно генерирую врача и пациента с разными болезнями, а потом врач лечит пациента (если может)*/
            switch (rndGen.Next(6))
            {
                //Здесь я делаю даункаст до конкретного врача
                case 0:
                    doctor1 = new GastroEnterologist("Михаил Афанасьевич", "Булгаков", rndGen.Next(0, 35));
                    break;
                case 1:
                    doctor1 = new Urologist("Михаил Афанасьевич", "Булгаков", rndGen.Next(0, 35));
                    break;
                case 2:
                    doctor1 = new Psychiater("Михаил Афанасьевич", "Булгаков", rndGen.Next(0, 35));
                    break;
                case 4:
                    doctor1 = new Cardiologist("Михаил Афанасьевич", "Булгаков", rndGen.Next(0, 35));
                    break;
                default:
                    doctor1 = new Hepatologist("Михаил Афанасьевич", "Булгаков", rndGen.Next(0, 35));
                    break;
            }


            var patient1 = new Patient("Кот", "Бегемот", 100);
            doctor1.Greet(patient1); //Обязательно привесттсвуем пациента
            foreach (var patientorgan in patient1.organs)
            {
                doctor1.Cure(patientorgan);
                //Ищем в его органах патологии. Вообще, можно было бы это и в метод врача запихнуть,
                //Но я прописал здесь
            }
            Console.WriteLine($"На этом {patient1.name} {patient1.surname} закончил свое посещение больницы");
            Console.WriteLine("Ну все, рабочий день закончен, до завтра!");
            if (Console.ReadKey().Key != ConsoleKey.Enter) break;
        }


    }
}
