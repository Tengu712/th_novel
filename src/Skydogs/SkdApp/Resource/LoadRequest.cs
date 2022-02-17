namespace Skydogs.SkdApp.Resource;

using System.Collections.Generic;

class LoadImageRequest
{
    public HashSet<string> Keys { get; } = new HashSet<string>();

    public LoadImageRequest() { }

    public void Add(string key)
    {
        Keys.Add(key);
    }

    public void Add(string[] keys)
    {
        foreach (var i in keys)
            Keys.Add(i);
    }
}
