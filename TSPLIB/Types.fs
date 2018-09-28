namespace TSPLIB


module Types =

  type private ProblemType = ProblemType of string
  let private TravelingSalesman = ProblemType "TSP"
  let private AsymetricTravelingSalesman = ProblemType "ATSP"
  let private SequentialOrdering = ProblemType "SOP"
  let private HamiltonianCycle = ProblemType "HCP"
  let private CapacitatedVehicleRouting = ProblemType "CVRP"
  let private Tour = ProblemType "TOUR"

  type Problem =
    | TravelingSalesman
    | AsymetricTravelingSalesman
    | SequentialOrdering
    | HamiltonianCycle
    | CapacitatedVehicleRouting
    | Tour

  type private EdgeWeightType = EdgeWeightType of string

  let private Explicit = EdgeWeightType "Explicit"
  let private Euclidian2D = EdgeWeightType "EUC_2D"
  let private Euclidian3D = EdgeWeightType "EUC_3D"
  let private Maximum2D = EdgeWeightType "MAX_2D"
  let private Maximum3D = EdgeWeightType "MAX_3D"
  let private Manhattan2D = EdgeWeightType "MAN_2D"
  let private Manhattan3D = EdgeWeightType "MAN_3D"

  let private EuclidianRoundUp2D = EdgeWeightType "CEIL_2D"
  let private Geographical = EdgeWeightType "GEO"
  let private ATT = EdgeWeightType "ATT"

  let private CrystallographyV1 = EdgeWeightType "XRAY1"
  let private CrystallographyV2 = EdgeWeightType "XRAY2"
  let private Special = EdgeWeightType "SPECIAL"


  type EdgeWeight =
    | Explicit
    | Euclidian2D
    | Euclidian3D
    | Maximum2D
    | Maximum3D
    | Manhattan2D
    | Manhattan3D
    | EuclidianRoundUp2D
    | Geographical
    | ATT
    | CrystallographyV1
    | CrystallographyV2
    | Special


  type private NodeCoordinateFormatType = NodeCoordinateFormatType of string

  let private TwoDimensional = NodeCoordinateFormatType "TWOD_COORDS"
  let private ThreeDimensional = NodeCoordinateFormatType "THREED_COORDS"
  let private NoCoordinates = NodeCoordinateFormatType "NO_COORDS"

  type NodeCoordinateFormat =
    | TwoDimensional
    | ThreeDimensional
    | NoFormat

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
    EdgeWeight: EdgeWeight;
    NodeFormat: NodeCoordinateFormat;
    NodeCoordinates: NodeCoordinate list;
  }

