namespace TSPLIB


module Types =

  type Problem = private Problem of string
    with
    static member Name (Problem pt) = pt


  let TravelingSalesman = Problem "TSP"
  let AsymetricTravelingSalesman = Problem "ATSP"
  let SequentialOrdering = Problem "SOP"
  let HamiltonianCycle = Problem "HCP"
  let CapacitatedVehicleRouting = Problem "CVRP"
  let Tour = Problem "TOUR"
  let Unknown = Problem "UNKNOWN"

  type EdgeWeight = private EdgeWeight of string
    with
    static member Name (EdgeWeight ew) = ew

  let Explicit = EdgeWeight "Explicit"
  let Euclidian2D = EdgeWeight "EUC_2D"
  let Euclidian3D = EdgeWeight "EUC_3D"
  let Maximum2D = EdgeWeight "MAX_2D"
  let Maximum3D = EdgeWeight "MAX_3D"
  let Manhattan2D = EdgeWeight "MAN_2D"
  let Manhattan3D = EdgeWeight "MAN_3D"

  let EuclidianRoundUp2D = EdgeWeight "CEIL_2D"
  let Geographical = EdgeWeight "GEO"
  let ATT = EdgeWeight "ATT"

  let CrystallographyV1 = EdgeWeight "XRAY1"
  let CrystallographyV2 = EdgeWeight "XRAY2"
  let Special = EdgeWeight "SPECIAL"


  type NodeCoordinateFormat = private NodeCoordinateFormat of string
    with
    static member Name (NodeCoordinateFormat ncf) = ncf

  let TwoDimensional = NodeCoordinateFormat "TWOD_COORDS"
  let ThreeDimensional = NodeCoordinateFormat "THREED_COORDS"
  let NoCoordinates = NodeCoordinateFormat "NO_COORDS"


  type NodeCoordinate =
    | TwoDimensional of float * float
    | ThreeDimensional of float * float * float
    | LabeledTwoDimensional of string * float * float
    | LabeledThreeDimensional of string * float * float * float

  type Tsp = {
    Name: string;
    Type: Problem;
    Comments: string list;
    Dimension: int;
    EdgeWeightType: EdgeWeight;
    NodeFormat: NodeCoordinateFormat;
    NodeCoordinates: NodeCoordinate list;
  }

