[Setup]
AppId={{A6F27D36-5A1E-4B0A-8D53-ED73E96E8E20}
AppName=GMU EduTrans ERP
AppVersion=1.0.0
DefaultDirName={autopf}\GMU EduTrans ERP
DefaultGroupName=GMU EduTrans ERP
OutputDir=..\dist
OutputBaseFilename=GMU EduTrans ERP Setup
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible

[Files]
Source: "..\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\GMU EduTrans ERP"; Filename: "{app}\GMUEduTrans.Desktop.exe"
Name: "{autodesktop}\GMU EduTrans ERP"; Filename: "{app}\GMUEduTrans.Desktop.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Buat shortcut di Desktop"; GroupDescription: "Shortcut tambahan:"

[Run]
Filename: "{app}\GMUEduTrans.Desktop.exe"; Description: "Jalankan GMU EduTrans ERP"; Flags: nowait postinstall skipifsilent
