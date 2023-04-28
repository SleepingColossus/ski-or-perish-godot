extends HBoxContainer


func set_health(health: int):
    match health:
        0:
            $HeartIndicator1.turn_off()
            $HeartIndicator2.turn_off()
            $HeartIndicator3.turn_off()
        1:
            $HeartIndicator1.turn_on()
            $HeartIndicator2.turn_off()
            $HeartIndicator3.turn_off()
        2:
            $HeartIndicator1.turn_on()
            $HeartIndicator2.turn_on()
            $HeartIndicator3.turn_off()
        _:
            $HeartIndicator1.turn_on()
            $HeartIndicator2.turn_on()
            $HeartIndicator3.turn_on()
