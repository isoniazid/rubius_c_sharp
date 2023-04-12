using System.IO.Compression;

public class FileManager
{
    Exception noPathException = new Exception("Путь не указан");


    public string Unpack(string? path, string? destinationPath = null) //По умолчанию распакует в папку с программой
    {
        if (destinationPath == null) destinationPath = Directory.GetCurrentDirectory();
        if (path == null) throw (noPathException);


        if (path.Contains(".zip"))
        {
            try
            {
                ZipFile.ExtractToDirectory(path, destinationPath);
                return "Архив распакован";
            }

            catch
            {
                throw (new Exception($"Невозможно распаковать архив {path}"));
                /*Вообще, это, наверно,
                довольно тупо при нахождении исключения генерировать новое исключение,
                но у меня же свой класс, я должен предоставить свой интерфейс и свои ошибки))*/
            }
        }
        else
        {
            throw (new Exception("Формат архива не поддерживается или путь указан неверно"));
        }
    }

    public string Read(string? path)
    {
        if (path == null) throw (noPathException);

        if (File.Exists(path)) return File.ReadAllText(path);

        else throw (new Exception($"Невозможно прочитать данные из файла {path}"));
    }

    public string[] Observe(string? path)
    {
        if (path == null) throw (noPathException);

        try
        {
            List<string> result = new List<string>();

            string[] dirList = Directory.GetDirectories(path);
            for(int i = 0; i <dirList.Length; ++i)
            {
                dirList[i]+="\t"+Directory.GetLastWriteTime(dirList[i]);
                dirList[i]+="\tПапка";
            }
            result.AddRange(dirList);

            string[] filesList = Directory.GetFiles(path);
            for(int i = 0; i <filesList.Length; ++i)
            {
                filesList[i]+="\t"+File.GetLastWriteTime(filesList[i]);
                filesList[i]+="\tФайл";
            }
            result.AddRange(filesList);


            return result.ToArray();
        }

        catch
        {
            throw (new Exception($"Невозможно посмотреть содержимое {path}"));
        }
    }

    public string MakeFile(string? path)
    {
        if (path == null) throw (noPathException);

        try
        {
            var currentFile = File.Create(path);
            currentFile.Close();
            return Path.GetFullPath(path);
        }

        catch
        {
            throw (new Exception($"Невозможно создать файл {path}"));
        }
    }

    public void WriteToFile(string? path, string data)
    {
        if (path == null) throw (noPathException);

        if (File.Exists(path)) File.WriteAllText(path, data);

        else throw (new Exception($"Запись в несуществующий файл {path}"));
    }

    public void Delete(string? path)
    {
        if (path == null) throw (noPathException);

        if (File.Exists(path)) File.Delete(path);

        else throw (new Exception($"Удаление несуществущего файла {path}"));
    }

}