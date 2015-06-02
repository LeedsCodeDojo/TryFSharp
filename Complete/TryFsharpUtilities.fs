module TryFsharpUtilities

open Functional3D
open System.Drawing

module TryFs =
    let showIt shapeFun = shapeFun()

    let rec showEm shapes = 
        match shapes with
            | h::t when t.Length = 0 -> showIt h
            | h::t -> (showIt h) $ (showEm t)

    let cuboid x y z length height depth colour =
        fun () -> 
                    Fun.cube
                        |> Fun.color colour
                        |> Fun.translate (x, y, z)
                        |> Fun.scale (length, height, depth)
                        |> Fun.rotate (90., 0., 0.)

    let cube x y z height colour = cuboid x y z height height height colour

    let cylinder x y z height radius colour =
        fun () -> Fun.cylinder
                    |> Fun.scale (radius, radius, height)
                    |> Fun.color colour
                    |> Fun.translate (x, y, z)
                    |> Fun.rotate (90., 0., 0.)

    let cone x y z height radius colour =
        fun () -> Fun.cone
                    |> Fun.scale (radius, radius, height)
                    |> Fun.color colour
                    |> Fun.translate (x, y, z)
                    |> Fun.rotate (90., 0., 0.)
