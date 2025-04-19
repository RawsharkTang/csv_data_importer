#if TOOLS
using Godot;
using System;

[Tool]
public partial class CSVData : EditorPlugin
{
	CSVImport CSVImport;
	public override void _EnterTree()
	{
		CSVImport = new CSVImport();
		AddImportPlugin(CSVImport);
		// Initialization of the plugin goes here.
	}

	public override void _ExitTree()
	{
		RemoveImportPlugin(CSVImport);
		// Clean-up of the plugin goes here.
	}
}
#endif
