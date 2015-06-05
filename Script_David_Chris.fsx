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

type corrds = CsvProvider<"C:\\Users\\Chris\\Desktop\\postcode-outcodes.csv">

type population = CsvProvider<"C:\\Users\\Chris\\Desktop\\population.csv">

//let file = corrds.Load("C:\\Users\\Chris\\Desktop\\postcode-outcodes.csv")
let file = corrds.Load("http://www.freemaptools.com/download/outcode-postcodes/postcode-outcodes.csv")
let populationFile = population.Load("C:\\Users\\Chris\\Desktop\\population.csv")


            // lat goes from 49 to 60
            // long goes from -7.8 to 1.7  

let colourFromPostCode postCode =
    let population = 
        populationFile.Rows 
            |> Seq.filter (fun row -> row.``Postcode District`` = postCode)
            |> Seq.sumBy (fun row -> row.``All usual residents``)
    match population with
        | 0 -> Color.Green
        | p when p < 1000 -> Color.Orange
        | p when p < 5000 -> Color.DarkOrange
        | p when p < 10000 -> Color.Red
        | p when p < 20000 -> Color.DarkRed
        | _ -> Color.Blue

let latInRange lat = 
    0.-(lat - 53.0)

let lonInRange lon =
    lon + 2.5

let toPlot = file.Rows 
                |> Seq.map (fun row -> (row.Outcode, float row.Latitude, float row.Longitude))
                |> Seq.map (fun (postcode, lat,lon) -> (colourFromPostCode postcode, latInRange lat, lonInRange lon))
                |> Seq.map (fun (colour, lat,lon) -> cube lon lat 1.0 1.0 colour )
                |> Seq.toList
                |> TryFs.showEm