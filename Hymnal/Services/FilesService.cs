namespace Hymnal.Services;

public sealed class FilesService
{
    public FilesService()
    { }

    public async Task<string> ReadFileAsync(string fileName)
    {
        using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(fileStream);

        return reader.ReadToEnd();
    }
}
