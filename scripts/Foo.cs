using CSVDataImporter;
using Godot;
[GlobalClass]
public partial class Foo : RefCounted, IIndexed
{
    public StringName ID { get; set; }
    [ColumnName("foo")] public string FOO { get; set; }
    public override string ToString()
    {
        return $"ID: {ID}, FOO: {FOO}";
    }
    
}