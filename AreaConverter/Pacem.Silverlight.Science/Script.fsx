#light

// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

#load "EuclideanGeometry.fs"
open System
open Pacem.Science.Geometry

let points = [|Point2D(0.0, 0.0); Point2D(1.0, 0.0); Point2D(1.0,2.0)|]
let a = EuclideanGeometry.Area2D(points)
a;;
