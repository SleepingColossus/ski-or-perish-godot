namespace Util.FileDetector

open System.IO

module Program =

    let [<Literal>] TemplateFile = "_template.tmx"
    let [<Literal>] CollisionFileIdentifier = "_collision"

    let getDirectoryInfos : DirectoryInfo * DirectoryInfo =
        let currentDirPath = Directory.GetCurrentDirectory()
        let currentDir = DirectoryInfo(currentDirPath)
        let repoDir = currentDir.Parent.Parent.Parent.Parent.Parent

        let mapsDirPath = Path.Combine(repoDir.FullName, "Assets", "Maps")
        let exportDirPath = Path.Combine(mapsDirPath, "Exported")

        let mapsDir = DirectoryInfo(mapsDirPath)
        let exportDir = DirectoryInfo(exportDirPath)

        mapsDir, exportDir

    [<EntryPoint>]
    let main _ =

        let mapsDir, exportDir = getDirectoryInfos

        System.Console.WriteLine(sprintf "Scanning maps in: %s" mapsDir.FullName)
        System.Console.WriteLine(sprintf "Scanning exported assets in: %s"exportDir.FullName)

        let tmxFiles =
            mapsDir.GetFiles("*.tmx")
            |> Array.filter (fun fileInfo -> fileInfo.Name <> TemplateFile)

        let exportedPngs = exportDir.GetFiles("*.png")
        let exporetdXmls = exportDir.GetFiles("*.xml")

        for file in tmxFiles do
            let baseName = Path.GetFileNameWithoutExtension(file.Name)

            let pngName = sprintf "%s.png" baseName
            let xmlName = sprintf "%s%s.xml" baseName CollisionFileIdentifier

            let pngExists = exportedPngs |> Array.map(fun fileInfo -> fileInfo.Name) |> Array.contains(pngName)
            let xmlExists = exporetdXmls |> Array.map(fun fileInfo -> fileInfo.Name) |> Array.contains(xmlName)

            if not pngExists then System.Console.WriteLine(sprintf "Cannot find BG file: %s" pngName)
            if not xmlExists then System.Console.WriteLine(sprintf "Cannot find Collision file: %s" xmlName)

        System.Console.WriteLine("Press any key to continue ...")
        System.Console.ReadKey() |> ignore
        0