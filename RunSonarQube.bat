REM Run in Developer Command Prompt for VS 2019 where the solution is kept
SonarScanner.MSBuild.exe begin /k:"life" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="ddf1ec2e3a1c836eb83be6c3d39e72af4fe254ff" /d:sonar.cs.vscoveragexml.reportsPaths=%CD%\VisualStudio.coveragexml 
MsBuild.exe /t:Rebuild
"C:\Users\rober\AppData\Local\FineCodeCoverage\msTestPlatform\16.9.1\tools\net451\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" collect /output:VisualStudio.coverage1 "vstest.console.exe" "D:\RobDev\RogedoLife\Rogedo.LifeEngine.Test\bin\Debug\netcoreapp3.1\Rogedo.LifeEngine.Test.dll"
"C:\Users\rober\AppData\Local\FineCodeCoverage\msTestPlatform\16.9.1\tools\net451\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" collect /output:VisualStudio.coverage2 "vstest.console.exe" "D:\RobDev\RogedoLife\Rogedo.LifeEngine.Tools.Test\bin\Debug\netcoreapp3.1\Rogedo.LifeEngine.Tools.Test.dll"
"C:\Users\rober\AppData\Local\FineCodeCoverage\msTestPlatform\16.9.1\tools\net451\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" analyze /output:VisualStudio.coveragexml VisualStudio.coverage1 VisualStudio.coverage2
SonarScanner.MSBuild.exe end /d:sonar.login="ddf1ec2e3a1c836eb83be6c3d39e72af4fe254ff"
