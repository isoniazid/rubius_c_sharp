class Program1
{

    public static string ArrayToTabs(string[] lines)
    {
        var SB = new System.Text.StringBuilder();
        foreach (var element in lines)
        {
            SB.Append(element);
            SB.Append("\t");
        }
        return SB.ToString();
    }
    public static void Main()
    {

            string? currentPath;
            var currentFM = new FileManager();

            //Console.WriteLine("Введи путь к архиву");
            var archivePath = "UmpaLumpa.zip";
            currentFM.Unpack(archivePath);

            //Console.WriteLine("Введи путь к папке, в которой надо посмотреть содержимое");
            currentPath = "UmpaLumpa";
            var info = (currentFM.Observe(currentPath));
            foreach (var element in info) Console.WriteLine(element);

   

    }
}