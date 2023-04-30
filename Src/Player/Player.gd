extends CharacterBody2D

enum PlayerState {
    IDLE,
    MOVE,
    JUMP,
    CRASH,
    INVINCIBLE,
    WIN,
    LOSE,
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
signal health_changed(new_health: int)

@export var max_health : int = 3
var health : int = max_health

# multiply vertical velocity by -1 instead of using negative literals which look ugly
const DIRECTION := -1
@export var base_speed :float = 400
@export var bonus_speed = 50
var _difficulty_level = 0

var _current_state = PlayerState.IDLE
var _angle := Angle.DOWN

@onready var sprite := $AnimatedSprite

@onready var turn_timer_ground = $TurnTimerGround
@onready var turn_timer_air = $TurnTimerAir
var _can_turn_ground := true
var _can_turn_air := true

var _starting_jump_angle
var _times_turned_this_jump

var _screen_width

func _ready():
    _change_state(PlayerState.IDLE)
    vertical_velocity_changed.emit(base_speed)

    _screen_width = get_viewport_rect().size.x


func _process(delta):
    if _current_state == PlayerState.MOVE or _current_state == PlayerState.JUMP or _current_state == PlayerState.INVINCIBLE:
        if Input.is_action_pressed("left"):
            _rotate_angle(-1)
        elif Input.is_action_pressed("right"):
            _rotate_angle(1)

        move_and_collide(velocity)

    if _current_state == PlayerState.MOVE or _current_state == PlayerState.INVINCIBLE:
        var x_velocity = _get_x_intensity() * base_speed * delta
        velocity.x = x_velocity
        move_and_collide(velocity)

        _adjust_velocity()

    global_position.x = clamp(global_position.x, 0, _screen_width)

func _rotate_angle(angle_delta: int):
    if(_current_state == PlayerState.MOVE or _current_state == PlayerState.INVINCIBLE):
        if(_can_turn_ground):
            _can_turn_ground = false
            turn_timer_ground.start()
        else:
            return

        _angle = _angle + angle_delta as Angle

        if(_angle < 0):
            _angle = Angle.LEFT
        elif(_angle > Angle.RIGHT):
            _angle = Angle.RIGHT

        sprite.frame = _angle
        _adjust_velocity()

    if(_current_state == PlayerState.JUMP):
        if(_can_turn_air):
            _can_turn_air = false
            turn_timer_air.start()
        else:
            return

        _angle = _angle + angle_delta as Angle

        if(_angle < Angle.LEFT):
            _angle = Angle.LEFT_UP1
        elif(_angle > Angle.LEFT_UP1):
            _angle = Angle.LEFT

        sprite.frame = _angle

        _times_turned_this_jump += 1

        if _angle == _starting_jump_angle and _times_turned_this_jump >= Angle.size():
            _times_turned_this_jump = 0
            $Spin360Sound.play()
            $Spin360Animation.play("360_vfx_on")


func _adjust_velocity():
    var y_intensity := _get_y_intensity()
    var difficulty_bonus = _difficulty_level * bonus_speed
    var y_final = (base_speed + difficulty_bonus) * y_intensity * DIRECTION

    vertical_velocity_changed.emit(y_final)

func _change_state(state):
    _current_state = state
    match state:
        PlayerState.IDLE:
            sprite.play("idle")
            vertical_velocity_changed.emit(0)
        PlayerState.MOVE:
            sprite.play("move")
            sprite.pause()
            sprite.frame = _angle
            $InvincibilityFlicker.stop()
        PlayerState.JUMP:
            sprite.play("jump")
            sprite.pause()
            sprite.frame = _angle
            _starting_jump_angle = _angle
            _times_turned_this_jump = 0
            $JumpAnimation.play("jump_on")
            $JumpSound.play()
        PlayerState.CRASH:
            sprite.play("crash")
            vertical_velocity_changed.emit(0)
            $CrashSound.play()
            health -= 1
            health_changed.emit(health)
            if health <= 0:
                _change_state(PlayerState.LOSE)
            else:
                $CrashTimer.start()
        PlayerState.INVINCIBLE:
            sprite.play("move")
            sprite.pause()
            sprite.frame = _angle
            $InvincibilityFlicker.play("flicker_on")
            $InvincibilityTimer.start()
        PlayerState.WIN:
            sprite.play("win")
            vertical_velocity_changed.emit(0)
            $VictorySound.play()
        PlayerState.LOSE:
            sprite.play("crash")
            vertical_velocity_changed.emit(0)
            $CrashSound.play()

func land():
    if _angle >= Angle.LEFT and _angle <= Angle.RIGHT:
        _change_state(PlayerState.MOVE)
    else:
        _angle = Angle.DOWN
        _change_state(PlayerState.CRASH)


# determine how fast the player should be moving horizontally based on current angle
func _get_x_intensity() -> float:
    match _angle:
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
func _get_y_intensity() -> float:
    match _angle:
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
    _can_turn_ground = true


func _on_turn_timer_air_timeout():
    _can_turn_air = true


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
                if _current_state == PlayerState.MOVE:
                    _change_state(PlayerState.CRASH)
            elif tile_type == 1:
                if not _current_state == PlayerState.WIN:
                    _change_state(PlayerState.WIN)
            elif tile_type == 2:
                if not _current_state == PlayerState.JUMP:
                    _change_state(PlayerState.JUMP)


func _on_crash_timer_timeout():
    if _current_state == PlayerState.CRASH:
        _change_state(PlayerState.INVINCIBLE)


func _on_invincibility_timer_timeout():
    if _current_state == PlayerState.INVINCIBLE:
        _change_state(PlayerState.MOVE)


func start():
    _change_state(PlayerState.MOVE)


func increase_difficulty():
    _difficulty_level += 1
    print_debug("Difficulty increased to %s" % _difficulty_level)
