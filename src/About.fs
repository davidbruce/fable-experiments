module About

open Browser
open Browser.Types
open Fable.Core

let load () = 
    let root = document.getElementById("app")
    printfn("at about")
    root.innerHTML <- "About"

