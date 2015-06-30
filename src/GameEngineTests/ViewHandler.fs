module ViewHandler

open IView
open MainMenuView
open GameState
open System
open ViewType
open ListView
open SystemView
open MissionView
open ShipView
open CrewView
open StorageView
open LocationView

let private ViewCollection:Collections.Generic.IDictionary<ViewType, IView> = dict [
                                    (Menu, new MainMenuView() :> IView);
                                    (Missions, new MissionsView() :> IView);
                                    (Ship, new ShipView() :> IView);
                                    (Crew, new CrewView() :> IView);
                                    (Storage, new StorageView() :> IView);
                                    (List, new ListView() :> IView);
                                    (System, new SystemView() :> IView);
                                    (Location, new LocationView() :> IView);
                                  ]

let handle state = 
    let _gamestate = ViewCollection.[state.CurrentView].innerLoop state
    _gamestate