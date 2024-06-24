module MDNCanvasTutorial.DrawingShapes

open Browser
open Browser.Types
open Fable.Core
open Highligher


module DrawingRectangles =
    let renderCanvas (canvas: HTMLCanvasElement) (ctx: CanvasRenderingContext2D) =
        ctx.fillRect(25, 25, 100, 100)
        ctx.clearRect(45, 45, 60, 60)
        ctx.strokeRect(50, 50, 50, 50)

module DrawingPaths = 
    let renderCanvas (canvas: HTMLCanvasElement) (ctx: CanvasRenderingContext2D) =
        ctx.beginPath()
        ctx.moveTo(75, 50)
        ctx.lineTo(100, 75)
        ctx.lineTo(100, 25)
        ctx.fill()