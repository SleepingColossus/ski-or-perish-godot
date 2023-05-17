extends CanvasLayer


# Called when the node enters the scene tree for the first time.
func _ready():
    pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
    pass


func _on_hide_countdown_timer_timeout():
    $CountdownContainer/Countdown.visible = false


func update_score(new_score: int):
    var score_str = str(new_score)
    $ScoreContainer/Score.text = score_str


func show_game_over_info():
    $GameOverInfo.visible = true


func _on_touch_button_left_button_down():
    Global.left_button_pressed = true


func _on_touch_button_left_button_up():
    Global.left_button_pressed = false


func _on_touch_button_right_button_down():
    Global.right_button_pressed = true


func _on_touch_button_right_button_up():
    Global.right_button_pressed = false
