﻿open System
open GameState

// In basic form, to start mission you select where and how many time then you need to provide resources
// you cant start mission with not enough food, water and some equipment
// at the end mission has different outcomes
// that would be - First iteration of missions
// but we need mission state etc

// Explore mission
// So I can go to Planet / Space View ( wont start doing this now )
// On Planet i can go to explore tab.
// this will give me information if exploration is possible and how many things are
// there to explore. Also there will be info about risk planet kind etc
// Also there will be a list of ongoing missions for this planet 
// ( you can cancel them etc)
// on this screen i will be able to start new mission 
// this will then move to liust of people i can select
// on this list i can have select which people i want to send - explorers? free people ?
// later on after we found something we might have to send someone else ?

// each person has values
// ID - used to identify person
// Health 
// Happines
// Fatigue

// Each person has many needs
// we calcualte all the needs with crew member guid or id 
// then an engine runs and tries to fulfill those needs
// all the unfulfiled needs generate some negative effect

// On Ship people can have one selected primary profession 
// Must have for now
// - biotanist - creates food on ship
//    - we might extend this to cooks + different specialisations
// - engineer - maintains ship
//    - couild extend it to different areas like engine, shield, weapons, repair
// - officer - maintains different areas of the ship influencing everyone + reports
//    - different specialisations 
// - doctor - heals people
//    - different specialisatonn + nursery etc
// - soldier - different skillset
//    - specs - marine
//    - weapon specialist 


[<EntryPoint>]
let main argv = 
    let rec mainLoop(gamestate) = 
        let _gamestate = ViewHandler.handle gamestate
        mainLoop(_gamestate)    
    mainLoop(initialGameState)     
    0
