public class GastroEnterologist : Doctor
{
   public GastroEnterologist(string name, string surname, int cabinetNumber) : base(name, surname, cabinetNumber)
   {
    speciality = "гастроэнтеролог";
    _organsToCure.Add(new Intestine());
    _organsToCure.Add(new Gaster());
   }

}