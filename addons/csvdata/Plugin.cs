using System.Linq;
using Godot;
#nullable disable
namespace CSVDataImporter
{
	[Tool]
	public partial class Plugin : EditorPlugin
	{
		CSVImport CSVImport;
		public override string _GetPluginName()
		{
			return "CSVDataImporterPlugin";
		}

		public override void _EnterTree()
		{
			CSVImport = new CSVImport();
			AddImportPlugin(CSVImport, true);
			// Initialization of the plugin goes here.
		}

		public override void _ExitTree()
		{
			RemoveImportPlugin(CSVImport);
			// CSVImport.Free();
			CSVImport = null;
			// Clean-up of the plugin goes here.
		}

		// public override bool _Handles(GodotObject @object)
		// {
		// 	if (@object is Resource resource)
		// 	{
		// 		var ext = resource.ResourcePath.GetExtension().ToLower();
		// 		if (CSVImport._GetRecognizedExtensions().Contains(ext))
		// 		{
		// 			// GD.Print($"Can Edit {resource.ResourcePath}");
		// 			return true;
		// 		}
		// 	}
		// 	return base._Handles(@object);
		// }

		// public override void _Edit(GodotObject @object)
		// {
		// 	if (@object is Resource resource)
		// 	{
		// 		var import = resource.ResourcePath + ".import";
		// 		var importfile = new ConfigFile();
		// 		importfile.Load(import);
		// 		var remappedResource = importfile.GetValue("remap", "path");
		// 		// GD.Print($"Remapped resource: {remappedResource}");
		// 		var res = GD.Load<CSVResource>(remappedResource.AsString());
		// 		if (res != null)
		// 		{
		// 			// 🔥 Make it appear as if it belongs to the CSV file
		// 			// res.TakeOverPath(resource.ResourcePath);
		// 			EditorInterface.Singleton.InspectObject(res);
		// 		}
		// 	}
		// 	base._Edit(@object);
		// }


	}
}
