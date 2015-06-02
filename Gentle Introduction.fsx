// F# Recap
//
// Work through this to introduce yourself to F# if you've not done anything similar before

(*** Running the Code ***)

// This is a script.  It's not an executable program.  To run some (or all) of it
// in the F# Interactive (FSI) window, highlight it and press ALT-ENTER

// You can also type code straight into FSI.  Just remember to finish the line with ;;.


(*** Working with Values ***)

// Define a value with 'let'
let i = 10

// YOUR TURN - Define a value:

// Types get inferred, like with 'var' in C#.  Hover your mouse over the symbol to see 
// the type (or run it in FSI)
let f = 2.5
let b = 13I

// YOUR TURN - find out what type b is

// You can't (by default) change a value once it's been set!
s <- "hi dad"

// There's NO automatic type casting!
let (f2:float) = 5

// But there are some handy conversion functions, with the same names as the types
let (f3:float) = float 5

// YOUR TURN - create a float, and cast it to an int

(*** Functions ***)

// Also declare functions using 'let'
let square x = 
    x * x

// We don't use braces.  Indentation is used to define scope.

// YOUR TURN - create a function to add two numbers.
// To test it, highlight it and load it into FSI, and you can call it from there like 'add 5 7;;'

// Functions with no parameters need parentheses:
let doStuff() = printfn "hi mum"
doStuff()

(*** Printing ***)

// printfn lets you print stuff.
printfn "Integer: %i, Float: %f, Anything: %A" 5 9.9 [1..5]

(*** Tuples ***)

// Tuples let you store things in groups
let myTuple = (1.0,2,"hi mum")

// You can get the values out with pattern matching
let t1, t2, t3 = myTuple

// YOUR TURN - create a tuple containing three integers, then extract the contents into three values

(*** Records ***)

// Records are lightweight types
type myRecord = { Name: string; Age: int }

let jim = {Name = "Jim"; Age = 34}

// YOUR TURN - create a record to store your pet details, and populate it with a gerbil called Dean.

(*** Evaluation ***)

// Things are evaluated STRICTLY left-to-right
let result = square 10 + 2 //102

let failure = square cube 2 // doesn't even compile - you're trying to pass the unapplied 'cube' 
// function as a parameter to the 'square' function

// Use parenthesis to define different evaluation order
let result2 = square (10+2) // 144
let success = square (cube 2)

// YOUR TURN - call the 'cube' function, passing 2*5