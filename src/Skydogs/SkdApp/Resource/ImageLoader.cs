using System.Collections.Generic;

namespace Skydogs.SkdApp.Resource;

class ImageLoader
{
    private static HashSet<string> s_set = new HashSet<string>();
    private static HashSet<string> s_setTemp = new HashSet<string>();

    public static bool Load(LoadImageRequest request)
    {
        foreach (var i in request.Keys)
        {
            if (s_set.Contains(i))
                continue;
            if (!ResourceX.LoadImage(i))
                return false;
            s_set.Add(i);
        }
        return true;
    }

    public static bool LoadTemp(LoadImageRequest request)
    {
        var unloadset = new HashSet<string>(s_setTemp);
        unloadset.ExceptWith(request.Keys);
        foreach (var i in unloadset)
        {
            DirectX.UnloadImageWithKey(i);
            s_setTemp.Remove(i);
        }
        foreach (var i in request.Keys)
        {
            if (s_setTemp.Contains(i) || s_set.Contains(i))
                continue;
            if (!ResourceX.LoadImage(i))
                return false;
            s_setTemp.Add(i);
        }
        return true;
    }

    private ImageLoader() { }
}
