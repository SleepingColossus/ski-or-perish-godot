extends CharacterBody2D


func _ready():
    pass


func _process(_delta):
    move_and_slide()


func set_map_velocity(new_velocity: float):
    velocity.y = new_velocity
