cd /d %~dp0

@rem ˆê‰ñ“®‚©‚µ‚½‚ç‚à‚¤‚â‚ñ‚È‚¢‚ÆŽv‚¤

set ROOT_DIR=..\..
set RAW_DIR=%ROOT_DIR%\raw-intellisense
set WORK_DIR=%ROOT_DIR%\work-intellisense
set APP=VsLocalizedIntellisense.Xml\bin\x64\Debug\net8.0\VsLocalizedIntellisense.Xml.exe

"%APP%" --raw_dir=%RAW_DIR% --work_intellisense_dir=%WORK_DIR% --dotnet_version_clr=net8.0 --dotnet_version_standard=netstandard2.1
@rem "%APP%" --raw_dir=%RAW_DIR% --work_intellisense_dir=%WORK_DIR% --dotnet_version_clr=net7.0 --dotnet_version_standard=ignore
@rem "%APP%" --raw_dir=%RAW_DIR% --work_intellisense_dir=%WORK_DIR% --dotnet_version_clr=net6.0 --dotnet_version_standard=ignore
@rem "%APP%" --raw_dir=%RAW_DIR% --work_intellisense_dir=%WORK_DIR% --dotnet_version_clr=net5.0 --dotnet_version_standard=ignore
@rem "%APP%" --raw_dir=%RAW_DIR% --work_intellisense_dir=%WORK_DIR% --dotnet_version_clr=net3.1 --dotnet_version_standard=ignore
@rem "%APP%" --raw_dir=%RAW_DIR% --work_intellisense_dir=%WORK_DIR% --dotnet_version_clr=net3.0 --dotnet_version_standard=ignore



