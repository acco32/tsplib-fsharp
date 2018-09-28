module TSPLIB

open TSPLIB.Types

let createTsp =
  {
    Name = "string.Empty";
    Type = TravelingSalesman;
    Comments = [];
    Dimension = -1;
    Capacity = None;
    EdgeWeight = Euclidian2D;
  }
