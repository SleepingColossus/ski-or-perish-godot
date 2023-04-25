extends CharacterBody2D

enum CrossingPoint {
    LEFT,
    CENTER,
    RIGHT,
}

@export_group("CrossingPoint")
@export var entryPoint: CrossingPoint
@export var exitPoint: CrossingPoint

func _ready():
    pass


func _process(_delta):
    move_and_slide()


func set_map_velocity(new_velocity: float):
    velocity.y = new_velocity
