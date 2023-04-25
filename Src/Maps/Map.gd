class_name Map

extends CharacterBody2D

@export_group("CrossingPoint")
@export var entry_point: MapEnums.CrossingPoint
@export var exit_point: MapEnums.CrossingPoint

func _ready():
    pass


func _process(_delta):
    move_and_slide()


func set_map_velocity(new_velocity: float):
    velocity.y = new_velocity
