module App 

open Browser
open Browser.Types
open Fable.Core
open Fable.Core.JsInterop

let private classes : CssModules.src.App = import "default" "./App.module.css"

module Router =
    type Route = { path: string; title: string; callback: unit -> unit }
    type Router = { root: string; routes: Route list; globalCallback: string -> unit }
    type HistoryCallback = string -> Route -> unit

    module private StateFns = 
        let  replaceState (root: string) (route: Route) : unit =
            let path = root + route.path
            history.replaceState({| path = path |}, route.title, path)
            route.callback ()
            document.title <- route.title

        let  pushState (root: string) (route: Route) : unit =
            let path = root + route.path
            history.pushState({| path = path |}, route.title, path)
            route.callback ()
            document.title <- route.title

        let  keepState (root: string) (route: Route) : unit =
            route.callback ()
            document.title <- route.title

    module private RouteFns =
        let findRoute (path: string) (router: Router) =
            printfn "findRoute: %s" path
            router.routes 
            |> List.tryFind (fun r -> r.path = path)
            // |> Option.map (fun r ->  { path = router.root + r.path; title = r.title; callback = r.callback })

        let loadRoute (path: string) (historyCallback: HistoryCallback) (router: Router) : unit =
            findRoute path router 
            |> Option.map (fun validRoute -> (historyCallback router.root validRoute); (router.globalCallback validRoute.path))
            |> ignore 

    [<RequireQualifiedAccess>]
    module Navigate =
        let toInitialPath (path: string) (router: Router) =
            router |> RouteFns.loadRoute path StateFns.replaceState

        let toPath (path: string) (router: Router) =
            router |> RouteFns.loadRoute path StateFns.pushState

        let backToPath (path: string) (router: Router) =
            router |> RouteFns.loadRoute path StateFns.keepState

open Router

let changeTitle (title: string) =
    document.title <- title

let defaultTitle = "Fable Experiments"

let router: Router = { 
    root = "/fable-experiments"; 
    routes = [
        { path = "/"; title = defaultTitle + " - Home"; callback = Home.load }
        { path = "/about"; title = defaultTitle + " - About"; callback = About.load }
        { path = "/mdn-canvas-tutorial"; title = defaultTitle + " - Port - MDN Canvas Tutorial"; callback = MDNCanvasTutorial.Index.load }
    ]
    globalCallback = (fun path -> (
                let links = document.getElementsByClassName("app-route")
                for i = 0 to links.length - 1 do 
                    printfn "links: %s" (links.[i].getAttribute("href"))
                    printfn "route: %s" path
                    if links.[i].getAttribute("href") = path then
                        links.[i].classList.add("active")
                    else 
                        links.[i].classList.remove("active")
            )
        )
}

let setupNavigation () =
    let links = document.getElementsByClassName("app-route") 
    for i = 0 to links.length - 1 do
        let link = links.[i] :?> HTMLAnchorElement
        link.addEventListener("click", fun _ ->
            let path = link.getAttribute("href")
            window.event.preventDefault()
            router |> Navigate.toPath path |> ignore
        )

window.addEventListener("popstate", fun _ -> router |> Navigate.backToPath (window.location.pathname.Replace(router.root, "")) |> ignore;)

window.onload <- 
    fun _ ->
        printfn "window.location.pathname: %s" window.location.pathname

        setupNavigation ()

        router 
        |> Navigate.toInitialPath (window.location.pathname.Replace(router.root, ""))
        |> ignore

