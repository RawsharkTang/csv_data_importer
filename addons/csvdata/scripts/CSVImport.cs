using Godot;
using Godot.Collections;
using nietras.SeparatedValues;
using System;
[Tool]
public partial class CSVImport : EditorImportPlugin {
    [Flags]
    public enum Presets {
        CSV = 1 << 0,

    }
    [Flags]
    public enum Delimiters {
        Auto = 1 << 0,
        COMMA = 1 << 1,
        TAB = 1 << 2
    }
    public override string _GetImporterName() {
        return "luoshark.csv_resource";
    }

    public override string _GetVisibleName() {
        return "CSVResource";
    }

    public override float _GetPriority() {
        return 2.0f;
    }

    public override int _GetImportOrder() {
        return 0;
    }

    public override string[] _GetRecognizedExtensions() {
        return ["csv", "tsv"];
    }


    public override string _GetResourceType() {
        return "Resource";
    }

    public override string _GetSaveExtension() {
        return "tres";
    }

    public override int _GetPresetCount() {
        return 1;
    }

    public override string _GetPresetName(int presetIndex) {
        return Enum.GetName((Presets)(1 << presetIndex));
    }
    public override bool _GetOptionVisibility(string path, StringName optionName, Dictionary options) {
        return true;
    }
    public override Array<Dictionary> _GetImportOptions(string path, int presetIndex) {
        var p = (Presets)(1 << presetIndex);
        Array<Dictionary> hint_dict = [];
        if (p == Presets.CSV) {
            hint_dict.Add(new Dictionary()
            {
                {"name","Parse"},
                {"default_value",false},
            });
            hint_dict.Add(
            new Dictionary(){
                {"name","Delimiter"},
                {"default_value",1},
                {"property_hint",(int)PropertyHint.Enum},
                {"hint_string","AUTO:0,COMMA:1,TAB:2"}
            });
            hint_dict.Add(new Dictionary()
            {
                {"name","SkipRow"},
                {"default_value",0},
            });
        }
        return hint_dict;
    }

    public override Error _Import(string sourceFile, string savePath, Dictionary options, Array<string> platformVariants, Array<string> genFiles) {
        var file = FileAccess.Open(sourceFile, FileAccess.ModeFlags.Read);
        if (FileAccess.GetOpenError() != Error.Ok) {
            GD.Print($"Failed to open file: {sourceFile}");
            return Error.Failed;
        }
        var parse = options["Parse"].AsBool();
        var delimiter = (Delimiters)(1 << options["Delimiter"].AsInt32());
        var skip_row = options["SkipRow"].AsInt32();
        skip_row = parse ? skip_row + 1 : skip_row;
        char delimiter_char = ',';
        switch (delimiter) {
            case Delimiters.COMMA: delimiter_char = ','; break;
            case Delimiters.TAB: delimiter_char = '\t'; break;
            default: break;
        }
        // GD.Print($"Read Option Parse:{parse},Delimiter:{Enum.GetName(delimiter)},Skip Row:{skip_row}");
        // using var auto_reader = Sep.Auto.Reader(o => o with { Unescape = true, Trim = SepTrim.All }).FromText(file.GetAsText());
        using var reader = delimiter == Delimiters.Auto ?
            Sep.Auto.Reader(o => o with { Unescape = true, Trim = SepTrim.All }).FromText(file.GetAsText()) : Sep.New(delimiter_char).Reader(o => o with { Unescape = true, Trim = SepTrim.All }).FromText(file.GetAsText());
        file.Close();
        var headers = reader.Header;
        // GD.Print(string.Join(",", headers.ColNames));

        var csv_resource = new CSVResource();
        if (!headers.IsEmpty) {
            csv_resource._headers = [.. headers.ColNames];
            csv_resource._data = [];
        }
        if (!reader.IsEmpty) {
            var type_row = parse && reader.MoveNext() ? ParseType(reader.Current, headers) : []; //Extract the types
            foreach (var row in reader) {
                if (row.RowIndex <= skip_row) continue;
                // var a = row[1];
                var value_dict = new Dictionary();
                var key = row[0].ToString();
                foreach (var h in csv_resource._headers) {
                    Variant v = Variant.From(row[h].ToString());
                    if (parse) {
                        type_row.TryGetValue(h, out string type_name);
                        switch (type_name) {
                            case "string":
                                v = Variant.From(row[h].Parse<string>());
                                // value_dict.Add(h, Variant.From(row[h].Parse<string>()));
                                break;
                            case "int":
                                v = Variant.From(row[h].Parse<int>());
                                // value_dict.Add(h, Variant.From(row[h].Parse<int>()));
                                break;
                            case "float":
                                v = Variant.From(row[h].Parse<float>());
                                // value_dict.Add(h, Variant.From(row[h].Parse<float>()));
                                break;
                            case "bool":
                                v = Variant.From(row[h].Parse<bool>());
                                // value_dict.Add(h, Variant.From(row[h].Parse<bool>()));
                                break;
                            case "json":
                                v = GD.StrToVar(row[h].ToString());
                                // value_dict.Add(h, GD.StrToVar(row[h].ToString()));
                                break;
                            default:
                                break;
                        }
                    }
                    // else
                    // {
                    //     v = Variant.From(row[h].ToString());
                    //     // value_dict.Add(h, GD.StrToVar(row[h].ToString()));
                    // }
                    value_dict.Add(h, v);
                }
                // csv_resource.Data.Add(key, value_dict);
                csv_resource._data.Add(key, value_dict);
                // GD.Print($"{key} : {value_dict}");
            }
        }
        reader.Dispose();
        var file_name = $"{savePath}.{_GetSaveExtension()}";
        var err = ResourceSaver.Save(csv_resource, file_name);
        if (err != Error.Ok) {
            GD.PrintErr($"Failed to save resource {file_name}");
        }
        return err;
    }

    public System.Collections.Generic.Dictionary<string, string> ParseType(SepReader.Row current, SepReaderHeader header) {
        var dict = new System.Collections.Generic.Dictionary<string, string>();
        foreach (var h in header.ColNames) {
            dict.Add(h, current[h].ToString());
        }
        return dict;
    }

}
