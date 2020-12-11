; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#include <idp.iss>

#define Major
#define Minor
#define Build
#define Dummy
#define ApplicationName "CCS7Manager"
#define BaseDir  ".\"
#define BuildDir BaseDir + "\bin\Release\"
#define ApplicationExeName "CCS7Manager.exe"
#define ApplicationFullPath BuildDir + ApplicationExeName
#expr ParseVersion(ApplicationFullPath, Major, Minor,Build,Dummy)
#define ApplicationVersion Str(Major) + "." + Str(Minor) + "." + Str(Build)
#define ApplicationPublisher "F4IKZ"
#define ApplicationURL "https://bitbucket.org/Artemia/ccs7manager"
#define InstallFileName "CCS7Manager_" + str(Major) +"_" + str(Minor) + "_" + str(Build) + "_win"

[Code]
function Framework45IsNotInstalled(): Boolean;
var
  bSuccess: Boolean;
  regVersion: Cardinal;
begin
  Result := True;
bSuccess := RegQueryDWordValue(HKLM, 'Software\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', regVersion);
  if (True = bSuccess) and (regVersion >= 378389) then begin
    Result := False;
  end;
end;

procedure InitializeWizard;
begin
  if Framework45IsNotInstalled() then
  begin
    idpAddFile('http://go.microsoft.com/fwlink/?LinkId=397707', ExpandConstant('{tmp}\NetFrameworkInstaller.exe'));
    idpDownloadAfter(wpReady);
  end;
end;

procedure InstallFramework;
var
  StatusText: string;
  ResultCode: Integer;
begin
  StatusText := WizardForm.StatusLabel.Caption;
  WizardForm.StatusLabel.Caption := 'Installing .NET Framework 4.5.2. This might take a few minutes...';
  WizardForm.ProgressGauge.Style := npbstMarquee;
  try
    if not Exec(ExpandConstant('{tmp}\NetFrameworkInstaller.exe'), '/passive /norestart', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
    begin
      MsgBox('.NET installation failed with code: ' + IntToStr(ResultCode) + '.', mbError, MB_OK);
    end;
  finally
    WizardForm.StatusLabel.Caption := StatusText;
    WizardForm.ProgressGauge.Style := npbstNormal;
DeleteFile(ExpandConstant('{tmp}\NetFrameworkInstaller.exe'));
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  case CurStep of
    ssPostInstall:
      begin
        if Framework45IsNotInstalled() then
        begin
          InstallFramework();
        end;
      end;
  end;
end;

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{c49f4199-c4b3-4f8a-9841-ba391c78f51d}}
AppName={#ApplicationName}
AppVerName={#ApplicationName} {#ApplicationVersion}
AppPublisher={#ApplicationPublisher}
AppPublisherURL={#ApplicationURL}
AppSupportURL={#ApplicationURL}
AppUpdatesURL={#ApplicationURL}
DefaultDirName={pf}\{#ApplicationName}
DisableProgramGroupPage=yes
LicenseFile=LICENSE.GPLv3
InfoBeforeFile=changelog.txt
OutputDir=.\
OutputBaseFilename= {#InstallFileName}
SetupIconFile=satellite.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "{#ApplicationFullPath}"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BuildDir}Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#ApplicationName}"; Filename: "{app}\{#ApplicationExeName}"
Name: "{commondesktop}\{#ApplicationName}"; Filename: "{app}\{#ApplicationExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#ApplicationName}"; Filename: "{app}\{#ApplicationExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#ApplicationExeName}"; Description: "{cm:LaunchProgram,{#StringChange(ApplicationName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

