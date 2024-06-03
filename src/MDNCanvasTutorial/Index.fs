module MDNCanvasTutorial.Index

open Browser
open Browser.Types
open Fable.Core
open Fable.Core.JsInterop

//JSX renderer
import "h" "dom-chef"

let name = "Phillip"
let age = 30
let things = ["Hello, world!"; "Using Dom-chef + F#"]

let Thing () =
    JSX.html 
        $"""
        <div>
            {things 
                |> List.map (fun x -> JSX.html $"<span>{x}</span>") 
                |> List.toArray 
            }
        </div>
        """

let Thing2 () = 
    JSX.html $"""<h1 class="Heading">Hello Thing 2</h1>"""

let Thing3 () =
    let title = document.createElement("h1")
    title.classList.add("Heading")
    title.innerHTML <- "Hello Thing 3"
    title

let handleClick e = 
    printfn "clicked"

let x =
    JSX.html
        $""" 
        <div>
            <span onClick={handleClick}>Click Me!</span>
            <br/>
            Name: {name}, Age: {age}
            <Thing/>
            <Thing2/>
            <Thing3/>
            <br/>
            <h1>Hello, world!</h1>
            <p>Using Dom-chef + F#</p>
        </div>
        """

let load () = 
    let root = document.getElementById("app")
    printfn("at mdn-canvas-tutorial")
    root.appendChild(x :> obj :?> HTMLElement) |> ignore
