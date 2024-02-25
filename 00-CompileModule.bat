@echo off

call MC-CS2 OcbBiggerSchools.dll Harmony\*.cs ^
  /reference:"%PATH_CS2_MANAGED%\Game.dll" && ^
echo Successfully compiled OcbBiggerSchools.dll

pause