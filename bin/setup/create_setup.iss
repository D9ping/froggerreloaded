[Setup]
AppId={{3CA63D69-FC83-4D9B-B46D-35E3AAFDA8EA}
AppName=Frogger Reloaded
AppVerName=Frogger Reloaded 0.9
AppPublisher=Frogger Reloaded
VersionInfoProductVersion=0.9.0.0
AppPublisherURL=http://code.google.com/p/froggerreloaded/
AppSupportURL=http://code.google.com/p/froggerreloaded/
AppUpdatesURL=http://code.google.com/p/froggerreloaded/
DefaultDirName={pf}\Frogger Reloaded
DefaultGroupName=Frogger Reloaded
LicenseFile=..\Debug\license.txt
OutputDir=.\
OutputBaseFilename=froggerreloadded_v0.9
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin
WizardSmallImageFile=.\setuplogosmall.bmp
WizardImageFile=.\setuplogo.bmp
BackColor=clGreen
BackSolid=yes
WizardImageBackColor=clGreen
WizardImageStretch=yes
ShowTasksTreeLines=no
;WindowVisible=yes
;AppCopyright=Copyright (C) 2010 Frogger Reloaded


[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
;Name: "dutch"; MessagesFile: "compiler:Languages\Dutch.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\Debug\Frogger.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Debug\highscores.mdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Debug\sounds\beep.wav"; DestDir: "{app}\sounds\"; Flags: ignoreversion
Source: "..\Debug\sounds\frog_made_it.wav"; DestDir: "{app}\sounds\"; Flags: ignoreversion
Source: "..\Debug\sounds\punch.wav"; DestDir: "{app}\sounds\"; Flags: ignoreversion
Source: "..\..\Resources\Flubber.ttf"; DestDir: "{fonts}"; FontInstall: "Flubber"; Flags: onlyifdoesntexist uninsneveruninstall

[Icons]
Name: "{group}\Frogger Reloaded"; Filename: "{app}\Frogger.exe"
Name: "{group}\{cm:UninstallProgram,Frogger Reloaded}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\Frogger Reloaded"; Filename: "{app}\Frogger.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\Frogger.exe"; Description: "{cm:LaunchProgram,Frogger Reloaded}"; Flags: nowait postinstall skipifsilent







