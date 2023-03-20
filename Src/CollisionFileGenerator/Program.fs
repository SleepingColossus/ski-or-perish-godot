namespace CollisionFileGenerator

open System.IO
open System.Linq
open System.Xml.Linq

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

    let getCollisionData (xmlFilePath: string) =
        let doc = XDocument.Load(xmlFilePath)

        let data =
            doc.Descendants("layer")
            |> Seq.find(fun layer -> layer.Attribute("name").Value = "Special")
            |> fun (layer) -> layer.Descendants("data").Single().Value

        let modifiedData = data.Replace(",", " ")
        modifiedData

    let createCollisionDocument (templatePath: string) data =
        let template = XDocument.Load(templatePath)
        template.Descendants("Data").Single().SetValue(data)
        template

    [<EntryPoint>]
    let main _ =

        let mapsDir, exportDir = getDirectoryInfos
        let templateFile = Path.Combine(exportDir.FullName, "_template.xml")

        let tmxFiles =
            mapsDir.GetFiles("*.tmx")
            |> Array.filter (fun fileInfo -> fileInfo.Name <> TemplateFile)

        let exporetdXmls = exportDir.GetFiles("*.xml")

        for file in tmxFiles do
            let baseName = Path.GetFileNameWithoutExtension(file.Name)
            let xmlName = sprintf "%s%s.xml" baseName CollisionFileIdentifier
            let xmlExists = exporetdXmls |> Array.map(fun fileInfo -> fileInfo.Name) |> Array.contains(xmlName)
            if not xmlExists then
                let data = getCollisionData file.FullName
                let doc = createCollisionDocument templateFile data
                let fullPath = Path.Combine(exportDir.FullName, xmlName)
                System.Console.WriteLine(sprintf "Generating %s" fullPath)
                doc.Save(fullPath)

        System.Console.WriteLine("Press any key to continue ...")
        System.Console.ReadKey() |> ignore
        0
