// cppConsole.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <filesystem>
#include <stdlib.h>
#include <windows.h>
#include <Shlwapi.h>
#include <atlstr.h>
#include "JsonConfiguration.h"

int main()
{
    std::cout << "Hello World!\n";

    JsonConfiguration configuration = JsonConfiguration();
    std::cout << "value: " << configuration.GetValue() << std::endl;

    CString directoryPath = configuration.GetDirectory();
    std::wcout << "directoryPath: " << directoryPath.GetString() << std::endl;

    CString jsonFile = configuration.GetConfigurationFilePath();
    std::wcout << "jsonFile: " << jsonFile.GetString() << std::endl;

    CString fullPath = configuration.GetFullPath(jsonFile);
    std::wcout << "fullPath: " << fullPath.GetString() << std::endl;

    CString jsonContent = configuration.ReadFileContent();
    std::wcout << "jsonContent: " << jsonContent.GetString() << std::endl;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
