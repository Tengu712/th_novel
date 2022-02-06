using System.Drawing;
using System.Resources;

class ResGen
{
    public static void Main(string[] args)
    {
        try
        {
            var writer = new ResXResourceWriter("resource.resx");
            writer.AddResource("reimu", Bitmap.FromFile("../res/reimu.png"));
            writer.Close();
        }
        catch (System.IO.FileNotFoundException e)
        {
            System.Console.WriteLine(e.ToString());
        }
    }
}
