msbuild.exe IO.Swagger.sln /p:Configuration=Release
cd C:\temp\test\Code_PSTK\WixProject1
msbuild.exe PSTK_Installer.wixproj /p:Configuration=Debug