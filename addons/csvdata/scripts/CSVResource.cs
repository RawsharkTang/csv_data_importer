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

    public List<string> _headers = [];

    [Export]
    public Godot.Collections.Dictionary<string, Godot.Collections.Dictionary> Data
    {
        get => GetData();
        set => SetData(value);
    }


    public Dictionary<string, Godot.Collections.Dictionary> _data = [];

    public Godot.Collections.Dictionary<string, Godot.Collections.Dictionary> GetData()
    {
        return new Godot.Collections.Dictionary<string, Godot.Collections.Dictionary>(_data);
    }

    public void SetData(Godot.Collections.Dictionary<string, Godot.Collections.Dictionary> value)
    {
        _data = new Dictionary<string, Godot.Collections.Dictionary>(value);
    }

    public Godot.Collections.Dictionary Fetch(string key)
    {
        _data.TryGetValue(key, out var value);
        return value;
    }
}
