extends Control


func turn_on():
    $On.visible = true
    $Off.visible = false


func turn_off():
    $On.visible = false
    $Off.visible = true
