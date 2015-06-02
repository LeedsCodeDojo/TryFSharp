// F# Language Demo
// ================

// ** run code interactively in the REPL **
let isEven num = 
    num % 2 = 0
// ^ not much syntax!

// ** immutable by default**
let x = 10
x <- 20

// ** type inference **
let add x y =
    x + y

add 5 7

// ** currying & partial application **
let add10 = add 10

add10 5

// ** pipe operator **

// old way
List.sum (List.map add10 (List.filter isEven [1..10]))

// better way
[1..10]
|> List.filter isEven
|> List.map add10
|> List.sum

// ** Discriminated Unions **
type Shape =
    | Circle
    | Rectangle of int * int
    | Square of int

// ** pattern matching **
let printShape shape =
    match shape with
    | Circle -> printfn "I'm a circle"
    | Rectangle(width, height) -> printfn "I'm a rectangle of size %ix%i" width height
    | Square(0) -> printfn "I'm a really small square"
    | Square(x) when x > 100 -> printfn "I'm a really big square"
    | Square(_) -> printfn "I'm some other square"

let myTuple = (1, 2, 3)

let x, y, z = myTuple

// F# Nice Features
// ================

// ** units of measure **
[<Measure>] type mile
[<Measure>] type hour

let distanceTravelled = 200<mile>
let timeTaken = 3<hour>

let speed = distanceTravelled / timeTaken

let area = 5<mile> * 3<mile>

// ** Type Providers **

// http://data.worldbank.org/developers/data-catalog-api

#r @".\packages\FSharp.Data.2.2.2\lib\net40\FSharp.Data.dll"
open FSharp.Data

let wb = WorldBankData.GetDataContext()

wb
  .Countries.``United Kingdom``
  .Indicators.``School enrollment, tertiary (% gross)``
|> Seq.maxBy fst

// ** interactive charting **

#load "packages/FSharp.Charting.0.82/FSharp.Charting.fsx"
open FSharp.Charting

wb.Countries.Albania.CapitalCity

wb.Countries.``United Kingdom``
    .Indicators.``School enrollment, tertiary (% gross)``
|> Chart.Line

let countries = 
 [| wb.Countries.Australia
    wb.Countries.``United Kingdom``
    wb.Countries.``United States`` |]

[ for c in countries ->
    c.Indicators.``School enrollment, tertiary (% gross)`` ]
|> List.map Chart.Line
|> Chart.Combine

// Backup charting

Chart.Pie( [(10,5);(7,3);(19,6)], Labels=["Europe";"USA";"Asia"])

// set up some data
let futureDate numDays = System.DateTime.Today.AddDays(float numDays)
let rnd = System.Random()
let rand() = rnd.NextDouble()

let expectedIncome = [ for x in 1 .. 100 -> (futureDate x, 1000.0 + rand() * 100.0 * exp (float x / 40.0) ) ]
let expectedExpenses = [ for x in 1 .. 100 -> (futureDate x, rand() * 500.0 * sin (float x / 50.0) ) ]
let computedProfit = (expectedIncome, expectedExpenses) ||> List.map2 (fun (d1,i) (d2,e) -> (d1, i - e))

// show it!
Chart.Combine(
   [ Chart.Line(expectedIncome,Name="Income")
     Chart.Line(expectedExpenses,Name="Expenses") 
     Chart.Line(computedProfit,Name="Profit") ])
