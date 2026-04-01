@tool
extends Node


func _ready():
	var c = get_node("%CSharpLoader")
	var foo = c.GetFoo("1")
	print("Got foo: %s" % foo)
