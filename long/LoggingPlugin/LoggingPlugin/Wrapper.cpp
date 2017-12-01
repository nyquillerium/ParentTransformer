#include "stdafx.h"
#include "Wrapper.h"
#include <Windows.h>
#include <string>
#include <fstream>
#include <iostream>
#include <vector>


using namespace std;

char* HelloWorld() 
{
	return "Hello World!";
}

void Log(char *a_Objectname, char *a_Item, char *a_Value)
{
	CreateDirectoryA(a_Objectname, NULL);
	if (GetLastError() == ERROR_PATH_NOT_FOUND)
	{
		std::string l_ErrorMsg = "Error creating directory: ";
		l_ErrorMsg += a_Objectname;
		std::string ErrorFile = "ErrorOutput.txt";
		std::ofstream Out(ErrorFile);
		Out << l_ErrorMsg;
		Out.close();
		return;
	}
	///
	std::string l_Dir(a_Objectname);
	l_Dir += "/";
	l_Dir += a_Item;
	l_Dir += ".txt";

	std::ofstream l_Out;
	l_Out.open(l_Dir, std::ios_base::app);
	l_Out << a_Value << std::endl;
	l_Out.close();

}

char* LoadFile(char *FileName)
{
	ifstream diag(FileName);
	std::string str;
	std::string file_contents;
	while (std::getline(diag, str))
	{
		file_contents += str;
		file_contents.push_back('\n');
	}
	return &file_contents[0u];
}

