#include "StdAfx.h"
#include "resourceppc.h"
#include "MainWnd.h"

CMainFrame::CMainFrame () :
	m_dlgLogin(this),
	m_bExiting (false),
	m_iMinWidth (0),
	m_iMinHeight (0)
{
}

CMainFrame::~CMainFrame ()
{
}

bool CMainFrame::PreTranslateMessage(MSG& msg)
{
	if (m_dlgLogin && m_dlgLogin.IsDialogMessage(&msg))
		return true;
	if (m_spInPlace)
		return S_OK == m_spInPlace->TranslateAccelerator(&msg);
	return false;
}

LRESULT CMainFrame::OnCreate(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	SHINITDLGINFO shidi;
	shidi.dwMask = SHIDIM_FLAGS;
	shidi.dwFlags = SHIDIF_SIZEDLGFULLSCREEN;
	shidi.hDlg = m_hWnd;
	SHInitDialog(&shidi);

	m_bExiting = false;
	if (m_wndAx.Create(m_hWnd, 0, 0, WS_CHILD | WS_VISIBLE))
	{
		HRESULT hr = m_wndAx.CreateControl(L"Ptt.Ptt");
		if (SUCCEEDED(hr))
		{
			m_wndAx.QueryControl(&m_spMesh);
			if(m_spMesh)
			{
				m_spMesh.QueryInterface(&m_spInPlace);
				ATL::CComPtr<PttLib::INetwork> spNetwork;
				m_spMesh->get_Network(&spNetwork);
				if(spNetwork)
				{
					spNetwork->put_NetworkName(ATL::CComBSTR("default"));
					spNetwork->put_LoginServer(ATL::CComBSTR("default.loudtalks.net"));
					ATL::CComQIPtr<PttLib::INetwork2> spNetwork2(spNetwork);
					if(spNetwork2)
						spNetwork2->EnableTls(ATL::CComBSTR("tls.zellowork.com"));
				}
				m_spMesh->get_Settings(&m_spSettings);
				m_spSettings->put_ShowTrayIcon(VARIANT_TRUE);
				DispEventAdvise(m_spMesh.p);
			}
		}
	}

	SHMENUBARINFO mbi = { sizeof(mbi) };
	mbi.hwndParent = m_hWnd;
	mbi.nToolBarId = IDR_MENUBAR;
	mbi.hInstRes  = GetModuleHandle(NULL);

	BOOL bRet = SHCreateMenuBar(&mbi);

	if (m_spMesh)
	{
		m_dlgLogin.Create(m_hWnd);
		RECT rc = {0};
		m_dlgLogin.GetWindowRect(&rc);
		m_iMinWidth = rc.right - rc.left;
		m_iMinHeight = rc.bottom - rc.top;
		//SetWindowPos(0, 0, 0, m_iMinWidth, m_iMinHeight * 2, SWP_NOOWNERZORDER | SWP_NOZORDER | SWP_NOMOVE);
		ManageControls();
		m_dlgLogin.AddUsername(L"test1");
		m_dlgLogin.AddUsername(L"test2");
		m_dlgLogin.AddUsername(L"test3");
		m_dlgLogin.AddUsername(L"test4");
		m_dlgLogin.AddUsername(L"test5");
		m_dlgLogin.AddUsername(L"test6");
		m_dlgLogin.AddUsername(L"test7");
		m_dlgLogin.SetUsername(L"test4");
		m_dlgLogin.SetPassword(L"test");
	} else
		DestroyWindow();
	return 0;
}

LRESULT CMainFrame::OnDestroy(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	if (m_spMesh)
		DispEventUnadvise(m_spMesh.p);
	m_spInPlace.Release();
	m_spSettings.Release();
	m_spMesh.Release();
	PostQuitMessage(0);
	return 0;
}

LRESULT CMainFrame::OnSize(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	ManageControls();
	return 0;
}

