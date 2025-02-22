@tool
class_name CSVEditor
extends Control

@onready var data_container := %DataContainer
@onready var debug_bt: Button = %Debug
var test_data = preload("res://addons/csv-data-importer/visual_editor/Items.csv")
var shift_pressed := false


func _ready() -> void:
	debug_bt.pressed.connect(func(): _display_csv_data(test_data))


func _display_csv_data(csv_data: CSVData):
	if !csv_data:
		return
	## Clear Previous
	col_lists.clear()
	for c in data_container.get_children():
		c.queue_free()
	## Load New
	var array_2d := Array2D.new(csv_data.records)
	var headers := csv_data.headers
	row_count = len(array_2d.get_rows())
	col_count = len(headers)

	## create rows
	index_row_list = create_id_row()
	data_container.add_child(index_row_list)

	## create cols
	for col_idx in range(col_count):
		var header = headers[col_idx]
		var col_data = array_2d.get_col(col_idx)
		# print("Found Col:%s" % str(col_data))
		var c := _create_new_col(header, col_data)
		c.item_selected.connect(func(row_idx): select(col_idx, row_idx))
		c.multi_selected.connect(func(row_idx, selected): multi_select(col_idx, row_idx, selected))
		col_lists.append(c)
		data_container.add_child(c)
		pass


var row_count := 0
var col_count := 0
var index_row_list: ItemList
var col_lists: Array[ItemList]


func create_id_row() -> ItemList:
	var item_list := ItemList.new()
	item_list.auto_height = true
	item_list.auto_width = true
	item_list.allow_reselect = true
	item_list.select_mode = ItemList.SELECT_MULTI
	item_list.item_selected.connect(select_row)
	item_list.multi_selected.connect(multi_select_row)
	item_list.add_item("Row")
	for r in range(row_count):
		item_list.add_item(str(r))
		pass
	item_list.add_item("+")
	return item_list


func _create_new_col(header := "", rows := []) -> ItemList:
	if !header:
		return
	var item_list := ItemList.new()
	item_list.auto_height = true
	item_list.auto_width = true
	item_list.allow_reselect = true
	item_list.select_mode = ItemList.SELECT_MULTI
	item_list.add_item(header)

	for r in range(row_count):
		var r_data = rows[r] if len(rows) > r else ""
		r_data = str(r_data) if r_data else " "
		item_list.add_item(str(r_data))
		pass
	return item_list


func select(col := 0, row := 0):
	index_row_list.select(row)
	for col_list in col_lists:
		if col_lists.find(col_list) == col:
			continue
		col_list.deselect_all()
	# print("Last Select At (%d,%d)" % [last_selected_col + 1, last_selected_row])


func select_row(row := 0):
	var is_last_row = index_row_list.item_count == row + 1
	## if last deselect all other col
	if is_last_row:
		print("Try Add New Row")
		for col_list in col_lists:
			col_list.deselect_all()
	else:
		for col_list in col_lists:
			col_list.select(row)
	# print("Last Select At (%d,%d)" % [last_selected_col, last_selected_row])


func multi_select_row(row := 0, selected := false):
	# print("Selected (%d,%d) %s" % [-1, row, selected])
	var is_last_row = index_row_list.item_count == row + 1
	var selection := index_row_list.get_selected_items()
	## if last deselect all other col
	if !is_last_row:
		for col_list in col_lists:
			if selected:
				col_list.select(row, false)
			else:
				col_list.deselect(row)
			for r in col_list.get_selected_items():
				if !selection.has(r):
					col_list.deselect(r)
	else:
		if len(selection) == 1:
			for col_list in col_lists:
				col_list.deselect_all()


func multi_select(col := 0, row := 0, selected := false):
	# print("Selected (%d,%d) %s" % [col, row, selected])
	if selected:
		index_row_list.select(row, false)
	var selections = col_lists[col].get_selected_items()
	var index_selections = index_row_list.get_selected_items()
	for r in index_selections:
		if !selections.has(r):
			index_row_list.deselect(r)
			pass

	## deselect other columns
	for col_list in col_lists:
		if col_lists.find(col_list) == col:
			continue
		col_list.deselect_all()
	pass
