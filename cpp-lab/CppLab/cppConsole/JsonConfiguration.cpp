#include <iostream>
#include <filesystem>
#include <stdlib.h>
#include <windows.h>
#include <Shlwapi.h>
#include <atlstr.h>
#include <iostream>
#include <fstream>
#include <string>

#include "JsonConfiguration.h"
#include "rapidjson/document.h"
#include "rapidjson/filereadstream.h"

#define BUFSIZE 4096
#pragma warning(disable : 4996)

using namespace rapidjson;

JsonConfiguration::JsonConfiguration()
{
}

int JsonConfiguration::GetValue()
{
    CString jsonContent = ReadFileContent();
    const TCHAR* x = (LPCTSTR)jsonContent;

    char converted[BUFSIZE];
    wcstombs(converted, x, wcslen(x) + 1);

    Document document;
    if (document.Parse(converted).HasParseError())
    {
        std::cout << "Parse failed" << std::endl;
        return -1;
    }
        
    int value = document["value"].GetInt();
    return value;
}

CString JsonConfiguration::GetDirectory()
{
    WCHAR exeFilePath[MAX_PATH];
    GetModuleFileNameW(NULL, exeFilePath, MAX_PATH);
    CString exePath(exeFilePath);

    CString exeDirectory(exeFilePath);
    PathRemoveFileSpec(exeDirectory.GetBuffer(0));
    exeDirectory.ReleaseBuffer(-1);

    return exeDirectory;
}

CString JsonConfiguration::GetConfigurationFilePath()
{
    CString jsonFile = GetDirectory() + "\\..\\..\\cppConsole\\configuration.json";
    jsonFile = GetFullPath(jsonFile);
    return jsonFile;
}

CString JsonConfiguration::ReadFileContent()
{
    CString fileContent("");
    std::string lineContent("");
    CString jsonFile = GetConfigurationFilePath();
    
    std::ifstream configurationFile(jsonFile.GetString());
    if (!configurationFile.good()) {
        std::cout << "No file found " << jsonFile.GetString() << '\n';
    }

    // Use a while loop together with the getline() function to read the file line by line
    while (std::getline(configurationFile, lineContent)) {
        // Output the text from the file
        fileContent = fileContent + CString(lineContent.c_str());
    }

    // Close the file
    configurationFile.close();
    return fileContent;
}

CString JsonConfiguration::GetFullPath(CString path)
{
    DWORD  retval = 0;
    TCHAR  buffer[BUFSIZE] = TEXT("");
    TCHAR** lppPart = { NULL };
    retval = GetFullPathName(path.GetString(), BUFSIZE, buffer, lppPart);
    return CString(buffer);
}

