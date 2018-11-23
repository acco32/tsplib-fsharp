namespace TSPLIB.Tests

open System.IO
open System

module Constants =
  let testFolder = Environment.CurrentDirectory
  let testOutputFolder = Path.Combine(Environment.CurrentDirectory, "output")
  Directory.CreateDirectory(testOutputFolder) |> ignore