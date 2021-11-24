Get-ChildItem *.csproj -Recurse | ForEach-Object {
    $content = [xml] (Get-Content $_)
    $xmlNameSpace = new-object System.Xml.XmlNamespaceManager($content.NameTable)
    $xmlNameSpace.AddNamespace("p", "<a href="http://schemas.microsoft.com/developer/msbuild/2003%22)">http://schemas.microsoft.com/developer/msbuild/2003")</a>
    if (-not $content.Project.PropertyGroup[0].Features) {
        Write-Host "Features missing in $_"
        $featureElt = $content.CreateElement("Features", "<a href="http://schemas.microsoft.com/developer/msbuild/2003%22)">http://schemas.microsoft.com/developer/msbuild/2003")</a>
        $featureElt.set_InnerText("IOperation")

        $content.Project.PropertyGroup[0].AppendChild($featureElt)
    }
    $content.Save($_)

    # Normalise line endings
    (Get-Content $_ -Encoding UTF8) | Set-Content $_ -Encoding UTF8
}