import { h } from "dom-chef";
import { map, toArray, ofArray } from "../../fable_modules/fable-library-js.4.18.0/List.js";
import { printf, toConsole } from "../../fable_modules/fable-library-js.4.18.0/String.js";


export const name = "Phillip";

export const age = 30;

export const things = ofArray(["Hello, world!", "Using Dom-chef + F#"]);

export function Thing() {
    return         <div>
            {toArray(map((x_1) => <span>{x_1}</span>, things))}
        </div>
        ;
}

export function Thing2() {
    return <h1 class="Heading">Hello Thing 2</h1>;
}

export function Thing3() {
    const title = document.createElement("h1");
    title.classList.add("Heading");
    title.innerHTML = "Hello Thing 3";
    return title;
}

export function handleClick(e) {
    toConsole(printf("clicked"));
}

export const x =         <div>
            <span onClick={(e) => {
    handleClick(e);
}}>Click Me!</span>
            <br/>
            Name: {name}, Age: {age}
            <Thing/>
            <Thing2/>
            <Thing3/>
            <br/>
            <h1>Hello, world!</h1>
            <p>Using Dom-chef + F#</p>
        </div>
        ;

export function load() {
    const root = document.getElementById("app");
    toConsole(printf("at mdn-canvas-tutorial"));
    root.appendChild(x);
}

