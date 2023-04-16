using System.IO.Compression;

public class FileManager
{
    Exception noPathException = new Exception("Путь не указан");


    public async Task<string> Unpack(string? path, string? destinationPath = null) //По умолчанию распакует в папку с программой
    {
        if (destinationPath == null) destinationPath = Directory.GetCurrentDirectory();
        if (path == null) throw (noPathException);


        if (path.Contains(".zip"))
        {
            try
            {
                await Task.Run(() => ZipFile.ExtractToDirectory(path, destinationPath));
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

    public async Task<string> Read(string? path)
    {
        var StrResult = new System.Text.StringBuilder();
        char[] result;
        if (path == null) throw (noPathException);


        if (File.Exists(path))
        {
            using (StreamReader reader = File.OpenText(path))
            {
                result = new char[reader.BaseStream.Length];
                await reader.ReadAsync(result, 0, (int)reader.BaseStream.Length);
            }

            foreach(var ch in result)
            {
                StrResult.Append(ch);
            }

            return StrResult.ToString();
        }

        else throw (new Exception($"Невозможно прочитать данные из файла {path}"));
    }

    public async Task<string[]> Observe(string? path)
    {
        if (path == null) throw (noPathException);

        try
        {
            List<string> result = new List<string>();

            string[] dirList = Directory.GetDirectories(path);
            for (int i = 0; i < dirList.Length; ++i)
            {
                dirList[i] += "\t" + Directory.GetLastWriteTime(dirList[i]);
                dirList[i] += "\tПапка";
            }
            result.AddRange(dirList);

            string[] filesList = Directory.GetFiles(path);
            for (int i = 0; i < filesList.Length; ++i)
            {
                filesList[i] += "\t" + File.GetLastWriteTime(filesList[i]);
                filesList[i] += "\tФайл";
            }
            result.AddRange(filesList);

            await Task.Delay(0); //Тут без комментариев)0)))
            return result.ToArray();
        }

        catch
        {
            throw (new Exception($"Невозможно посмотреть содержимое {path}"));
        }
    }

    public async Task<string> MakeFile(string? path)
    {
        if (path == null) throw (noPathException);

        try
        {
            await using FileStream currentFileStream = new FileStream(path, FileMode.CreateNew);
            await using var currentStreamWriter = new StreamWriter(currentFileStream);
            await currentStreamWriter.WriteAsync("");
            return "Файл создан";
        }

        catch
        {
            throw (new Exception($"Невозможно создать файл {path}"));
        }
    }

    public async Task WriteToFile(string? path, string data)
    {
        if (path == null) throw (noPathException);

        if (File.Exists(path))
        {
            await using FileStream currentFileStream = new FileStream(path, FileMode.Open);
            await using var currentStreamWriter = new StreamWriter(currentFileStream);
            await currentStreamWriter.WriteAsync(data);
        }

        else throw (new Exception($"Запись в несуществующий файл {path}"));
    }

    public void WriteToFile(string? path, string[] data)
    {
        if (path == null) throw (noPathException);

        if (File.Exists(path))
        {
            foreach (var line in data) File.WriteAllText(path, line);
        }

        else throw (new Exception($"Запись в несуществующий файл {path}"));
    }

    public void Delete(string? path)
    {
        if (path == null) throw (noPathException);

        if (File.Exists(path)) File.Delete(path);

        else throw (new Exception($"Удаление несуществущего файла {path}"));
    }

}