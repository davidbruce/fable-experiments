import { printf, toConsole } from "../../fable_modules/fable-library-js.4.18.0/String.js";

export function load() {
    const root = document.getElementById("app");
    toConsole(printf("at mdn-canvas-tutorial"));
    root.innerHTML = "MDN Canvas Tutorial";
}

