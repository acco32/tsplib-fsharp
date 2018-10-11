namespace TSPLIB.TravelingSalesman

open System.IO
open Xunit
open TSPLIB.Types
open TSPLIB.TravelingSalesman
open System

module Tests =
  let testFolder = Environment.CurrentDirectory
  let testOutputFolder = Path.Combine(Environment.CurrentDirectory, "output")
  Directory.CreateDirectory(testOutputFolder) |> ignore


  [<Fact>]
  let ``Can create a default TSP structure`` () =
    let defaultTspProblem = DefaultTsp
    Assert.NotNull(defaultTspProblem)

  [<Fact>]
  let ``Can read externally generated TSPLIB file data/data.tsp`` () =
    let tspFile = Path.Combine [|testFolder; "data/data.tsp"|]
    let tspProblem = ReadTspFile tspFile
    Assert.NotNull(tspProblem)
    Assert.Equal(tspProblem.Name, "TSP")
    Assert.Equal(tspProblem.Dimension, 5)
    Assert.Equal(tspProblem.ProblemType, TravelingSalesman)
    Assert.Same(tspProblem.NodeFormat, TwoDimensional)

  [<Fact>]
  let ``Can write TSP file``() =

    let output = Path.Combine [|testOutputFolder; "test1.tsp"|]

    let tspProblem =
      DefaultTsp
      |> Name "TSP File"
      |> Comment "Some comment"
      |> Comment "Some other comment"
      |> ProblemType TravelingSalesman
      |> EdgeWeightType Euclidian2D
      |> NodeFormat TwoDimensional
      |> Coordinate [0.0; 0.0]
      |> Coordinate [0.0; 0.5]
      |> Coordinate [0.0; 1.0]
      |> Coordinate [1.0; 1.0]
      |> Coordinate [1.0; 0.0]

    let bytesWritten = tspProblem |> WriteTspFile output
    Threading.Thread.Sleep(1000) |> ignore // TODO: change to async wait and remove sleep
    Assert.True((bytesWritten > 0L))

