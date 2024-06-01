import { Record } from "../fable_modules/fable-library-js.4.18.0/Types.js";
import { record_type, lambda_type, unit_type, string_type } from "../fable_modules/fable-library-js.4.18.0/Reflection.js";
import { ofArray, tryFind } from "../fable_modules/fable-library-js.4.18.0/List.js";
import { map } from "../fable_modules/fable-library-js.4.18.0/Option.js";
import { load } from "./About.fs.js";
import { printf, toConsole } from "../fable_modules/fable-library-js.4.18.0/String.js";

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

function Router_findRoute(path, router_1) {
    return tryFind((r) => (r.path === path), router_1);
}

function Router_replaceState(route) {
    history.replaceState({
        path: route.path,
    }, route.title, route.path);
    route.callback();
}

function Router_pushState(route) {
    history.pushState({
        path: route.path,
    }, route.title, route.path);
    route.callback();
}

function Router_keepState(route) {
    route.callback();
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

export const router = ofArray([new Router_Route("/", "Home", () => {
    document.getElementById("app").innerHTML = "Home";
    document.title = "Fable - Home";
}), new Router_Route("/about", "About", () => {
    load();
}), new Router_Route("/contact", "Contact", () => {
    toConsole(printf("/contact"));
    changeTitle("Fable - Contact");
})]);

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
    Router_backTo(window.location.pathname, router);
});

window.onload = ((_arg) => {
    toConsole(printf("onload"));
    setupNavigation();
    Router_initialRoute(window.location.pathname, router);
});

