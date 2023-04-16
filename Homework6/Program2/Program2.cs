class Program2
{
    public static List<(string name, DateTime creationTime, string type)> Parse(string str)
    {
        string[] rawStrs = str.Split("\t");
        var result = new List<(string name, DateTime creationTime, string type)>();

        for (int i = 0; i < rawStrs.Length - 1; i += 3)
        {
            result.Add((rawStrs[i], Convert.ToDateTime(rawStrs[i + 1]), rawStrs[i + 2]));
        }

        for (int i = 0; i < result.Count; ++i) //Тут я написал свою сортировку, самую простую
        {
            for (int j = 0; j < result.Count; ++j)
            {
                if (result[i].Item2 > result[j].Item2)
                {
                    (result[i], result[j]) = (result[j], result[i]);
                }
            }
        }

        return result;
    }

    public static async Task Main()
    {

        try
        {
            var appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var pathToFileWithPath = $"{appdataPath}/Lesson12Homework.txt";
            var currentFM = new FileManager();

            Console.WriteLine("Читаю файл с путем...");
            string pathToCSV = await currentFM.Read(pathToFileWithPath);

            Console.WriteLine("Читаю файл со списком...");
            string filesListRaw = await currentFM.Read(pathToCSV);
            var filesList = Parse(filesListRaw);
            foreach (var element in filesList) Console.WriteLine(element);


            currentFM.Delete(pathToFileWithPath);
            Console.WriteLine("Удалил файл с путем");

        }

        catch (Exception ex) // ГЕНИАЛЬНЕЙШАЯ ОБРАБОТКА ОШИБОК
        {
            Console.WriteLine("Что-то пошло не так...");
            Console.WriteLine(ex.Message);
        }

    }
}