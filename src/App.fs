module App 

open Browser
open Browser.Types
open Fable.Core
open Fable.Core.JsInterop

import "h" "dom-chef"

module Router =
    type Route = { path: string; title: string; callback: unit -> unit }
    type Router = { root: string; routes: Route list }

    let private findRoute (path: string) (router: Router) =
        printfn "findRoute: %s" path
        router.routes 
        |> List.tryFind (fun r -> r.path = path)
        |> Option.map (fun r ->  { path = router.root + r.path; title = r.title; callback = r.callback })

    let private replaceState (route: Route) =
        history.replaceState({| path = route.path |}, route.title, route.path)
        route.callback ()
        document.title <- route.title

    let private pushState (route: Route) =
        history.pushState({| path = route.path |}, route.title, route.path)
        route.callback ()
        document.title <- route.title

    let private keepState (route: Route) =
        route.callback ()
        document.title <- route.title

    let private loadRoute (path: string) (historyFun: Route -> unit) (router: Router) =
        findRoute path router 
        |> Option.map historyFun 

    let initialRoute (path: string) (router: Router) =
        router |> loadRoute path replaceState

    let navigateTo (path: string) (router: Router) =
        router |> loadRoute path pushState

    let backTo (path: string) (router: Router) =
        router |> loadRoute path keepState

open Router

let changeTitle (title: string) =
    document.title <- title

let defaultTitle = "Fable Experiments"


let html = 
    JSX.html 
        $"""
            <div>David Bruce's site for experimenting with F# and Fable</div>
        """

let router: Router = { 
    root = "/fable-experiments"; 
    routes = [
        { path = "/"; title = defaultTitle + " - Home"; callback = (fun _ -> 
                printfn "clearing"
                let root = document.getElementById("app")
                root.innerHTML <- "" 
                root.appendChild(html :> obj :?> HTMLElement) |> ignore
                document.title <- "Fable - Home"
            ) 
        }
        { path = "/about"; title = defaultTitle + " - About"; callback = About.load }
        { path = "/mdn-canvas-tutorial"; title = defaultTitle + " - Port - MDN Canvas Tutorial"; callback = MDNCanvasTutorial.Index.load }
    ]
}

let setupNavigation () =
    let links = document.getElementsByClassName("app-route") 
    for i = 0 to links.length - 1 do
        let link = links.[i] :?> HTMLAnchorElement
        link.addEventListener("click", fun _ ->
            let path = link.getAttribute("href")
            window.event.preventDefault()
            router |> navigateTo path |> ignore
        )

window.addEventListener("popstate", fun _ -> router |> backTo (window.location.pathname.Replace(router.root, "")) |> ignore;)

window.onload <- 
    fun _ ->
        printfn "window.location.pathname: %s" window.location.pathname

        setupNavigation ()

        router 
        |> initialRoute (window.location.pathname.Replace(router.root, ""))
        |> ignore

