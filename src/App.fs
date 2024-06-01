module App 

open Browser
open Browser.Types

module Router =
    type Route = { path: string; title: string; callback: unit -> unit }
    type Router = Route list

    let private findRoute (path: string) (router: Router) =
        router |> List.tryFind (fun r -> r.path = path)

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

let router: Router = [
    { path = "/"; title = defaultTitle + " - Home"; callback = fun _ -> document.getElementById("app").innerHTML <- "Home"; document.title <- "Fable - Home" }
    { path = "/about"; title = defaultTitle + " - About"; callback = About.load }
    { path = "/mdn-canvas-tutorial"; title = defaultTitle + " - Port - MDN Canvas Tutorial"; callback = MDNCanvasTutorial.Index.load }
]

let setupNavigation () =
    let links = document.getElementsByClassName("app-route") 
    for i = 0 to links.length - 1 do
        let link = links.[i] :?> HTMLAnchorElement
        link.addEventListener("click", fun _ ->
            let path = link.getAttribute("href")
            window.event.preventDefault()
            router |> navigateTo path |> ignore
        )

window.addEventListener("popstate", fun _ -> router |> backTo window.location.pathname |> ignore;)

window.onload <- 
    fun _ ->
        //change this to be somekind of compiletime change
        let appBaseUrl = document.getElementById("app-base")
        printfn "%s" window.location.hostname
        if window.location.hostname = "localhost" 
            then appBaseUrl.setAttribute("href", "http://localhost:5173")
            else appBaseUrl.setAttribute("href", "https://davidbruce.fable-experiments.github.io")
        
        printfn "onload"
        setupNavigation ()
        router 
        |> initialRoute window.location.pathname 
        |> ignore

