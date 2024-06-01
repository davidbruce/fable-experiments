module MDNCanvasTutorial.Index

open Browser
open Browser.Types
open Fable.Core


let load () = 
    let root = document.getElementById("app")
    printfn("at mdn-canvas-tutorial")
    root.innerHTML <- "MDN Canvas Tutorial"
