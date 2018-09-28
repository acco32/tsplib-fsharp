module TSPLIB.TravelingSalesman.Tests

open System
open Xunit
open TSPLIB.Types
open TSPLIB.TravelingSalesman

[<Fact>]
let ``Can create a default TSP structure`` () =
  let defaultTspProblem = DefaultTsp
  Assert.NotNull(defaultTspProblem)
  