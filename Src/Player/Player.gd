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

signal vertical_velocity_changed(new_velocity: float)

@export var max_health : int = 3
var health : int = max_health

# multiply vertical velocity by -1 instead of using negative literals which look ugly
const DIRECTION := -1
@export var base_speed :float = 200
@export var accelerated_speed :float = 400
var _accelerating := false

var current_state = PlayerState.IDLE
var angle := Angle.DOWN

@onready var sprite := $AnimatedSprite

@onready var turn_timer_ground = $TurnTimerGround
@onready var turn_timer_air = $TurnTimerAir
var can_turn_ground := true
var can_turn_air := true


func _ready():
    change_state(PlayerState.MOVE)
    vertical_velocity_changed.emit(base_speed)


func _process(delta):
    if(current_state == PlayerState.MOVE or current_state == PlayerState.JUMP) or current_state == PlayerState.INVINCIBLE:
        if Input.is_action_pressed("left"):
            rotate_angle(-1)
        elif Input.is_action_pressed("right"):
            rotate_angle(1)

        var x_velocity = get_x_intensity() * base_speed * delta

        velocity.x = x_velocity

        move_and_collide(velocity)

    if current_state == PlayerState.MOVE or current_state == PlayerState.INVINCIBLE:
        if Input.is_action_just_pressed("down"):
            _accelerating = true
        if Input.is_action_just_released("down"):
            _accelerating = false

        _adjust_velocity()

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
        _adjust_velocity()

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


func _adjust_velocity():
    var y_base := accelerated_speed if _accelerating else base_speed
    var y_intensity := get_y_intensity()
    var y_final = y_base * y_intensity * DIRECTION

    vertical_velocity_changed.emit(y_final)

func change_state(state):
    current_state = state
    match state:
        PlayerState.IDLE:
            sprite.play("idle")
            vertical_velocity_changed.emit(0)
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
            vertical_velocity_changed.emit(0)
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
            vertical_velocity_changed.emit(0)
            $VictorySound.play()


# determine how fast the player should be moving horizontally based on current angle
func get_x_intensity() -> float:
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
        _ : return 0


# determine how fast the player should be moving vertically based on current angle
func get_y_intensity() -> float:
    match angle:
        Angle.LEFT: return 0
        Angle.LEFT_DOWN1: return 0.25
        Angle.LEFT_DOWN2: return 0.5
        Angle.LEFT_DOWN3: return 0.75
        Angle.DOWN: return 1
        Angle.RIGHT_DOWN3: return 0.75
        Angle.RIGHT_DOWN2: return 0.5
        Angle.RIGHT_DOWN1: return 0.25
        Angle.RIGHT: return 0
        _ : return 0


func _on_turn_timer_ground_timeout():
    can_turn_ground = true


func _on_turn_timer_air_timeout():
    can_turn_air = true


func _on_area_2d_body_shape_entered(body_rid: RID, body: Node2D, _body_shape_index: int, _local_shape_index: int) -> void:
    if body is TileMap:
        var collided_tile_coords = body.get_coords_for_body_rid(body_rid)

        for index in body.get_layers_count():
            # do not check for collisions on background layer
            if index == 0:
                continue

            var tile_data = body.get_cell_tile_data(index, collided_tile_coords)

            if not (tile_data is TileData):
                continue

            var tile_type = tile_data.get_custom_data_by_layer_id(0)

            if tile_type == 0:
                if current_state == PlayerState.MOVE:
                    change_state(PlayerState.CRASH)
            elif tile_type == 1:
                if not current_state == PlayerState.WIN:
                    change_state(PlayerState.WIN)


func _on_crash_timer_timeout():
    if current_state == PlayerState.CRASH:
        change_state(PlayerState.INVINCIBLE)


func _on_invincibility_timer_timeout():
    if current_state == PlayerState.INVINCIBLE:
        change_state(PlayerState.MOVE)
