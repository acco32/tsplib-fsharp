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
    | TwoDimension of float * float
    | ThreeDimension of float * float * float

  type Tsp = {
    Name: string;
    ProblemType: Problem;
    Comments: string list;
    Dimension: int;
    EdgeWeightType: EdgeWeight;
    NodeFormat: NodeCoordinateFormat;
    NodeCoordinates: NodeCoordinate list;
  }

  type EdgeWeightFormat = private EdgeWeightFormat of string
    with
    static member Name (EdgeWeightFormat ewf) = ewf

  let Function = EdgeWeightFormat "FUNCTION"
  let FullMatrix = EdgeWeightFormat "FULL_MATRIX"
  let UpperRow = EdgeWeightFormat "UPPER_ROW"
  let LowerRow = EdgeWeightFormat "LOWER_ROW"
  let UpperDiagonalRow = EdgeWeightFormat "UPPER_DIAG_ROW"
  let LowerDiagonalRow = EdgeWeightFormat "LOWER_DIAG_ROW"
  let UpperColumn = EdgeWeightFormat "UPPER_COL"
  let LowerColumn = EdgeWeightFormat "LOWER_COL"
  let UpperDiagonalColumn = EdgeWeightFormat "UPPER_DIAG_COL"
  let LowerDiagonalColumn = EdgeWeightFormat "LOWER_DIAG_COL"


  type EdgeDataFormat = private EdgeDataFormat of string
    with
    static member Name (EdgeDataFormat edf) = edf

  let EdgeList = EdgeDataFormat "EDGE_LIST"
  let AdjacencyList = EdgeDataFormat "ADJ_LIST"

  type DisplayData = private DisplayData of string
    with
    static member Name (DisplayData dd) = dd

  let CoordinateDisplay = DisplayData "COORD_DISPLAY"
  let TwoDimensionalDisplay = DisplayData "TWOD_DISPLAY"
  let NoDisplay = DisplayData "NO_DISPLAY"

  type Vrp = {
    Name: string;
    ProblemType: Problem;
    Comments: string list;
    Dimension: int;
    EdgeWeightType: EdgeWeight;
    EdgeWeightFormat: EdgeWeightFormat;
    EdgeDataFormat: EdgeDataFormat;
    DisplayType: DisplayData;
    Capacity: int;
    Demand: int*int list;
    Depots: int list;
    EdgeData: int list list
    EdgeWeights: int list list
  }
