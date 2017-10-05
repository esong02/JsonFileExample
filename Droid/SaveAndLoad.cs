using System;
using System.IO;

using Xamarin.Forms;

using JsonFileExample.Interfaces;
using JsonFileExample.Droid;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace JsonFileExample.Droid
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public bool FindFile(string filename)
        {
            try{

				var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				var filePath = Path.Combine(documentsPath, filename);
				var file = System.IO.File.ReadAllText(filePath);

            }catch(FileNotFoundException e){
                System.Diagnostics.Debug.Write("Error: " + e.ToString());
                return false;
            }

            return true;
        }

        public string LoadText(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            return System.IO.File.ReadAllText(filePath);
        }

        public void SaveText(string filename, string text)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            System.IO.File.WriteAllText(filePath, text);
        }
    }
}
