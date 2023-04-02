public abstract class Doctor //Создал абстрактный класс, от которого наследуются все врачи
{

    /*
    Обратите внимание, что здесь есть виртуальные методы, которые можно переопределить.
    Я такое сделал, например, с психиатром
    */
    protected string speciality = "врач";
    protected string _name = "Не задано";
    protected string _surname = "Не задано";

    /*
    Обратите внимание, что врач может лечить несколько органов. Например гастроэнтеролог - кишечник и желудок.
    */
    protected List<Organ> _organsToCure = new List<Organ>();

    int _cabinetNumber = -1; //вот это САМОЕ ВАЖНОЕ, что нужно знать о докторе, когда идешь в больницу

    public Doctor(string name, string surname, int cabinetNumber)
    {
        _name = name;
        _surname = surname;
        _cabinetNumber = cabinetNumber;
        Console.WriteLine($"Доктор {_name} {_surname} заходит в свой кабинет №{_cabinetNumber}");
    }

    public virtual void Greet(Patient patient)
    {
        Console.WriteLine($"Здравствуйте {patient.name} {patient.surname}! Меня зовут {_name} {_surname}, я {speciality}. Буду вас лечить...");
    }

    public virtual void Cure(Organ patientOrgan)
    //Каждый врач лечит по-своему...
    {
        foreach (var currentOrgan in _organsToCure)
        {
            if (currentOrgan.name == patientOrgan.name) //Если врач может лечить этот орган....
            {

                if (patientOrgan.state == ORGAN_STATES.UNHEALTHY) //И орган нездоров...
                {
                    patientOrgan.state = ORGAN_STATES.HEALTHY;
                    Console.WriteLine($"Вижу проблему... Поздравляю, больше вас {patientOrgan.name} не будет беспокоить.");
                    Console.WriteLine($"За свои услуги я беру очень скромно. Всего {CalculatePrice()} рублей!");
                    return;
                }

                else //И орган здоров...
                {
                    Console.WriteLine($"Так, {patientOrgan.name} у вас в порядке...");
                    return;
                }
            }
        }
        //Если врач орган лечить не может
        Console.WriteLine($"{patientOrgan.name}, говорите? Это не ко мне...");
    }

    protected virtual int CalculatePrice()
    //И берет тоже по-своему...
    {
        var rndGen = new Random();
        return rndGen.Next(50000);
    }


}

