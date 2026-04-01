using CSVDataImporter;
using Godot;

public partial class CSharpLoadFirst : Node
{
    public override void _Ready()
    {
    }
    public Foo GetFoo(string id)
    {
        var data = GD.Load<CSVData>("res://scripts/Foo.csv");
        if (data == null)
        {
            GD.PrintErr("Failed to load CSV data.");
            return null;
        }
        var foo = data.ParseRow<Foo>(id);
        return foo;
    }
}