public class Cardiologist : Doctor
{
   public Cardiologist(string name, string surname, int cabinetNumber) : base(name, surname, cabinetNumber)
   {
    speciality = "кардиолог";
    _organsToCure.Add(new Heart());
   }

}