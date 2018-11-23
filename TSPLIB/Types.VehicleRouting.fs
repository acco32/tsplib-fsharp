namespace TSPLIB

open Types

module VehicleRoutingTypes =

  type VrpBase = {
    Name: string
    ProblemType: Problem
    Comments: string list
    Dimension: int
    EdgeWeightType: EdgeWeight
    DisplayType: DisplayData
    Capacity: int
    Demand: (int * int) list
    Depots: int list
  }

  type VrpDefault = {
    NodeCoordinates: NodeCoordinate list;
  }

  type VrpExplicit =  {
      EdgeWeightFormat: EdgeWeightFormat
      EdgeDataFormat: EdgeDataFormat
      EdgeData: int list list
      EdgeWeights: int list list
    }

  type Vrp =
    | Vrp of VrpBase * VrpDefault
    | Exp of VrpBase * VrpExplicit
