using System;
namespace JsonFileExample.Interfaces
{
    public interface ISaveAndLoad
    {
        void SaveText(string filename, string text);
        string LoadText(string filename);
        bool FindFile(string filename);
    }
}
