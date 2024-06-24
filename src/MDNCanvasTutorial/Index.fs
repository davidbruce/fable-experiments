module MDNCanvasTutorial.Index

open Browser
open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Highligher

let name = "Phillip"
let age = 30
let things = [|"Hello, world!"; "Using F# string interpolation"|]

let thing () = 
    html 
        $"""
        <div>
            {things 
                |> Array.map (fun x -> html $"<span>{x}</span>") 
                |> String.concat "<br/>"
            }
        </div>
        """

let handleClick e = 
    printfn "clicked"

let renderCanvas (canvas: HTMLCanvasElement) (ctx: CanvasRenderingContext2D) =
    ctx.fillStyle <- U3.Case1 ("rgb(200 0 0)")
    ctx.fillRect(10, 10, 50, 50)

    ctx.fillStyle <- U3.Case1 ("rgb(0 0 200 / 50%)")
    ctx.fillRect(30, 30, 50, 50)

// This returns a tuple of string and unit function
// This is for the purposes of wiring up event listeners or any of scripts
// that are relevant to the html markup 
let template (id: string) (renderFunc: HTMLCanvasElement -> CanvasRenderingContext2D -> unit) =
    html
        $""" 
        <article>
            <canvas id="{id}" width="150" height="150">
                Cavnas tutorial element
            </canvas>
        </article>
        """
    , 
    (fun () -> 
        let canvas = document.getElementById(id) :?> HTMLCanvasElement
        let ctx = canvas.getContext_2d()
        renderFunc canvas ctx
    )

let createElement (root: HTMLElement) (markup: string) = 
    let element = document.createElement("div")
    element.id <- "test"
    element.innerHTML <- markup
    element

// open BasicUsage
let load () = 
    let root = document.getElementById("app")
    printfn("at mdn-canvas-tutorial")
    root.innerHTML <- "" 

    let (basicUsage, basicUsageInit) = template "basic-usage" renderCanvas

    root.appendChild(createElement root basicUsage) |> ignore
    basicUsageInit ()

    let (drawingRecetangles, drawingRectanglesInit) = template "drawing-rectangles" DrawingShapes.DrawingRectangles.renderCanvas 

    root.appendChild(createElement root drawingRecetangles) |> ignore
    drawingRectanglesInit ()

    let (drawingPaths, drawingPathsInit) = template "drawing-paths" DrawingShapes.DrawingPaths.renderCanvas 

    root.appendChild(createElement root drawingPaths) |> ignore
    drawingPathsInit ()





