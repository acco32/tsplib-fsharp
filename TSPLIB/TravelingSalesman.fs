namespace TSPLIB

open Types

module TravelingSalesman =

  let DefaultTsp =
    {
      Name = "";
      Type = TravelingSalesman;
      Comments = [];
      Dimension = 0;
      EdgeWeight = Euclidian2D;
      NodeFormat = NoFormat;
      NodeCoordinates = [];
    }

  let Name name (tsp:Tsp): Tsp =
    {tsp with Name=name}

  let Comment (comment:string) (tsp:Tsp) =
    let newComments = List.append tsp.Comments [comment]
    {tsp with Comments = newComments}

  let Type (problemType:Problem) (tsp: Tsp) =
    {tsp with Type = problemType}

  let Dimension (dim:int) (tsp:Tsp) =
    {tsp with Dimension=dim}

  let EdgeWeight (edgeWeight:EdgeWeight) (tsp:Tsp) =
    {tsp with EdgeWeight=edgeWeight}

  let NodeCoordType (coordType:NodeCoordinateFormat) (tsp:Tsp) =
    {tsp with NodeFormat = coordType}

  let Coordinate (coord:NodeCoordinate) (tsp:Tsp) =
    let newCoordinates = List.append tsp.NodeCoordinates [coord]
    {tsp with NodeCoordinates=newCoordinates}


