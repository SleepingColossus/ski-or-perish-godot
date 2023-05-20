extends Node

@export var left_segments : Array[PackedScene]
@export var center_segments : Array[PackedScene]
@export var right_segments : Array[PackedScene]
@onready var maps_node : Node = $Maps

# from project setting: display/window/size/viewport_height
const Y_OFFSET = 567

var _previous_segment : Map
var _current_segment : Map

const SCORE_FACTOR = 10000
const DIFFICULTY_THRESHOLD = 100
var _game_velocity : float
var _total_distance : float
var _previous_difficulty: int = 0
@export var score_360_jump : int
var _total_360_jumps : int = 0

var _can_retry := false

func _ready():
    $Player.vertical_velocity_changed.connect(_on_Player_vertical_velocity_changed)
    $Player.health_changed.connect(_on_Player_health_changed)
    $Player.full_circle_jump_performed.connect(_on_Player_full_circle_jump)
    $Player.game_lost.connect(_on_Player_game_lost)


func _process(_delta):
    _total_distance += abs(_game_velocity)
    var score = int(_total_distance / SCORE_FACTOR) + (_total_360_jumps * score_360_jump)
    $GameplayInterface.update_score(score)

    var difficulty = int(score / DIFFICULTY_THRESHOLD)
    if difficulty != _previous_difficulty:
        $Player.increase_difficulty()

    _previous_difficulty = difficulty

    if Input.is_action_pressed("ui_cancel"):
        get_tree().change_scene_to_file("res://MainMenu/MainMenu.tscn")

    if Input.is_action_pressed("retry") and _can_retry:
        get_tree().reload_current_scene()


func _on_Player_vertical_velocity_changed(new_velocity: float):
    _game_velocity = new_velocity

    var maps = maps_node.get_children()
    for map in maps:
        map.set_map_velocity(new_velocity)


func _on_Player_health_changed(new_health: int):
    $GameplayInterface/HeartIndicators.set_health(new_health)


func _on_Player_full_circle_jump():
    _total_360_jumps += 1


func _on_Player_game_lost():
    $GameplayInterface.show_game_over_info()


func _on_start_timer_timeout():
    $Player.start()


func _on_retry_timer_timeout():
    _can_retry = true
