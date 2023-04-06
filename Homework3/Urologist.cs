public class Urologist : Doctor
{
    public Urologist(string name, string surname, int cabinetNumber) : base(name, surname, cabinetNumber)
    {
        speciality = "уролог";
        _organsToCure.Add(new Kidney());
    }

}