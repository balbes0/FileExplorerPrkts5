using System.IO;

namespace FileExplorer.Model
{
    public class FileItem
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public FileItem(string path)
        {
            if (path.Length == 3 && path[1] == ':') 
            {
                Name = "Локальный диск:";
            }
            else
            {
                Name = System.IO.Path.GetFileName(path);
            }
            Path = path;
        }
    }
}
