extends Node


func _on_timer_timeout():
    $KeyUp.visible = not $KeyUp.visible
    $KeyDown.visible = not $KeyDown.visible
