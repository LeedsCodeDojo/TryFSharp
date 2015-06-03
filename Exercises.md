Try F#
======

If you've never used anything like F#, you might want to go through the [Gentle Introduction](https://github.com/LeedsCodeDojo/TryFSharp/blob/master/Gentle%20Introduction.fsx) to get yourself up to speed.

If you don't know how to do something, check out the [F# Cheat Sheet](http://dungpa.github.io/fsharp-cheatsheet/).

In this set of exercises, we are going to use F# to draw 3D shapes, starting with a simple cube and ending up reading a json feed and drawing the shapes specified in there.  Along the way we'll see how to define and pass functions as parameters, the pipe operator, type inference, tuples, define discriminated unions, pattern match, use higher order functions, compose functions and use type providers.

Functions to draw shapes on screen are provided and are TryFs.cuboid, TryFs.cube, TryFs.cone and TryFs.cylinder.

## 1 Define a Function ##

Open the Script.fsx file, which you will modify.  Run the initial lines into F# Interactive (by selecting them and pressing Alt+Enter).

There is a utility module referenced called TryFs which contains some handy functions.  For example, there are a few functions like this which can be used to create shapes:

``` fsharp
TryFs.cylinder 0.0 0.0 0.0 1.0 2.0 Color.Green
```

You can find out what the parameters are using intellisense or by hovering the mouse over the function.

Your first task is to define a new function called draw(), it should have no parameters and draw a red cube using one of the utility functions similar to the one above.  To show a shape a Drawing3D object should be passed into the TryFs.showIt function - this is what is created when you call TryFs.cylinder and the like.

Remember:
* If a function has no parameters, it needs parentheses (both in the definition and when you call it)
* When you pass your TryFs.cube function to showIt, you'll need to wrap it in parenthesis due to the strict left-to-right compilation

## 2 Pipe Operator ##

The F# forward pipe operator |> passes the value on its left to the function on its right.  So for example instead of
``` fsharp
add 5 10
```
you can do
``` fsharp
10 |> add 5
```

Edit your draw() function to change the way the shape is passed to the showIt function to use the |> operator.

## 3 Add Function Parameters ##

Now add x, y and z parameters to the draw function to define the position of the cube on the screen and pass them to TryFs.cube as the first three parameters.  You shouldn't need to give the parameters types in the definition as type inference will work out that they are floats from their usage. 

Note that coordinates 0.0, 0.0, 0.0 is the centre of the window.

## 4 Define a Discriminated Union ##

An F# discriminated union is similar to an enum in C# and Java in that it defines a set of named constants.

Define a discriminated union type called Shape and is made up of the shapes we are able to draw - Cuboid, Cube, Cone and Cylinder.

## 5 Pattern Matching ##

Pattern matching in F# can be done using the match..with expression - for instance, this will print some text to the console depending on the value of an int:

``` fsharp
match num with
| 3 -> printf "num is 3"
| 8 -> printf "num is 8"
| x -> printf "num is something else: %i" x
```

Add another parameter to your draw function - this one will define which shape is going to be drawn and will be the discriminated union type defined above.

Add a simple pattern match for each type of shape and draw the correct one.

Remember that your Discriminated Union needs to be declared above where it's used in the file.

## 6 Add values to the Discriminated Union ##

Unlike enums a discriminated union can have types associated with it.  Our Cylinder needs two parameters for its dimension - height and width - so this would look like this:

``` fsharp
Cylinder of float * float
```

Add dimensions for each shape to the discriminated union - then the shapes can be created with their dimensions, the Cylinder as defined above can be created like this:

``` fsharp
Cylinder (3.0, 1.0)
```
and use the values in the pattern match to draw shapes with the dimensions that were passed in.

## 7 Add a record type ##

Add a record type that defines the shape, the coordinates and a colour, such as 'Object3D'.  Make the coordinates a tuple of float * float * float.  Replace the current parameters of the draw function with a single one of this record type.

As a reminder, a record is defined and created like this:
``` fsharp
type myRecord = { Name: string; Age: int }
let jim = {Name = "Jim"; Age = 34}
```

## 8 Draw a list of shapes ##

Change the draw function to take a list of shape definition records and show them all.  To do this rather than passing each individual Drawing3D instance to the TryFs.showIt function you will need to pass a list of Drawing3D instances to the TryFs.showEm function.  Take a look at the List.map function to do this.

## 9 Draw a tower ##

You should now be able to use your draw function and new-found F# skills to use a cylinder and a cone to draw a tower on the screen.

## 10 Generate a sequence of the same shape ##

Now try creating a function that will take a shape record and return a list of the same shape to display at different screen coordinates.  Rather than creating new shape records from scratch you should be able to use the with keyword to create the new shape records.

The with keyword works like this:

``` fsharp
type myRecord = { Name: string; Age: int }

let jim = {Name = "Jim"; Age = 34}

let olderJim = {jim with Age = 44}
```

In the above example olderJim.Name is Jim as it has been copied but olderJim.Age is updated to 44.

## 11 Draw some cool stuff ##

Using the code you've written, see what interesting things you can draw!

You could try things like:
* Repeating patterns of objects
* Taking existing objects and modifying them

## 12 Read shapes from json feed ##

Use the [json type provider](http://fsharp.github.io/FSharp.Data/library/JsonProvider.html) to read shape definitions from the shapes.json file and create a list of shape records to pass into the draw function.

The records can be loaded from the json file like this

``` fsharp
type Shapes = JsonProvider<"shapes.json">

let shapesList = Shapes.Load("shapes.json")
```

shapesList will then be a sequence of records and you can use intellisense to see the fields that are available.



