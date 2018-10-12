namespace TSPLIB

open System.IO
open Types
open System.Text.RegularExpressions

module TravelingSalesman =

  let DefaultTsp =
    {
      Name = "";
      ProblemType = TravelingSalesman;
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

  let ProblemType (problemType:Problem) (tsp: Tsp) =
    {tsp with ProblemType = problemType}

  let EdgeWeightType (edgeWeight:EdgeWeight) (tsp:Tsp) =
    {tsp with EdgeWeightType = edgeWeight}

  let NodeFormat (coordType:NodeCoordinateFormat) (tsp:Tsp) =
    {tsp with NodeFormat = coordType}

  let Coordinate (coord: float list) (tsp:Tsp) =

    match coord.Length with
    | n when n=2 ->
        let newCoordinates = List.append tsp.NodeCoordinates [TwoDimension(coord.[0], coord.[1])]
        {tsp with NodeCoordinates=newCoordinates; Dimension=newCoordinates.Length}
    | n when n=3 ->
        let newCoordinates = List.append tsp.NodeCoordinates [ThreeDimension(coord.[0], coord.[1], coord.[2])]
        {tsp with NodeCoordinates=newCoordinates; Dimension=newCoordinates.Length}
    | _ ->
        failwith "Invalid Coordinate Length"

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
            | "TYPE" -> tsp <- tsp |> ProblemType (parseProblemType v)
            | "DIMENSION" -> ()
            | "NODE_COORD_TYPE" -> tsp <- tsp |> NodeFormat (parseCoordFormat v)
            | "EDGE_WEIGHT_TYPE" -> tsp <- tsp |> EdgeWeightType (parseEdgeWeight v)
            | _ -> ()

          | _ ->

            let k = temp.[0]

            match k with
            | "NODE_COORD_SECTION" | "EOF" -> ()
            | _ ->
              let data = Regex.Replace(x.Trim(), @"\s+", " ").Split(' ')
              let s = List.map (fun c -> float(c)) (Array.toList data.[1..])
              tsp <- tsp |> Coordinate s
     )

    {tsp with Dimension = tsp.NodeCoordinates.Length}

  let WriteTspFile (filename:string) (tsp:Tsp)  =
    use file = new StreamWriter(filename)

    fprintfn file "NAME: %s" tsp.Name
    List.iter (fun (x:string) -> fprintfn file "COMMENT: %s" x) tsp.Comments
    fprintfn file "TYPE: %s" (Problem.Name tsp.ProblemType)
    fprintfn file "DIMENSION: %i" tsp.Dimension

    if tsp.NodeFormat <> NoCoordinates then
      fprintfn file "NODE_COORD_TYPE: %s" (NodeCoordinateFormat.Name tsp.NodeFormat)

    fprintfn file "EDGE_WEIGHT_TYPE: %s" (EdgeWeight.Name tsp.EdgeWeightType)

    fprintfn file "NODE_COORD_SECTION"
    List.iteri (
      fun idx (coord:NodeCoordinate) ->

        match coord with
        | TwoDimension(x,y) ->
            fprintfn file "%i %f %f" (idx+1) x y
        | ThreeDimension(x,y,z) ->
            fprintfn file "%i %f %f %f" (idx+1) x y z

      ) (tsp.NodeCoordinates)
    fprintf file "EOF"

    file.Flush()

    file.BaseStream.Length
