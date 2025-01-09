@echo off

if "%1"=="" (
    echo usage: merge.bat ^<from branch^> ^<to branch^>
    exit /b 1
)
if "%2"=="" (
    echo usage: merge.bat ^<from branch^> ^<to branch^>
    exit /b 1
)
set from=%1
set to=%2
git checkout %1
git pull
git checkout %2
git merge --no-ff %1
