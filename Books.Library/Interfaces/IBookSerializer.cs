namespace Books.Library.Interfaces;

public interface IBookSerializer
{
    T? ReadFromFile<T>(string path) where T : class;

    void WriteToFile<T>(T input, string path) where T : class;
}
