#r "references/OpenTK.dll"
#r "references/OpenTK.GLControl.dll"
#r "references/FSharp.Data.dll"
#load "functional3d.fs"
#load "TryFsharpUtilities.fs"

open Functional3D
open TryFsharpUtilities
open System.Drawing
open FSharp.Data

type Shape =
    | Cuboid of float * float * float
    | Cube of float
    | Cone of float * float
    | Cylinder of float * float

type ShapeDef =
    {
        Shape : Shape
        Coords : (float * float * float) option
        Colour : Color
    }

let draw coords shapeDefs = 
    let getDrawing shapeDef coords =
        let x, y, z = shapeDef.Coords |> coords
        match shapeDef.Shape with
            | Cuboid (width, height, depth) -> TryFs.cuboid x y z width height depth shapeDef.Colour
            | Cube height -> TryFs.cube x y z height shapeDef.Colour
            | Cone (width, height) -> TryFs.cone x y z height width shapeDef.Colour
            | Cylinder (width, height) -> TryFs.cylinder x y z 3. 1. shapeDef.Colour

    shapeDefs |> List.map(fun s -> getDrawing s coords) |> TryFs.showEm

let defaultCoords c = match c with | None -> (0., 0., 0.) | Some(xyz) -> xyz
let moveDiagonal c = let x, y, z = c
                     (x - 0.5, y - 0.5, z)

type Shapes = JsonProvider<"shapes.json">
let shapesList = Shapes.Load("shapes.json")

shapesList |> Seq.map(fun s -> match s.Shape with
                                | "Cylinder" -> {Shape = Cylinder(float s.Height, float s.Width); Coords = Some(float s.X, float s.Y, float s.Z); Colour = ColorTranslator.FromHtml(s.Colour)}
                                | "Cone" -> {Shape = Cone(float s.Height, float s.Width); Coords = Some(float s.X, float s.Y, float s.Z); Colour = ColorTranslator.FromHtml(s.Colour)}
                                | "Cuboid" -> {Shape = Cuboid(float s.Height, float s.Width, float s.Depth); Coords = Some(float s.X, float s.Y, float s.Z);Colour = ColorTranslator.FromHtml(s.Colour)}
                                | _ -> {Shape = Cube 0.; Coords = None; Colour = Color.Red})
           |> Seq.toList
           |> draw defaultCoords
     