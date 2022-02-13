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
