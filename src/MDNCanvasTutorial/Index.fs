module MDNCanvasTutorial.Index

open Browser
open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Highligher

let name = "Phillip"
let age = 30
let things = ["Hello, world!"; "Using Dom-chef + F#"]


let thing () = 
    html 
        $"""
        <div>
            {things 
                |> List.map (fun x -> html $"<span>{x}</span>") 
                |> List.toArray 
            }
        </div>
        """

let handleClick e = 
    printfn "clicked"

let renderCanvas () =
    let canvas = document.getElementById("tutorial") :?> HTMLCanvasElement
    let ctx = canvas.getContext_2d()

    ctx.fillStyle <- U3.Case1 ("rgb(200 0 0)")
    ctx.fillRect(10, 10, 50, 50)

    ctx.fillStyle <- U3.Case1 ("rgb(0 0 200 / 50%)")
    ctx.fillRect(30, 30, 50, 50)

let template () =
    html
        $""" 
        <article>
            <button type="button">Click Me!</button>
            {thing ()}
            <canvas id="tutorial" width="150" height="150">
                Cavnas tutorial element
            </canvas>
        </article>
        """
    , 
    (fun () -> 
        let button = document.querySelector("button")
        button.addEventListener("click", handleClick)
        renderCanvas()
    )

// open BasicUsage
let load () = 
    let root = document.getElementById("app")
    printfn("at mdn-canvas-tutorial")
    root.innerHTML <- "" 
    let (markup, script) = template ()
    root.innerHTML <- markup 
    script ()
