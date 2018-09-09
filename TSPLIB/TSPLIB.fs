module TSPLIB

open TSPLIB.Types

let createTsp =
  {
    Name = "string.Empty";
    Type =TravelingSalemanProblem;
    Comments = [];
    Dimension = -1;
    Capacity = None;
    EdgeWeight = Euclidian2D;
  }