LRESULT CMainFrame::OnClose(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	// If Loudtalks Mesh control is still signed into network when
	// it's being destroyed, it'll automatically start signing out and
	// destroying of the control won't finish until the process is complete.
	// This causes GUI lockup. To avoid it, following approach is recommended.

	// If Loudtalks Mesh control is signed into network,
	// hide main window and start signing out.
	// Destroy window later when sign out process finishes and
	// appropriate event is fired.

	if (m_spSettings)
		m_spSettings->put_ShowTrayIcon(VARIANT_FALSE);
	if (m_spMesh && !m_bExiting)
	{
		PttLib::NETWORK_STATUS Status = PttLib::NSOFFLINE;
		m_spMesh->get_NetworkStatus(&Status);
		if (PttLib::NSOFFLINE != Status)
		{
			ShowWindow(SW_HIDE);
			m_bExiting = true;
			if (PttLib::NSONLINE == Status || PttLib::NSSIGNINGIN == Status)
				m_spMesh->SignOut();
			return 0;
		}
	}
	return DefWindowProc(uMsg, wParam, lParam);
}

LRESULT CMainFrame::OnSetFocus(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	if (m_dlgLogin.IsWindowVisible())
		m_dlgLogin.ShowFocus();
	else
	if (m_spMesh)
		m_wndAx.SetFocus();
	return 0;
}

LRESULT CMainFrame::OnCommand(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	switch (wID)
	{
	case ID_SIGNIN:
		OnSignIn(m_dlgLogin.GetUsername(), m_dlgLogin.GetPassword());
		break;
	case ID_SIGNOUT:
		if (m_spMesh)
			m_spMesh->SignOut();
		break;
	case ID_HISTORY:
		if (m_spMesh)
			m_spMesh->OpenHistory(NULL);
		break;
	// Settings, Feedback, About, Password Wizard are shown modally
	case ID_CHANGEPASSWORD:
		if (m_spMesh)
			m_spMesh->ShowPasswordWizard((long) m_hWnd);
		break;
	case ID_OPTIONS:
		if (m_spMesh)
			m_spMesh->ShowSettingsDialog((long) m_hWnd);
		break;
	case ID_FEEDBACK:
		if (m_spMesh)
			m_spMesh->ShowFeedbackDialog((long) m_hWnd);
		break;
	case ID_ABOUT:
/*		{
			ATL::CComQIPtr<PttLib::ISettings2> spSettings2(m_spSettings);
			if(spSettings2) {
				PttLib::IAudioDevices* pPlaybackDevices = NULL;
				PttLib::IAudioDevices* pRecordDevices = NULL;
				spSettings2->GetPlaybackDevices(&pPlaybackDevices);
				spSettings2->GetRecordingDevices(&pRecordDevices);
				int n1 ,n2;
				pPlaybackDevices->get_Count(&n1);
				pRecordDevices->get_Count(&n2);
				BSTR b1,b2;
				spSettings2->get_PlaybackDeviceId(&b1);
				spSettings2->get_RecordingDeviceId(&b2);
				for(int i=0;i<n1;++i) {
					BSTR bstrName;
					BSTR bstrId;
					pPlaybackDevices->get_Name(i, &bstrName);
					pPlaybackDevices->get_Id(i, &bstrId);
					std::wstring strName(bstrName);
					if(0==lstrcmp(b1,bstrId)) {
						strName.insert(0,L"*** ");
					}
					SysFreeString(bstrName);
					SysFreeString(bstrId);
				}
				for(int i=0;i<n2;++i) {
					BSTR bstrName;
					BSTR bstrId;
					pRecordDevices->get_Name(i, &bstrName);
					pRecordDevices->get_Id(i, &bstrId);
					std::wstring strName(bstrName);
					if(0==lstrcmp(b1,bstrId)) {
						strName.insert(0,L"*** ");
					}
					SysFreeString(bstrName);
					SysFreeString(bstrId);
				}
				int x = 0;
			}
		}
*/
		if (m_spMesh)
			m_spMesh->ShowAboutDialog((long) m_hWnd);
		break;
	case ID_EXIT:
		PostMessage(WM_CLOSE);
		break;
	default:;
	}
	return 0;
}

