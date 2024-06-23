module Home 

open Browser
open Highligher


let template () =
    html $"""
        <div>
            <my-button>Hello</my-button>
        </div>
    """
    

let load () = 
    let root = document.getElementById("app")
    printfn("at mdn-canvas-tutorial")
    root.innerHTML <- "" 
    root.innerHTML <- template ()
    // root.appendChild() |> ignore
