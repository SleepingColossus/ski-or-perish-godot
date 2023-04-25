class_name Map

extends CharacterBody2D

signal map_destroyed(map: Map)

@export_group("CrossingPoint")
@export var entry_point: MapEnums.CrossingPoint
@export var exit_point: MapEnums.CrossingPoint

# from project setting: display/window/size/viewport_height
const DESTROY_OFFSET = 567

func _ready():
    pass


func _process(_delta):
    move_and_slide()

    if position.y <= -DESTROY_OFFSET:
        map_destroyed.emit(self)
        queue_free()


func set_map_velocity(new_velocity: float):
    velocity.y = new_velocity
