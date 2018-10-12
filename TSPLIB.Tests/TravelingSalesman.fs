namespace TSPLIB.TravelingSalesman

open System.IO
open Xunit
open TSPLIB.Types
open TSPLIB.TravelingSalesman
open System
open Xunit

module Tests =
  let testFolder = Environment.CurrentDirectory
  let testOutputFolder = Path.Combine(Environment.CurrentDirectory, "output")
  Directory.CreateDirectory(testOutputFolder) |> ignore


  [<Fact>]
  let ``Can create a default TSP structure`` () =
    let defaultTspProblem = DefaultTsp
    Assert.NotNull(defaultTspProblem)

  [<Fact>]
  let ``Can read externally generated TSPLIB file data/a280.tsp`` () =
    let tspFile = Path.Combine [|testFolder; "data/a280.tsp"|]
    let tspProblem = ReadTspFile tspFile
    Assert.Equal(280, tspProblem.Dimension)
    Assert.Equal(TravelingSalesman, tspProblem.ProblemType)
    Assert.Equal( Euclidian2D, tspProblem.EdgeWeightType)


  let ``Can read externally generated TSPLIB file data/data.tsp`` () =
    let tspFile = Path.Combine [|testFolder; "data/data.tsp"|]
    let tspProblem = ReadTspFile tspFile
    Assert.NotNull(tspProblem)
    Assert.Equal(tspProblem.Name, "TSP")
    Assert.Equal(tspProblem.Dimension, 5)
    Assert.Equal(tspProblem.ProblemType, TravelingSalesman)
    Assert.Same(tspProblem.NodeFormat, TwoDimensional)

  [<Fact>]
  let ``Can write TSP file with 2D Coordinates``() =

    let output = Path.Combine [|testOutputFolder; "test2D.tsp"|]

    let tspProblem =
      DefaultTsp
      |> Name "TSP File 2D"
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
    Threading.Thread.Sleep(1000) |> ignore

    Assert.True(File.Exists(output))
    let bytesRead = (File.OpenRead(output)).Length
    Assert.True((bytesWritten = bytesRead))

  [<Fact>]
  let ``Can write TSP file with 3D Coordinates``() =

    let output = Path.Combine [|testOutputFolder; "test3D.tsp"|]

    let tspProblem =
      DefaultTsp
      |> Name "TSP File 3D"
      |> Comment "Some comment"
      |> Comment "Some other comment"
      |> ProblemType TravelingSalesman
      |> EdgeWeightType Euclidian3D
      |> NodeFormat ThreeDimensional
      |> Coordinate [0.0; 0.0; 0.0]
      |> Coordinate [0.0; 0.5; 2.0]
      |> Coordinate [0.0; 1.0; 1.1]
      |> Coordinate [1.0; 1.0; 1.0]
      |> Coordinate [1.0; 0.0; 0.0]

    let bytesWritten = tspProblem |> WriteTspFile output
    Threading.Thread.Sleep(1000) |> ignore

    Assert.True(File.Exists(output))
    let bytesRead = (File.OpenRead(output)).Length
    Assert.True((bytesWritten = bytesRead))

  [<Fact>]
  let ``Check content of written model is same when read ``() =
    let output = Path.Combine [|testOutputFolder; "model.tsp"|]

    let modelOut =
      DefaultTsp
      |> Name "TSP File 3D"
      |> Comment "Some comment"
      |> ProblemType TravelingSalesman
      |> EdgeWeightType Euclidian3D
      |> Coordinate [0.0; 0.0; 0.0]
      |> Coordinate [0.0; 0.5; 2.0]
      |> Coordinate [0.0; 1.0; 1.1]

    modelOut |> WriteTspFile output |> ignore
    Threading.Thread.Sleep(1000) |> ignore

    let modelIn = ReadTspFile output
    Assert.StrictEqual(modelOut, modelIn)

