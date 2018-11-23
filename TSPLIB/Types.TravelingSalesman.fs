namespace TSPLIB

open Types

module TravelingSalesmanTypes =

  type Tsp = {
      Name: string;
      ProblemType: Problem;
      Comments: string list;
      Dimension: int;
      EdgeWeightType: EdgeWeight;
      NodeFormat: NodeCoordinateFormat;
      NodeCoordinates: NodeCoordinate list;
    }
