#light

namespace Pacem.Science.Geometry

open System.Windows.Media

module public EuclideanGeometry = 
    let public Area2D(polygon: PointCollection) = 
        let n = polygon.Count
        let divideBy2 f = f * 0.5
        let step i = polygon.[i].X * polygon.[(i+1)%n].Y - polygon.[i].Y * polygon.[(i+1)%n].X
        {0..n-1}
        |> Seq.fold(fun acc i -> acc + step i ) 0.0
        |> divideBy2