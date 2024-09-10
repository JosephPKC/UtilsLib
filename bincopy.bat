SET source=%~dp0
SET dest=%1

xcopy /e /i "%source%bin" "%dest%"