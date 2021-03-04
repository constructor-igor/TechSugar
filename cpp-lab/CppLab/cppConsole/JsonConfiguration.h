#pragma once
class JsonConfiguration
{
public:
    JsonConfiguration();
    int GetValue();

    CString GetDirectory();
    CString GetConfigurationFilePath();
    CString ReadFileContent();
    CString GetFullPath(CString path);
};

