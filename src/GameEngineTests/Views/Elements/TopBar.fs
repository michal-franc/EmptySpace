module TopBar

open libtcod

let topBarText text =
    TCODConsole.root.setBackgroundColor(TCODColor.grey);
    TCODConsole.root.rect(0, 0, 50, 3, true, TCODBackgroundFlag.Set);
    TCODConsole.root.print(1, 1, text);
    TCODConsole.root.setBackgroundColor(TCODColor.black);