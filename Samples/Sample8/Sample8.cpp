#include "stdafx.h"
#include "resource.h"
#include "MainForm.h"

CComModule _Module;

int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
	CoInitializeEx(NULL,COINIT_MULTITHREADED);
	{
		CMainForm().DoModal();
	}
	CoUninitialize();
	return 0;
}
