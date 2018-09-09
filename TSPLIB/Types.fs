module TSPLIB.Types


type ProblemDataType = {
  Name: string
}

let TravelingSalemanProblem : ProblemDataType = {Name="TSP"}
let AsymetricTravelingSalemanProblem : ProblemDataType = {Name="ATSP"}
let SequentialOrderingProblem : ProblemDataType = {Name="SOP"}
let HamiltonianCycleProblem : ProblemDataType = {Name="HCP"}
let CapacitatedVehicleRoutingProblem : ProblemDataType = {Name="CVRP"}
let Tour : ProblemDataType = {Name="TOUR"}


type EdgeWeight = {
  Name: string
}

let Explicit : EdgeWeight = {Name="Explicit"}
let Euclidian2D : EdgeWeight = {Name="EUC_2D"}
let Euclidian3D : EdgeWeight = {Name="EUC_3D"}
let Maximum2D : EdgeWeight = {Name="MAX_2D"}
let Maximum3D : EdgeWeight = {Name="MAX_3D"}
let Manhattan2D : EdgeWeight = {Name="MAN_2D"}
let Manhattan3D : EdgeWeight = {Name="MAN_3D"}
let EuclidianRoundUp2D : EdgeWeight = {Name="CEIL_2D"}
let Geographical : EdgeWeight = {Name="GEO"}
let ATT : EdgeWeight = {Name="ATT"}
let CrystallographyV1 : EdgeWeight = {Name="XRAY1"}
let CrystallographyV2 : EdgeWeight = {Name="XRAY2"}
let Special : EdgeWeight = {Name="SPECIAL"}


type Tsp = {
  Name: string;
  Type: ProblemDataType;
  Comments: string list;
  Dimension: int;
  Capacity: int option;
  EdgeWeight: EdgeWeight;
}

