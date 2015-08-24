module Land

type Unit = {
    Name : string
    Hp : decimal
}

type Side = {
    Units : Unit list
}

type Encounter = {
    FriendlySide : Side
    HostileSide : Side
}

// phase vs phase