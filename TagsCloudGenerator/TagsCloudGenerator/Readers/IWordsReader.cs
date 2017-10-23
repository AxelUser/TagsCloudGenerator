using System.Collections.Generic;

namespace TagsCloudGenerator.Readers
{
    public interface IWordsReader
    {
        string[] SupportedFileExtensions { get; }

        bool CanHandle(string filename);

        List<string> GetAllWords(string filepath);
    }
}
