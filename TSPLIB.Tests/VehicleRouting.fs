namespace TSPLIB.Tests

open Xunit
open TSPLIB.VehicleRouting

module VehicleRouting =

  [<Fact>]
  let ``Can create a default VRP structure`` () =
    let defaultVrpProblem = DefaultVrp
    Assert.NotNull(defaultVrpProblem)

  [<Fact>]
  let ``Can create a explicit VRP structure`` () =
    let explicitVrpProblem = ExplicitVrp
    Assert.NotNull(explicitVrpProblem)
