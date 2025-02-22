@tool
class_name CSVData
extends Resource

## column name
@export var headers := []
## origin data
@export var records := []:
	set(v):
		records = v
		if _auto_setup:
			setup()

var _data := {}  #column name to index
@export var _auto_setup = false
@export var _initialed = false ## export this property so the reimport process will refresh it
## _data getter
var data:
	get:
		return _data


func _init(auto_setup = false):
	_auto_setup = auto_setup


func setup():
	if _initialed:
		push_warning(">_< csv file already setuped !")
		return self
	print("Setting up " + resource_name + ".csv")
	_initialed = true
	var field_indexs = {}
	_data.clear()
	for i in range(headers.size()):
		field_indexs[headers[i]] = i

	for i in range(headers.size()):
		for row in records:
			var primary_key = row[0]
			var row_data = {}
			for key in headers:
				var index = field_indexs[key]
				var value = row[index]
				row_data[key] = value
			_data[str(primary_key)] = row_data
			# print("Add Row: {0}".format([primary_key]))
	#headers.clear()
	#records.clear()

	return self


func fetch(primary_key):
	return _data.get(str(primary_key))


func keys():
	return _data.keys()
