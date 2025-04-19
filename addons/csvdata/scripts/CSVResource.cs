using System.Collections.Generic;
using Godot;

public partial class CSVResource : Resource
{
    [Export]
    public Godot.Collections.Array<string> Headers
    {
        get => [.. _headers]; // Convert List to Godot Array
        set => _headers = [.. value]; // Convert Godot Array to List
    }

    public List<string> _headers = new List<string>();

    [Export]
    public Godot.Collections.Dictionary<string, Godot.Collections.Dictionary> Data
    {
        get => new Godot.Collections.Dictionary<string, Godot.Collections.Dictionary>(_data);
        set => _data = new Dictionary<string, Godot.Collections.Dictionary>(value);
    }

    public Dictionary<string, Godot.Collections.Dictionary> _data = new();

    public Godot.Collections.Dictionary Fetch(string key)
    {
        _data.TryGetValue(key, out var value);
        return value;
    }
}
