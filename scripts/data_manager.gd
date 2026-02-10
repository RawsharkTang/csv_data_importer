@tool
extends Node

@export_tool_button("print") var p = func(): print(var_to_str([1, 2, 3]))
@export_tool_button("Test Load") var l = _load_csv
@export_file_path("*.csv") var csv_path: String


func _ready() -> void:
	_load_csv()


func _load_csv() -> void:
	var csv = load(csv_path)
	if csv:
		print("CSV loaded as Resource: %s" % (csv is Resource))
		print("CSV loaded as CSVData: %s" % (csv is CSVData))
