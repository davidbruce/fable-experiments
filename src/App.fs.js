import { Record } from "../fable_modules/fable-library-js.4.18.0/Types.js";
import { list_type, record_type, lambda_type, unit_type, string_type } from "../fable_modules/fable-library-js.4.18.0/Reflection.js";
import { replace, printf, toConsole } from "../fable_modules/fable-library-js.4.18.0/String.js";
import { map } from "../fable_modules/fable-library-js.4.18.0/Option.js";
import { ofArray, tryFind } from "../fable_modules/fable-library-js.4.18.0/List.js";
import { load } from "./About.fs.js";
import { load as load_1 } from "./MDNCanvasTutorial/Index.fs.js";

export class Router_Route extends Record {
    constructor(path, title, callback) {
        super();
        this.path = path;
        this.title = title;
        this.callback = callback;
    }
}

export function Router_Route_$reflection() {
    return record_type("App.Router.Route", [], Router_Route, () => [["path", string_type], ["title", string_type], ["callback", lambda_type(unit_type, unit_type)]]);
}

export class Router_Router extends Record {
    constructor(root, routes) {
        super();
        this.root = root;
        this.routes = routes;
    }
}

export function Router_Router_$reflection() {
    return record_type("App.Router.Router", [], Router_Router, () => [["root", string_type], ["routes", list_type(Router_Route_$reflection())]]);
}

function Router_findRoute(path, router_1) {
    toConsole(printf("findRoute: %s"))(path);
    return map((r_1) => (new Router_Route(router_1.root + r_1.path, r_1.title, r_1.callback)), tryFind((r) => (r.path === path), router_1.routes));
}

function Router_replaceState(route) {
    history.replaceState({
        path: route.path,
    }, route.title, route.path);
    route.callback();
    document.title = route.title;
}

function Router_pushState(route) {
    history.pushState({
        path: route.path,
    }, route.title, route.path);
    route.callback();
    document.title = route.title;
}

function Router_keepState(route) {
    route.callback();
    document.title = route.title;
}

function Router_loadRoute(path, historyFun, router_1) {
    return map(historyFun, Router_findRoute(path, router_1));
}

export function Router_initialRoute(path, router_1) {
    return Router_loadRoute(path, (route) => {
        Router_replaceState(route);
    }, router_1);
}

export function Router_navigateTo(path, router_1) {
    return Router_loadRoute(path, (route) => {
        Router_pushState(route);
    }, router_1);
}

export function Router_backTo(path, router_1) {
    return Router_loadRoute(path, (route) => {
        Router_keepState(route);
    }, router_1);
}

export function changeTitle(title) {
    document.title = title;
}

export const defaultTitle = "Fable Experiments";

export const router = new Router_Router("/fable-experiments", ofArray([new Router_Route("/", defaultTitle + " - Home", () => {
    document.getElementById("app").innerHTML = "Home";
    document.title = "Fable - Home";
}), new Router_Route("/about", defaultTitle + " - About", () => {
    load();
}), new Router_Route("/mdn-canvas-tutorial", defaultTitle + " - Port - MDN Canvas Tutorial", () => {
    load_1();
})]));

export function setupNavigation() {
    const links = document.getElementsByClassName("app-route");
    for (let i = 0; i <= (links.length - 1); i++) {
        const link = links[i];
        link.addEventListener("click", (_arg) => {
            const path = link.getAttribute("href");
            window.event.preventDefault();
            Router_navigateTo(path, router);
        });
    }
}

window.addEventListener("popstate", (_arg) => {
    Router_backTo(replace(window.location.pathname, router.root, ""), router);
});

window.onload = ((_arg) => {
    toConsole(printf("onload"));
    const arg = window.location.pathname;
    toConsole(printf("window.location.pathname: %s"))(arg);
    setupNavigation();
    Router_initialRoute(replace(window.location.pathname, router.root, ""), router);
});

