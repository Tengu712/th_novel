using System;
using System.Drawing;
using System.IO;
using System.Resources;

class ResGen
{
    public static void Main(string[] args)
    {
        try
        {
            var writer = new ResXResourceWriter("resource.resx");
            // Images
            string[] pngFiles = Directory.GetFiles("../res/", "*.png", SearchOption.TopDirectoryOnly);
            foreach (var f in pngFiles)
                writer.AddResource(f.Substring(7, f.Length - 11), Bitmap.FromFile(f));
            // Fonts
            string[] fntFiles = Directory.GetFiles("../res/", "*.otf", SearchOption.TopDirectoryOnly);
            foreach (var f in fntFiles)
            {
                var fs = new FileStream(f, FileMode.Open, FileAccess.Read);
                byte[] arr = new byte[fs.Length];
                fs.Read(arr, 0, arr.Length);
                fs.Close();
                writer.AddResource(f.Substring(7, f.Length - 11), arr);
            }
            // End
            writer.Close();
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.ToString());
        }
        catch (DirectoryNotFoundException e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
