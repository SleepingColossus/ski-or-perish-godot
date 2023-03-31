extends Node

enum PlayerState {
    IDLE,
    MOVE,
    JUMP,
    CRASH,
    WIN,
}

enum Angle {
    # ground and air
    LEFT,
    LEFT_DOWN1,
    LEFT_DOWN2,
    LEFT_DOWN3,
    DOWN,
    RIGHT_DOWN3,
    RIGHT_DOWN2,
    RIGHT_DOWN1,
    RIGHT,

    # air only
    RIGHT_UP1,
    RIGHT_UP2,
    RIGHT_UP3,
    UP,
    LEFT_UP3,
    LEFT_UP2,
    LEFT_UP1,
}

@export var max_health : int = 3
var health : int = max_health

@export var base_speed = 100

var current_state = PlayerState.IDLE
var angle := Angle.DOWN

@onready var sprite := $AnimatedSprite

@onready var turn_timer_ground = $TurnTimerGround
@onready var turn_timer_air = $TurnTimerAir
var can_turn_ground := true
var can_turn_air := true

# Called when the node enters the scene tree for the first time.
func _ready():
    change_state(PlayerState.MOVE)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
    if(current_state == PlayerState.MOVE or current_state == PlayerState.JUMP):
        if Input.is_action_pressed("left"):
            rotate(-1)
        elif Input.is_action_pressed("right"):
            rotate(1)

func rotate(angle_delta: int):
    if(current_state == PlayerState.MOVE):
        if(can_turn_ground):
            can_turn_ground = false
            turn_timer_ground.start()
        else:
            return

        angle = angle + angle_delta as Angle

        if(angle < 0):
            angle = Angle.LEFT
        elif(angle > Angle.RIGHT):
            angle = Angle.RIGHT

        sprite.frame = angle

    if(current_state == PlayerState.JUMP):
        if(can_turn_air):
            can_turn_air = false
            turn_timer_air.start()
        else:
            return

        angle = angle + angle_delta as Angle

        if(angle < Angle.LEFT):
            angle = Angle.LEFT_UP1
        elif(angle > Angle.LEFT_UP1):
            angle = Angle.LEFT

        sprite.frame = angle

func change_state(state):
    current_state = state
    match state:
        PlayerState.IDLE:
            sprite.play("idle")
        PlayerState.MOVE:
            sprite.play("move")
            sprite.pause()
            sprite.frame = angle
        PlayerState.JUMP:
            sprite.play("jump")
            sprite.pause()
            sprite.frame = angle
        PlayerState.CRASH:
            sprite.play("crash")
        PlayerState.WIN:
            sprite.play("win")


func _on_turn_timer_ground_timeout():
    can_turn_ground = true


func _on_turn_timer_air_timeout():
    can_turn_air = true