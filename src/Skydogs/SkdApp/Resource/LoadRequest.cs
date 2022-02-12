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

    public void AddCharacterKeysWithString(string str, string fontname)
    {
        for (int i = 0; i < str.Length; ++i)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("chr.");
            sb.Append(str[i]);
            sb.Append(".");
            sb.Append(fontname);
            Keys.Add(sb.ToString());
        }
    }
}
