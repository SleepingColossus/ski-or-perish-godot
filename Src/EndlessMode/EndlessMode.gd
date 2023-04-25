extends Node

@export var left_segments : Array[PackedScene]
@export var center_segments : Array[PackedScene]
@export var right_segments : Array[PackedScene]
@onready var maps_node : Node = $Maps


func _ready():
    $Player.vertical_velocity_changed.connect(_on_Player_vertical_velocity_changed)

    var initial_segment = center_segments[randi() % center_segments.size()]
    var map = initial_segment.instantiate()
    maps_node.add_child(map)

func _process(_delta):
    pass


func _on_Player_vertical_velocity_changed(new_velocity: float):
    var maps = maps_node.get_children()
    for map in maps:
        map.set_map_velocity(new_velocity)
