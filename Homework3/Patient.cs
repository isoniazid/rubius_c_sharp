public class Patient 
{

    /*
    Я мог бы сделать не публичные поля, а свойства, и сделать у них приватный сеттер.
    Это вопрос безопасноти, который я не стал прорабатывать, 
    чтобы не писать дополнительный код)))) Поэтому я просто сделал поля публичными.
    */
    public string name = "Не задано";
    public string surname = "Не задано";
    public int age;

    /*
    Я не стал прописывать все на свете органы, только часть.
    Зато, обратите внимание, у пациента две почки.
    И одна может быть больной, а вторая здоровой, это важно
    Более того, поскольку коллекция публичная - никто не мешает добавить 
    извне какой-нибудь новый орган, например процессор AMD Athlon в седалище
    */
    public List<Organ> organs = new List<Organ>() {
    new Kidney(),
    new Kidney(),
    new Liver(),
    new Mentality(),
    new Gaster(),
    new Intestine(),
    new Heart()
     };

     public Patient(string name, string surname, int age)
     {
        this.name = name;
        this.surname = surname;
        this.age = age;
        Console.WriteLine($"В больницу обратился пациент: {name} {surname}, возраст - {age}");
        BecomeIll();
     }


     void BecomeIll()
     {
        var rndgen = new Random();
        Console.WriteLine($"{name} {surname} заболел...");
        foreach(Organ currentOrgan in organs)
        {
            bool currentState = rndgen.Next(2)==0; //Это, по-сути, булейный рандом
            currentOrgan.state = currentState ? ORGAN_STATES.HEALTHY : ORGAN_STATES.UNHEALTHY;
            if(currentOrgan.state == ORGAN_STATES.UNHEALTHY) Console.WriteLine($"{currentOrgan.name} что-то беспокоит...");
        }
        
     }




}