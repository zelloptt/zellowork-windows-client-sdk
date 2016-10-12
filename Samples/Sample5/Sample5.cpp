#include "stdafx.h"
#include "resource.h"
#include "MainForm.h"

class CModule : public CAtlExeModuleT<CModule>
{
};

CModule _Module;

int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
	CoInitialize(0);
	AtlAxWinInit();

	CMainForm().DoModal();

	AtlAxWinTerm();
	CoUninitialize();
	return 0;
}
