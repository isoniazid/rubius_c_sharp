public class Hepatologist : Doctor
{
    public Hepatologist(string name, string surname, int cabinetNumber) : base(name, surname, cabinetNumber)
    {
        speciality = "Гепатолог";
        _organsToCure.Add(new Liver());
    }

}