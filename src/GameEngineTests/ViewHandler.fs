module ViewHandler

open IView
open MainMenuView
open GameState
open System
open ViewType

let private ViewCollection:Collections.Generic.IDictionary<ViewType, IView> = dict [
                                    (Menu, new MainMenuView() :> IView);
                                    (Missions, new MissionsView() :> IView);
                                    (Ship, new ShipView() :> IView);
                                    (Crew, new CrewView() :> IView);
                                    (Storage, new StorageView() :> IView);
                                    (Explore, new ExploreView() :> IView);
                                  ]

let handle state = 
        Console.Clear()
        let _gamestate = ViewCollection.[state.CurrentView].innerLoop state
        _gamestate