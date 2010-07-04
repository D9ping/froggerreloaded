[Setup]
AppId={{3CA63D69-FC83-4D9B-B46D-35E3AAFDA8EA}
AppName=Frogger Reloaded
AppVerName=Frogger Reloaded 0.9.9
VersionInfoVersion=0.9.9.0
OutputBaseFilename=froggerreloadded_v0.9.9_beta

AppPublisher=Frogger Reloaded
AppPublisherURL=http://code.google.com/p/froggerreloaded/
AppSupportURL=http://code.google.com/p/froggerreloaded/
AppUpdatesURL=http://code.google.com/p/froggerreloaded/
DefaultDirName={pf}\Frogger Reloaded
DefaultGroupName=Frogger Reloaded
LicenseFile=..\Debug\license.txt
OutputDir=.\
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

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
;Name: "dutch"; MessagesFile: "compiler:Languages\Dutch.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\Debug\Frogger.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Debug\levels\basic1.lvl"; DestDir: "{app}\levels\"; Flags: ignoreversion
Source: "..\Debug\levels\basic2.lvl"; DestDir: "{app}\levels\"; Flags: ignoreversion
Source: "..\Debug\levels\basic3.lvl"; DestDir: "{app}\levels\"; Flags: ignoreversion
Source: "..\Debug\levels\highway.lvl"; DestDir: "{app}\levels\"; Flags: ignoreversion
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

[CustomMessages]
english.dotnetmissing=This setup requires the .NET Framework v2.0. Please download and install the .NET Framework v.2 and run this setup again. Do you want to download the framework now?

[Code]
function InitializeSetup(): Boolean;
var
    ErrorCode: Integer;
    NetFrameWorkInstalled : Boolean;
    Result1 : Boolean;
begin

	NetFrameWorkInstalled := RegKeyExists(HKLM,'SOFTWARE\Microsoft\.NETFramework\policy\v2.0');
	if NetFrameWorkInstalled =true then
	begin
		Result := true;
	end;

	if NetFrameWorkInstalled = false then
	begin
		NetFrameWorkInstalled := RegKeyExists(HKLM,'SOFTWARE\Microsoft\.NETFramework\policy\v2.0');
		if NetFrameWorkInstalled =true then
		begin
			Result := true;
		end;

		if NetFrameWorkInstalled =false then
			begin
				//Result1 := (ExpandConstant('{cm:dotnetmissing}'), mbConfirmation, MB_YESNO) = idYes;
				Result1 := MsgBox(ExpandConstant('{cm:dotnetmissing}'),
						mbConfirmation, MB_YESNO) = idYes;
				if Result1 =false then
				begin
					Result:=false;
				end
				else
				begin
					Result:=false;
					ShellExec('open',
					'http://download.microsoft.com/download/5/6/7/567758a3-759e-473e-bf8f-52154438565a/dotnetfx.exe',
					'','',SW_SHOWNORMAL,ewNoWait,ErrorCode);
                end;
            end;
	end;
end;













