#define AppName "Frogger Reloaded"
#define AppNameShort = "froggerreloaded"
#define AppUrl "https://github.com/D9ping/froggerreloaded"
#define CurrentVersion = "1.0.2"

[Setup]
AppId={{3CA63D69-FC83-4D9B-B46D-35E3AAFDA8EA}
AppName={#AppName}
AppVerName={#AppName} {#CurrentVersion}
VersionInfoVersion={#CurrentVersion}.0
OutputBaseFilename={#AppNameShort}_v{#CurrentVersion}

AppPublisher={#AppName}
AppPublisherURL={#AppUrl}
AppSupportURL={#AppUrl}
AppUpdatesURL={#AppUrl}
DefaultDirName={pf}\{#AppName}
DefaultGroupName={#AppName}
LicenseFile=..\Debug\license.txt
OutputDir=.\
Compression=lzma
SolidCompression=true
PrivilegesRequired=admin
WizardSmallImageFile=.\setuplogosmall.bmp
WizardImageFile=.\setuplogo.bmp
BackColor=clGreen
BackSolid=true
WizardImageBackColor=clGreen
WizardImageStretch=true
ShowTasksTreeLines=false
AlwaysShowDirOnReadyPage=true

[Languages]
Name: english; MessagesFile: compiler:Default.isl
Name: dutch; MessagesFile: compiler:Languages\Dutch.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Files]
Source: ..\Debug\Frogger.exe; DestDir: {app}; Flags: ignoreversion
Source: ..\Debug\sounds\beep.wav; DestDir: {app}\sounds\; Flags: ignoreversion
Source: ..\Debug\sounds\frog_made_it.wav; DestDir: {app}\sounds\; Flags: ignoreversion
Source: ..\Debug\sounds\punch.wav; DestDir: {app}\sounds\; Flags: ignoreversion
Source: ..\Debug\sounds\sink.wav; DestDir: {app}\sounds\; Flags: ignoreversion
Source: ..\..\Resources\Flubber.ttf; DestDir: {fonts}; FontInstall: Flubber; Flags: onlyifdoesntexist uninsneveruninstall
; currently embedded in Frogger.exe executable and then extracted to %appdata%\froggerreloaded\ so it work for every user on the system.
;Source: ..\Debug\highscores.mdb; DestDir: {userappdata}\froggerreloaded\; Flags: ignoreversion
;Source: ..\Debug\levels\basic1.lvl; DestDir: {userappdata}\froggerreloaded\levels\; Flags: ignoreversion
;Source: ..\Debug\levels\basic2.lvl; DestDir: {userappdata}\froggerreloaded\levels\; Flags: ignoreversion
;Source: ..\Debug\levels\basic3.lvl; DestDir: {userappdata}\froggerreloaded\levels\; Flags: ignoreversion
;Source: ..\Debug\levels\coolroad.lvl; DestDir: {userappdata}\froggerreloaded\levels\; Flags: ignoreversion
;Source: ..\Debug\levels\highway.lvl; DestDir: {userappdata}\froggerreloaded\levels\; Flags: ignoreversion
;Source: ..\Debug\levels\sea.lvl; DestDir: {userappdata}\froggerreloaded\levels\; Flags: ignoreversion

[Icons]
Name: {group}\Frogger Reloaded; Filename: {app}\Frogger.exe
Name: {group}\{cm:UninstallProgram,Frogger Reloaded}; Filename: {uninstallexe}
Name: {commondesktop}\Frogger Reloaded; Filename: {app}\Frogger.exe; Tasks: desktopicon

[Run]
Filename: {app}\Frogger.exe; Description: {cm:LaunchProgram,Frogger Reloaded}; Flags: nowait postinstall skipifsilent

[CustomMessages]
english.dotnetmissing=FroggerReloaded requires the .NET Framework v2.0 with ServicePack 2 or higher. Please download and install .NET framework v2.0 and run this setup again. Do you want to go to the website of .NET framework v2.0 to download .NET framework v2.0 now?
dutch.dotnetmissing=FroggerReloaded heeft .NET framework v2.0 met ServicePack2 en hoger nodig. Download en installeer .NET framework v2.0 en voer dan deze setup opnieuw uit. Wil je nu naar de website van .NET framework v2.0 gaan om .NET framework v2.0 to downloaden?

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
                Result1 := MsgBox(ExpandConstant('{cm:dotnetmissing}'),
                    mbConfirmation, MB_YESNO) = idYes;
                if Result1 =false then
                begin
                    Result:=false;
                End
                else
                begin
                    Result:=false;
                    ShellExec('open','http://download.microsoft.com/download/5/6/7/567758a3-759e-473e-bf8f-52154438565a/dotnetfx.exe','','',SW_SHOWNORMAL,ewNoWait,ErrorCode);
                end;
            end;
    end;
end;
