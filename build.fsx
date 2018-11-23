#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.Tools.Git
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.DotNet
open Fake.Core
open Fake.Core.TargetOperators
open Fake.IO
open Fake.Tools.Git

let buildDir = "./build/"

let cleanOpts (opt:DotNet.Options) = opt


Target.create "Clean" (fun _ ->
  Shell.cleanDirs [buildDir]
  DotNet.exec cleanOpts "clean" "TSPLIB.sln" |> ignore
)

Target.create "Test" (fun _ ->
  let testOpts (opt:DotNet.TestOptions) =
    {
      opt with
        Configuration=DotNet.BuildConfiguration.Debug
    }

  DotNet.test testOpts "TSPLIB.Tests"
)

Target.create "Release" (fun _ ->
  let releaseOpts (opt:DotNet.BuildOptions) =
    {
      opt with
        Configuration=DotNet.BuildConfiguration.Release;
        OutputPath=Some(Path.combine __SOURCE_DIRECTORY__ buildDir);
    }

  DotNet.build releaseOpts "TSPLIB"
)

Target.create "Deploy-Nuget" (fun _ ->
  let releaseVersion = Information.getLastTag().TrimStart([|'v'|])
  System.Environment.SetEnvironmentVariable("TSPLIB_FS_VERSION", releaseVersion)

  let packOpts (opt:DotNet.PackOptions) : DotNet.PackOptions =
    {
      opt with
        Configuration=DotNet.BuildConfiguration.Release;
        OutputPath=Some(Path.combine __SOURCE_DIRECTORY__ buildDir);
    }

  DotNet.pack packOpts "TSPLIB"
  System.Environment.SetEnvironmentVariable("TSPLIB_FS_VERSION", "")

  DotNet.exec cleanOpts "nuget" "push build/*.nupkg -s https://www.nuget.org -k ${NUGET_API_KEY}" |> ignore
)

Target.create "Help" (fun _ ->
  Trace.trace "TSPLIB F# Build Script\nRun fake <script> -t [Target]"
)


"Clean"
==> "Test"
  ==> "Release"
==> "Help"

"Clean"
==> "Deploy-Nuget"
==> "Help"


Target.runOrDefault "Help"
