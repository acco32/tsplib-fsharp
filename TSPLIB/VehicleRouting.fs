namespace TSPLIB

open Types
open VehicleRoutingTypes

module VehicleRouting =

  let DefaultVrp =
    Vrp({Name = ""
         ProblemType = CapacitatedVehicleRouting
         Comments = []
         Dimension = 0
         DisplayType = NoDisplay
         Capacity = 0
         Demand = []
         Depots = []
         EdgeWeightType = Euclidian2D},
         {NodeCoordinates = []})

  let ExplicitVrp =
    Exp({Name = ""
         ProblemType = CapacitatedVehicleRouting
         Comments = []
         Dimension = 0
         EdgeWeightType = Explicit
         DisplayType = NoDisplay
         Capacity = 0
         Demand = []
         Depots = []},
        {EdgeWeightFormat = Function
         EdgeDataFormat = EdgeList
         EdgeData = []
         EdgeWeights = []})

  let Name (name: string) (vrp: Vrp) =
    match vrp with
    | Vrp(v,d) -> Vrp({v with Name = name},d)
    | Exp(v,e) -> Exp({v with Name = name},e)

  let Comment (comment:string) (vrp:Vrp) =
    match vrp with
    | Vrp(v,d) -> Vrp({v with Comments = (List.append v.Comments [comment])}, d)
    | Exp(v,e) -> Exp({v with Comments = (List.append v.Comments [comment])}, e)

  let ProblemType (problemType:Problem) (vrp:Vrp) =
    match vrp with
    | Vrp(v,d) -> Vrp({v with ProblemType = problemType}, d)
    | Exp(v,e) -> Exp({v with ProblemType = problemType}, e)

  let EdgeWeightType (edgeWeight:EdgeWeight) (vrp:Vrp) =
    match vrp with
    | Vrp(v,d) -> Vrp({v with EdgeWeightType = edgeWeight}, d)
    | Exp(v,e) -> Exp({v with EdgeWeightType = edgeWeight}, e)

  let Capacity (capacity:int) (vrp:Vrp) =
    match vrp with
    | Vrp(v,d) -> Vrp({v with Capacity = capacity}, d)
    | Exp(v,e) -> Exp({v with Capacity = capacity}, e)


