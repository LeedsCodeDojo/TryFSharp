#r "references/OpenTK.dll"
#r "references/OpenTK.GLControl.dll"
#r "references/FSharp.Data.dll"
#load "functional3d.fs"
#load "TryFsharpUtilities.fs"

open Functional3D
open TryFsharpUtilities
open System.Drawing
open FSharp.Data
open TryFs

// TryFs.cylinder 0.0 0.0 0.0 1.0 2.0 Color.Green

type Shape =
    | Cuboid of float * float * float
    | Cube of float
    | Cone of float * float
    | Cylinder of float * float

type ShapeToDraw = { Shape: Shape; x: float; y: float; z: float; Colour: Color}

let shapeToDrawToDrawing3d s = 
    match s.Shape with
    | Cuboid(w,h,d) -> TryFs.cuboid s.x s.y s.z w h d s.Colour
    | Cube(w) -> TryFs.cube s.x s.y s.z w s.Colour
    | Cone(w,h) -> TryFs.cone s.x s.y s.z w h s.Colour
    | Cylinder (w,h) -> TryFs.cylinder s.x s.y s.z w h s.Colour
    
let draw shapes =
    shapes
    |> List.map shapeToDrawToDrawing3d
    |> TryFs.showEm
    
//let generateCloneShapes s = 
//    let s2 = {s with x=4.0; Colour = Color.Red}
//    [s;s2] 


let f = 
  [
    {Shape= (Cuboid(1.0,1.0,4.0)) ; x=(-3.0); y=(0.0) ;z=(0.0); Colour=Color.Blue};
    {Shape= (Cuboid(1.0,1.0,1.0)) ; x=(-2.0); y=(0.0) ;z=(-1.5); Colour=Color.Blue}
    {Shape= (Cuboid(1.0,1.0,1.0)) ; x=(-2.0); y=(0.0) ;z=(0.3); Colour=Color.Blue}
  ]

let hash = 
  [
    {Shape= (Cuboid(0.7,0.7,3.0)) ; x=(0.5); y=(0.0) ;z=(0.0); Colour=Color.Blue};
    {Shape= (Cuboid(0.7,0.7,3.0)) ; x=(2.0); y=(0.0) ;z=(0.0); Colour=Color.Blue};
    {Shape= (Cuboid(3.0,0.7,0.7)) ; x=(0.3); y=(0.0) ;z=(0.7); Colour=Color.Blue};
    {Shape= (Cuboid(3.0,0.7,0.7)) ; x=(0.3); y=(0.0) ;z=(-0.7); Colour=Color.Blue};
  ]

(f@hash) |> draw
