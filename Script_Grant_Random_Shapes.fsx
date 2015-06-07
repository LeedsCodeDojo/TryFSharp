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

type Shape =
    | Cube of float
    | Cuboid of float * float * float
    | Cone of float * float
    | Cylinder of float * float

type Object3D = { shape:Shape; coords:float * float * float; colour:Color }

let draw objects = 
    
    objects
    |> List.map(fun object3D ->
        let x,y,z = object3D.coords
        let colour = object3D.colour

        match object3D.shape with
        | Cube(size) -> TryFs.cube x y z size colour
        | Cuboid(length, height, depth) -> TryFs.cuboid x y z length height depth colour
        | Cylinder(height, radius) -> TryFs.cylinder x y z height radius colour
        | Cone(height, radius) -> TryFs.cone x y z height radius colour)
    |> showEm

let rand = System.Random()
let nextRand(limit) = rand.Next(limit)
let randomColour() = 
    Color.FromArgb(nextRand(255), nextRand(255), nextRand(255))

let randomShape() = 
    match nextRand(4) with
    | 0 -> {shape=Cone(0.3,0.3); coords=0.,0.,0.; colour = Color.Purple }
    | 1 -> {shape=Cylinder(0.4,0.3); coords=0.,0.,0.; colour = Color.Purple }
    | 2 -> {shape=Cube(0.2); coords=0.,0.,0.; colour = Color.Purple }
    | _ -> {shape=Cuboid(0.2,0.1,0.3); coords=0.,0.,0.; colour = Color.Purple }

let randomShapes scope =
    [for x = -scope to scope do
        for y = -scope to scope do
            for z = -scope to scope do
                yield {randomShape() with coords = x,y,z; colour=randomColour() }]
    |> draw

randomShapes 6.0
