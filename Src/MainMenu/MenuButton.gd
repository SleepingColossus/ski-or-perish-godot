extends Button

@export var scene_to_load: String


func _on_pressed():
    get_tree().change_scene_to_file(scene_to_load)


func _on_exit_pressed():
    get_tree().quit()


func _on_toggle_fullscreen_pressed():
    if DisplayServer.window_get_mode() == DisplayServer.WINDOW_MODE_FULLSCREEN:
        DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_WINDOWED)
    else:
        DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)
