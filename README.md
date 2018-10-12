# TSPLIB-fsharp
[![Build Status](https://travis-ci.com/acco32/tsplib-fsharp.svg?branch=master)](https://travis-ci.com/acco32/tsplib-fsharp)


Permits the reading and writing of TSPLIB formats that can be solved by [LKH](http://akira.ruc.dk/~keld/research/LKH-3/) solver.


## Example
Write Traveling Salesman Model to `data.tsp`
```fsharp
let output = "data.tsp"

let modelOut =
  DefaultTsp
  |> Name "TSP File 3D"
  |> Comment "Some comment"
  |> ProblemType TravelingSalesman
  |> EdgeWeightType Euclidian3D
  |> NodeFormat ThreeDimensional
  |> Coordinate [0.0; 0.0; 0.0]
  |> Coordinate [0.0; 0.5; 2.0]
  |> Coordinate [0.0; 1.0; 1.1]

modelOut |> WriteTspFile output 

```

## Links

https://en.wikipedia.org/wiki/Lin%E2%80%93Kernighan_heuristic  