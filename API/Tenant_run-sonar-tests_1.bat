rem @echo off
rem file directory to the unit test set "UnitTestSolutionDir=<--Path to unit tests folder-->"

dotnet tool install --global dotnet-sonarscanner


dotnet sonarscanner begin /k:"Dotnet-Training" /d:sonar.host.url="https://sonar.mypropelinc.com"  /d:sonar.login="c03ec7511436166e6478624511969b7f7cf34271" /d:sonar.exclusions=bin,obj /d:sonar.coverageReportPaths="coverage/SonarQube.xml"

rem build the project	
dotnet build

rem Test the project
rem dotnet test "%SolutionDir%\MyProject.Tests.csproj" trx --results-directory "%TestDir%\TestResults" --filter "TestCategory=UnitTests"

dotnet sonarscanner end /d:sonar.login="71ae3d924efe4ed22183a22ecb195133ab385082"




