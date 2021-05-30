REM Run in Developer Command Prompt for VS 2019 where the solution is kept
SonarScanner.MSBuild.exe begin /k:"life" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="ddf1ec2e3a1c836eb83be6c3d39e72af4fe254ff"
MsBuild.exe /t:Rebuild
SonarScanner.MSBuild.exe end /d:sonar.login="ddf1ec2e3a1c836eb83be6c3d39e72af4fe254ff"
