namespace TSPLIB.TravelingSalesman

open System.IO
open Xunit
open TSPLIB.Types
open TSPLIB.TravelingSalesman

module Tests =
  let testFolder = __SOURCE_DIRECTORY__

  [<Fact>]

  let ``Can create a default TSP structure`` () =
    let defaultTspProblem = DefaultTsp
    Assert.NotNull(defaultTspProblem)

  [<Fact>]
  let ``Can read a TSPLIB file data/data.tsp`` () =
    let tspFile = Path.Combine [|testFolder; "data/data.tsp"|]
    let tspProblem = ReadTspFile tspFile
    Assert.NotNull(tspProblem)
    Assert.Equal(tspProblem.Name, "TSP")
    Assert.Equal(tspProblem.Dimension, 5)
    Assert.Equal(tspProblem.Type, TravelingSalesman)
    Assert.Same(tspProblem.NodeFormat, TwoDimensional)

