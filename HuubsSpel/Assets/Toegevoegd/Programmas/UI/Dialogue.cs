using System;

[Serializable]
public class Dialogue
{
    public string[] ToSay;
    public string Reply { get; set; }
    public string[] Options;

    public static implicit operator Dialogue(string v)
    {
        throw new NotImplementedException();
    }
}