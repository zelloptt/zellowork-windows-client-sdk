#include "stdafx.h"
#include "resourceppc.h"
#include "MainWnd.h"

class CModule : public CAtlExeModuleT<CModule>
{
};

CModule _Module;

extern "C" int WINAPI _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nShowCmd)
{
	CoInitializeEx(0, COINIT_APARTMENTTHREADED);
	AtlAxWinInit();

	MSG msg;
	PeekMessage(&msg, 0, 0, 0, PM_NOREMOVE); // Create thread's message queue
	TCHAR tszCaption[64] = {0};
	LoadString(GetModuleHandle(0), IDS_CAPTION, tszCaption, _countof(tszCaption));
	CMainFrame wndMain;
	if (wndMain.Create(0, 0, tszCaption, 0, 0, LoadMenu(GetModuleHandle(0), MAKEINTRESOURCE(IDR_MENU))))
	{
		wndMain.ShowWindow((nShowCmd == SW_MAXIMIZE) ? SW_SHOW : nShowCmd);
		SetForegroundWindow(wndMain);
		while (GetMessage(&msg, 0, 0, 0) > 0)
			if(!wndMain.PreTranslateMessage(msg))
			{
				TranslateMessage(&msg);
				DispatchMessage(&msg);
			}
	}

	AtlAxWinTerm();
	CoUninitialize();
	return 0;
}
