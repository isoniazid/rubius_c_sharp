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
        try
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

            //Console.WriteLine("Введи НАЗВАНИЕ (без расширения) файла, в который нужно записать данные");
            //var filename = Console.ReadLine() + ".csv";
            var filename = "infa.csv";
            var pathToFile = currentFM.MakeFile(filename);
            currentFM.WriteToFile(filename, ArrayToTabs(info));

            currentFM.Delete(archivePath);
            Console.WriteLine($"Архив {archivePath} был удален нахрен");

            var appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            currentFM.MakeFile($"{appdataPath}/Lesson12Homework.txt");
            currentFM.WriteToFile($"{appdataPath}/Lesson12Homework.txt", pathToFile);
            Console.WriteLine($"Создан файл с путем к другому файлу. Ищи его здесь: {Environment.SpecialFolder.ApplicationData}/Lesson12Homework.txt");
        }

        catch (Exception ex) // ГЕНИАЛЬНЕЙШАЯ ОБРАБОТКА ОШИБОК
        {
            Console.WriteLine("Что-то пошло не так...");
            Console.WriteLine(ex.Message);
        }

    }
}