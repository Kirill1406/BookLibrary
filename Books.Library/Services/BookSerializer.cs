using Books.Library.Interfaces;
using System.Xml.Serialization;

namespace Books.Library.Services;

public class BookSerializer : IBookSerializer
{
    public T? ReadFromFile<T>(string path) where T : class
    {
        ArgumentNullException.ThrowIfNullOrEmpty(path);

        var serializer = new XmlSerializer(typeof(T));

        using var sr = new StreamReader(path);
        return serializer.Deserialize(sr) as T;
    }

    public void WriteToFile<T>(T input, string path) where T : class
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(path, nameof(path));
        ArgumentNullException.ThrowIfNull(input, nameof(input));

        using var fs = File.Create(path);

        var serializer = new XmlSerializer(typeof(T));

        serializer.Serialize(fs, input);
    }
}
