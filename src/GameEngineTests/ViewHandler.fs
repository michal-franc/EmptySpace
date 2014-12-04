﻿module ViewHandler

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

let keyHandle state key = 
    ViewCollection.[state.CurrentView].HandleKeys key state

let getView state =
    ViewCollection.[state.CurrentView].GetView state