LRESULT CMainFrame::OnInitMenuPopup (UINT /*uMsg*/, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	if (!HIWORD (lParam))
		if (HMENU hMnu = reinterpret_cast<HMENU> (wParam))
			if (m_spMesh)
			{
				PttLib::NETWORK_STATUS Status = PttLib::NSOFFLINE;
				m_spMesh->get_NetworkStatus(&Status);
				EnableMenuItem(hMnu, ID_SIGNIN, MF_BYCOMMAND | (PttLib::NSOFFLINE == Status ? MF_ENABLED : (MF_DISABLED | MF_GRAYED)));
				EnableMenuItem(hMnu, ID_SIGNOUT, MF_BYCOMMAND | (PttLib::NSONLINE == Status ? MF_ENABLED : (MF_DISABLED | MF_GRAYED)));
				EnableMenuItem(hMnu, ID_HISTORY, MF_BYCOMMAND | (PttLib::NSONLINE == Status ? MF_ENABLED : (MF_DISABLED | MF_GRAYED)));
				EnableMenuItem(hMnu, ID_CHANGEPASSWORD, MF_BYCOMMAND | (PttLib::NSONLINE == Status ? MF_ENABLED : (MF_DISABLED | MF_GRAYED)));
			}
	return 0;
}

void CMainFrame::OnSignInStarted()
{
	ManageControls();
}

void CMainFrame::OnSignInSucceeded()
{
	ManageControls();
}

void CMainFrame::OnSignInFailed(PttLib::CLIENT_ERROR cerr)
{
	ManageControls();
}

void CMainFrame::OnSignOutStarted()
{
	ManageControls();
}

void CMainFrame::OnSignOutComplete()
{
	if (m_bExiting)
	{
		PostMessage(WM_CLOSE);
	} else
	{
		ManageControls();
	}
}

void CMainFrame::OnSignInRequested()
{
	PostMessage(WM_COMMAND, ID_SIGNIN);
}

void CMainFrame::OnGetCanSignIn(VARIANT_BOOL* pbVal)
{
	if (pbVal)
		*pbVal = (m_dlgLogin.GetUsername().empty() || m_dlgLogin.GetPassword().empty()) ? VARIANT_FALSE : VARIANT_TRUE;
}

void CMainFrame::ManageControls()
{
	if (m_spMesh)
	{
		RECT rc = {0};
		GetClientRect(&rc);
		PttLib::NETWORK_STATUS Status = PttLib::NSOFFLINE;
		m_spMesh->get_NetworkStatus(&Status);
		int iMeshTop = (Status == PttLib::NSSIGNINGIN || Status == PttLib::NSOFFLINE) ? m_iMinHeight : 0;
		m_wndAx.SetWindowPos(0, 0, iMeshTop, rc.right, rc.bottom - iMeshTop, SWP_NOOWNERZORDER | SWP_NOZORDER | SWP_NOACTIVATE);
		bool bLoginDlg = Status == PttLib::NSSIGNINGIN || Status == PttLib::NSOFFLINE;
		m_dlgLogin.SetWindowPos(0, (rc.right - m_iMinWidth) / 2, 0, 0, 0,
			SWP_NOOWNERZORDER | SWP_NOZORDER | SWP_NOSIZE | SWP_NOACTIVATE | (bLoginDlg ? SWP_SHOWWINDOW : SWP_HIDEWINDOW));
		m_dlgLogin.EnableControls(Status == PttLib::NSOFFLINE);
	}
}

void CMainFrame::OnSignIn(const std::wstring& sUsername, const std::wstring& sPassword)
{
	if (m_spMesh)
		m_spMesh->SignIn(CComBSTR(sUsername.c_str()), CComBSTR(sPassword.c_str()), VARIANT_FALSE);
}
