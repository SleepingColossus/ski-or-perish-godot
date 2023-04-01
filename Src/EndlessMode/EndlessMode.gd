extends Node

@onready var maps : Array[Node] = $Maps.get_children()


func _ready():
    $Player.vertical_velocity_changed.connect(_on_Player_vertical_velocity_changed)


func _process(delta):
    pass


func _on_Player_vertical_velocity_changed(new_velocity: float):
    for map in maps:
        map.set_map_velocity(new_velocity)
