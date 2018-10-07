namespace TSPLIB

open System.IO
open Types

module TravelingSalesman =

  let DefaultTsp =
    {
      Name = "";
      Type = TravelingSalesman;
      Comments = [];
      Dimension = 0;
      EdgeWeightType = Euclidian2D;
      NodeFormat = NoCoordinates;
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

  let EdgeWeightType (edgeWeight:EdgeWeight) (tsp:Tsp) =
    {tsp with EdgeWeightType=edgeWeight}

  let NodeFormat (coordType:NodeCoordinateFormat) (tsp:Tsp) =
    {tsp with NodeFormat = coordType}

  let Coordinates (coord:NodeCoordinate) (tsp:Tsp) =
    let newCoordinates = List.append tsp.NodeCoordinates [coord]
    {tsp with NodeCoordinates=newCoordinates}


  let ReadTspFile (filename:string) : Tsp =
    match filename with
    | null ->
        failwith "filename cannot be null"
    | "" ->
        failwith "filename cannot be an empty string"
    | _ -> ()

    match File.Exists(filename) with
    | false ->
        failwith "file does not exists"
    | _ -> ()

    let (|Field|Section|Unknown|) (fld:string) =
      match fld with
      | "NAME" | "COMMENT" | "TYPE" | "DIMENSION" | "NORD_COORD_TYPE" | "EDGE_WEIGHT_TYPE" ->
          Field
      | "NODE_COORD_SECTION" | "EOF" ->
          Section
      | _ -> Unknown

    let parseProblemType (problemString:string) : Problem =
      if Problem.Name TravelingSalesman = problemString then
        TravelingSalesman
      else
        Unknown

    let parseProblemDimension (dimensionString:string) : int =
      int(dimensionString)

    let parseEdgeWeight (edgeWeight:string) : EdgeWeight =
      if EdgeWeight.Name Euclidian2D = edgeWeight then
        Euclidian2D
      elif EdgeWeight.Name Euclidian3D = edgeWeight then
        Euclidian3D
      elif EdgeWeight.Name Maximum2D = edgeWeight then
        Maximum2D
      elif EdgeWeight.Name Maximum3D = edgeWeight then
        Maximum3D
      elif EdgeWeight.Name Manhattan2D = edgeWeight then
        Manhattan2D
      elif EdgeWeight.Name Manhattan3D = edgeWeight then
        Manhattan3D
      elif EdgeWeight.Name EuclidianRoundUp2D = edgeWeight then
        EuclidianRoundUp2D
      elif EdgeWeight.Name Geographical = edgeWeight then
        Geographical
      elif EdgeWeight.Name ATT = edgeWeight then
        ATT
      elif EdgeWeight.Name CrystallographyV1 = edgeWeight then
        CrystallographyV1
      elif EdgeWeight.Name CrystallographyV2 = edgeWeight then
        CrystallographyV2
      elif EdgeWeight.Name Special = edgeWeight then
        Special
      else
        Explicit

    let parseCoordFormat (coordFormat:string) : NodeCoordinateFormat =
      if NodeCoordinateFormat.Name TwoDimensional = coordFormat then
        TwoDimensional
      elif NodeCoordinateFormat.Name ThreeDimensional = coordFormat then
        ThreeDimensional
      else
        NoCoordinates

    let lines = File.ReadAllLines(filename)

    let mutable (tsp:Tsp) = DefaultTsp
    lines |> Array.iter (
      fun x ->
          let temp = x.Split(':')

          match temp.Length with
          | n when n = 2 ->

            let k, v = temp.[0].Trim(), temp.[1].Trim()

            match k with
            | "NAME" -> tsp <- tsp |> Name v
            | "COMMENT" ->  tsp <- tsp |> Comment v
            | "TYPE" -> tsp <- tsp |> Type (parseProblemType v)
            | "DIMENSION" -> tsp <- tsp |> Dimension (parseProblemDimension v)
            | "NODE_COORD_TYPE" -> tsp <- tsp |> NodeFormat (parseCoordFormat v)
            | "EDGE_WEIGHT_TYPE" -> tsp <- tsp |> EdgeWeightType (parseEdgeWeight v)
            | _ -> ()

          | _ ->

            let k = temp.[0]

            match k with
            | "NODE_COORD_SECTION" | "EOF" -> ()
            | _ ->
              let data = x.Split(' ')
              tsp <- tsp |> Coordinates (LabeledTwoDimensional (data.[0], float(data.[1]), float(data.[2])))

     )

    tsp

