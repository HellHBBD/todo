set from=%1
set to=%2
git checkout %1
git pull
git checkout %2
git merge --no-ff %1
