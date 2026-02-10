using System.Collections.Generic;
using Godot;
#nullable disable
namespace CSVDataImporter
{
    [GlobalClass, Tool]
    public partial class CSVData : Resource
    {
        [Export]
        public Godot.Collections.Array<StringName> Headers
        {
            get => [.. _headers]; // Convert List to Godot Array
            set => _headers = [.. value]; // Convert Godot Array to List
        }

        public List<StringName> _headers = [];

        /// <summary>
        /// Exported Data, for c# script use _data directly.
        /// </summary>
        [Export]
        public Godot.Collections.Dictionary<StringName, Godot.Collections.Dictionary> Data
        {
            get
            {
                return new Godot.Collections.Dictionary<StringName, Godot.Collections.Dictionary>(_data);
            }
            set
            {
                _data = new Dictionary<StringName, Godot.Collections.Dictionary>(value);
            }
        }

        public Dictionary<StringName, Godot.Collections.Dictionary> _data = [];


        public Godot.Collections.Dictionary Fetch(string key)
        {
            _data.TryGetValue(key, out var value);
            return value;
        }

    }
}
