

/*
Креатив!
*/

public class Psychiater : Doctor
{
    public Psychiater(string name, string surname, int cabinetNumber) : base(name, surname, cabinetNumber)
    {
        speciality = "психиатр";
        _organsToCure.Add(new Mentality());
    }

    public override void Greet(Patient patient)
    {
        Console.WriteLine($"Здравствуйте {patient.name} {patient.surname}! Меня зовут {_name} {_surname}, я {speciality}.");
        Console.WriteLine($"И конечно я верю, что у вас по сосудам плавают рыбки.");
    }

    public override void Cure(Organ patientOrgan)
    //Каждый врач лечит по-своему...
    {
        foreach (var currentOrgan in _organsToCure)
        {
            if (currentOrgan.name == patientOrgan.name) //Если врач может лечить этот орган....
            {

                if (patientOrgan.state == ORGAN_STATES.UNHEALTHY) //И орган нездоров...
                {
                    patientOrgan.state = ORGAN_STATES.HEALTHY;
                    Console.WriteLine($"Вижу проблему... Примите, пожалуйста вот эту таблеточку... Ну все, {patientOrgan.name} не будет беспокоить.");
                    Console.WriteLine($"За свои услуги я беру очень скромно. Всего {CalculatePrice()} рублей!");
                    return;
                }

                else //И орган здоров...
                {
                    Console.WriteLine($"Друг мой, вы лучше так не шутите. {patientOrgan.name} у вас в порядке, не надо драматизировать");
                    return;
                }
            }
        }
        //Если врач орган лечить не может
        Console.WriteLine($"{patientOrgan.name}? Простите, но я только душевными делами занимаюсь");
    }

}