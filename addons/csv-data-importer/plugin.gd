@tool
extends EditorPlugin


var import_plugin
var editor:CSVEditor

func _enter_tree():
	editor = preload("res://addons/csv-data-importer/visual_editor/csv_editor.tscn").instantiate()
	EditorInterface.get_editor_main_screen().add_child(editor)
	import_plugin = preload("import_plugin.gd").new()
	add_import_plugin(import_plugin)

func _exit_tree():
	if editor:
		editor.queue_free()
	remove_import_plugin(import_plugin)
	import_plugin = null

func _has_main_screen() -> bool:
	return true
	



func _make_visible(visible):
	if editor:
		editor.visible = visible


func _get_plugin_name():
	return "DataTable"


func _get_plugin_icon():
	# Must return some kind of Texture for the icon.
	return EditorInterface.get_editor_theme().get_icon("Node", "EditorIcons")
