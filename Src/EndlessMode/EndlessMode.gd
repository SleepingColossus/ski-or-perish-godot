extends Node

@export var left_segments : Array[PackedScene]
@export var center_segments : Array[PackedScene]
@export var right_segments : Array[PackedScene]
@onready var maps_node : Node = $Maps

# from project setting: display/window/size/viewport_height
const Y_OFFSET = 567
var _current_segment : Map

func _ready():
    $Player.vertical_velocity_changed.connect(_on_Player_vertical_velocity_changed)

    var initial_segment = center_segments[randi() % center_segments.size()]
    var map = initial_segment.instantiate()
    _current_segment = map
    maps_node.add_child(map)

    _next_map()


func _process(_delta):
    pass


func _on_Player_vertical_velocity_changed(new_velocity: float):
    var maps = maps_node.get_children()
    for map in maps:
        map.set_map_velocity(new_velocity)

func _next_map():
    var next_segment : PackedScene

    if _current_segment.exit_point == MapEnums.CrossingPoint.LEFT:
        next_segment = left_segments[randi() % left_segments.size()]
    elif _current_segment.exit_point == MapEnums.CrossingPoint.RIGHT:
        next_segment = right_segments[randi() % right_segments.size()]
    else:
        next_segment = center_segments[randi() % center_segments.size()]

    var map = next_segment.instantiate()
    _current_segment = map
    maps_node.add_child(map)
    map.position.y = Y_OFFSET
