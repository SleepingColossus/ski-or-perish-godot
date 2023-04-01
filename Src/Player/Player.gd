extends CharacterBody2D

enum PlayerState {
    IDLE,
    MOVE,
    JUMP,
    CRASH,
    INVINCIBLE,
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

@export var base_speed = 200

var current_state = PlayerState.IDLE
var angle := Angle.DOWN

@onready var sprite := $AnimatedSprite

@onready var turn_timer_ground = $TurnTimerGround
@onready var turn_timer_air = $TurnTimerAir
var can_turn_ground := true
var can_turn_air := true


func _ready():
    change_state(PlayerState.MOVE)


func _process(delta):
    if(current_state == PlayerState.MOVE or current_state == PlayerState.JUMP) or current_state == PlayerState.INVINCIBLE:
        if Input.is_action_pressed("left"):
            rotate_angle(-1)
        elif Input.is_action_pressed("right"):
            rotate_angle(1)

        var x_velocity = get_x_intensity() * base_speed * delta

        velocity.x = x_velocity

        move_and_collide(velocity)


func rotate_angle(angle_delta: int):
    if(current_state == PlayerState.MOVE or current_state == PlayerState.INVINCIBLE):
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
            $InvincibilityFlicker.stop()
        PlayerState.JUMP:
            sprite.play("jump")
            sprite.pause()
            sprite.frame = angle
        PlayerState.CRASH:
            sprite.play("crash")
            $CrashSound.play()
            $CrashTimer.start()
        PlayerState.INVINCIBLE:
            sprite.play("move")
            sprite.pause()
            sprite.frame = angle
            $InvincibilityFlicker.play("flicker_on")
            $InvincibilityTimer.start()
        PlayerState.WIN:
            sprite.play("win")


func get_x_intensity():
    match angle:
        Angle.LEFT: return -1
        Angle.LEFT_DOWN1: return -1
        Angle.LEFT_DOWN2: return -0.75
        Angle.LEFT_DOWN3: return -0.5
        Angle.DOWN: return 0
        Angle.RIGHT_DOWN3: return 0.5
        Angle.RIGHT_DOWN2: return 0.75
        Angle.RIGHT_DOWN1: return 1
        Angle.RIGHT: return 1


# TODO: merge with _on_area_2d_body_entered?
func _crash():
    change_state(PlayerState.CRASH)


func _on_turn_timer_ground_timeout():
    can_turn_ground = true


func _on_turn_timer_air_timeout():
    can_turn_air = true


func _on_area_2d_body_entered(_body):
    if current_state == PlayerState.MOVE:
        _crash()


func _on_crash_timer_timeout():
    if current_state == PlayerState.CRASH:
        change_state(PlayerState.INVINCIBLE)


func _on_invincibility_timer_timeout():
    if current_state == PlayerState.INVINCIBLE:
        change_state(PlayerState.MOVE)
