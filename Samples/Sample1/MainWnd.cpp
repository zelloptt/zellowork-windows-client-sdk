#include "StdAfx.h"
#include "MainWnd.h"
#include <atlsafe.h>
#include "AudioDlg.h"

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
	m_bExiting = false;
	if (m_wndAx.Create(m_hWnd, 0, 0, WS_CHILD | WS_VISIBLE | WS_CLIPCHILDREN | WS_CLIPSIBLINGS))
	if (SUCCEEDED(m_wndAx.CreateControl(L"Ptt.Ptt")))
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
			}
			ATL::CComQIPtr<PttLib::INetwork2> spNetwork2(spNetwork);
			if(spNetwork2)
				spNetwork2->EnableTls(ATL::CComBSTR("tls.zellowork.com"));
			m_spMesh->get_Settings(&m_spSettings);
			m_spSettings->put_ShowTrayIcon(VARIANT_TRUE);
			DispEventAdvise(m_spMesh.p);

			// OEM configuration is located in resource section
			if(HRSRC hResource = FindResource(GetModuleHandle(0), MAKEINTRESOURCE(IDR_OEM_CONFIG), _T("OEM_CONFIG")))
				if(HGLOBAL hGlobal = LoadResource(GetModuleHandle(0), hResource))
					if(DWORD nOemConfig = SizeofResource(GetModuleHandle(0), hResource))
						if(const void* pOemConfig = LockResource(hGlobal))
						{
							ATL::CComSafeArray<unsigned char> Data;
							Data.Add (nOemConfig, (unsigned char*) pOemConfig);
							ATL::CComPtr<PttLib::ICustomization> spCustomization;
							m_spMesh->get_Customization(&spCustomization);
							SAFEARRAY* pArray = Data;
							spCustomization->put_OemConfigData(&pArray);
							UnlockResource(hGlobal);
						}
		}
	}
	if (m_spMesh)
	{
		m_dlgLogin.Create(m_hWnd);
		RECT rc = {0};
		m_dlgLogin.GetWindowRect(&rc);
		AdjustWindowRect (&rc, GetWindowLong (GWL_STYLE), TRUE);
		m_iMinWidth = rc.right - rc.left;
		m_iMinHeight = rc.bottom - rc.top + 100;
		SetWindowPos(0, 0, 0, m_iMinWidth, m_iMinHeight, SWP_NOOWNERZORDER | SWP_NOZORDER | SWP_NOMOVE);
		ManageControls();
		ManageMenu();
		m_dlgLogin.AddUsername(L"test1");
		m_dlgLogin.AddUsername(L"test2");
		m_dlgLogin.AddUsername(L"test3");
		m_dlgLogin.AddUsername(L"test4");
		m_dlgLogin.AddUsername(L"test5");
		m_dlgLogin.AddUsername(L"test6");
		m_dlgLogin.AddUsername(L"test7");
		m_dlgLogin.SetUsername(L"test1");
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

LRESULT CMainFrame::OnGetMinMaxInfo(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	if (MINMAXINFO* p = reinterpret_cast<MINMAXINFO*>(lParam))
	{
		p->ptMinTrackSize.x = m_iMinWidth;
		p->ptMinTrackSize.y = m_iMinHeight;
	}
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
	case ID_NORMAL:
	case ID_COMPACT:
		if (m_spSettings)
			m_spSettings->put_CompactContactList(ID_COMPACT == wID ? VARIANT_TRUE : VARIANT_FALSE);
		ManageMenu();
		m_spMesh->UpdateContacts();
		break;
	case ID_CHANGEPASSWORD:
		if (m_spMesh)
			m_spMesh->ShowPasswordWizard(0);
		break;
	case ID_TOOLS_CHANGEAUDIODEVICES:
		{
			ATL::CComQIPtr<PttLib::ISettings2> spSettings2(m_spSettings);
			if(spSettings2) {
				CChangeAudioDlg dlg(spSettings2);
				dlg.DoModal();
			}
		}
		break;
	case ID_OPTIONS:
		if (m_spMesh)
			m_spMesh->ShowSettingsDialog(0);
		break;
	case ID_WEBHELP:
		ShellExecute(0, _T("open"), _T("http://support.zello.com/categories/20031828-Zello-Work"), 0, 0, SW_SHOW);
		break;
	case ID_REPORTAPROBLEM:
		if (m_spMesh)
			m_spMesh->ShowFeedbackDialog(0);
		break;
	case ID_ABOUT:
		if (m_spMesh)
			m_spMesh->ShowAboutDialog(0);
		break;
	default:;
	}
	return 0;
}

void CMainFrame::OnSignInStarted()
{
	ManageControls();
	ManageMenu();
}

void CMainFrame::OnSignInSucceeded()
{
	ManageControls();
	ManageMenu();
	if (m_spMesh)
	{
		ATL::CComPtr<PttLib::IContacts> spContacts;
		if (SUCCEEDED(m_spMesh->get_Contacts(&spContacts)) && spContacts)
		{
			ATL::CComPtr<PttLib::IContact> spContact;
			int nCount = 0;
			if (SUCCEEDED(spContacts->get_Count(&nCount)) && nCount > 0 &&
				SUCCEEDED(spContacts->get_Item(0, &spContact)) && spContact)
			{
				m_spMesh->SelectContact(spContact);
			}
		}
	}
}

void CMainFrame::OnSignInFailed(PttLib::CLIENT_ERROR cle)
{
	ManageControls();
	ManageMenu();
}

void CMainFrame::OnSignOutStarted()
{
	ManageControls();
	ManageMenu();
}

void CMainFrame::OnSignOutComplete()
{
	if (m_bExiting)
	{
		PostMessage(WM_CLOSE);
	} else
	{
		ManageControls();
		ManageMenu();
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

void CMainFrame::OnIncomingMessage(PttLib::IMessage* pMsg)
{
	PttLib::MESSAGE_TYPE mt;
	pMsg->get_Type(&mt);
	VARIANT_BOOL vb = VARIANT_FALSE;
	pMsg->get_Incoming(&vb);
}

void CMainFrame::OnMessageBegin(PttLib::IAudioInMessage* pMsg,VARIANT_BOOL* pbCanBeActivated)
{
	CComPtr<PttLib::IContact> spSender;
	CComPtr<PttLib::IContact> spAuthor;
	pMsg->get_Sender(&spSender.p);
	pMsg->get_Author(&spAuthor.p);
	if(spAuthor!=NULL) {
		BSTR bstr;
		spAuthor->get_Name(&bstr);
		SysFreeString(bstr);
	}
	*pbCanBeActivated = VARIANT_TRUE;
}

void CMainFrame::ManageControls()
{
	if (m_spMesh)
	{
		RECT rc = {0};
		GetClientRect(&rc);
		RECT rcLogin = {0};
		m_dlgLogin.GetClientRect (&rcLogin);
		PttLib::NETWORK_STATUS Status = PttLib::NSOFFLINE;
		m_spMesh->get_NetworkStatus(&Status);
		int iMeshTop = (Status == PttLib::NSSIGNINGIN || Status == PttLib::NSOFFLINE) ? rcLogin.bottom : 0;
		m_wndAx.SetWindowPos(0, 0, iMeshTop, rc.right, rc.bottom - iMeshTop, SWP_NOOWNERZORDER | SWP_NOZORDER);
		m_dlgLogin.SetWindowPos(0, (rc.right - rcLogin.right) / 2, 0, 0, 0, SWP_NOOWNERZORDER | SWP_NOZORDER | SWP_NOSIZE);
		m_dlgLogin.ShowWindow((Status == PttLib::NSSIGNINGIN || Status == PttLib::NSOFFLINE) ? SW_SHOW : SW_HIDE);
		m_dlgLogin.EnableControls(Status == PttLib::NSOFFLINE);
	}
}

void CMainFrame::ManageMenu()
{
	if (m_spMesh)
	{
		PttLib::NETWORK_STATUS Status = PttLib::NSOFFLINE;
		m_spMesh->get_NetworkStatus(&Status);
		VARIANT_BOOL bCompact = VARIANT_FALSE;
		if (m_spSettings)
			m_spSettings->get_CompactContactList(&bCompact);
		HMENU hMnu = GetMenu();
		EnableMenuItem(hMnu, ID_SIGNIN, MF_BYCOMMAND | (PttLib::NSOFFLINE == Status ? MF_ENABLED : (MF_DISABLED | MF_GRAYED)));
		EnableMenuItem(hMnu, ID_SIGNOUT, MF_BYCOMMAND | (PttLib::NSONLINE == Status ? MF_ENABLED : (MF_DISABLED | MF_GRAYED)));
		EnableMenuItem(hMnu, ID_HISTORY, MF_BYCOMMAND | (PttLib::NSONLINE == Status ? MF_ENABLED : (MF_DISABLED | MF_GRAYED)));
		EnableMenuItem(hMnu, ID_CHANGEPASSWORD, MF_BYCOMMAND | (PttLib::NSONLINE == Status ? MF_ENABLED : (MF_DISABLED | MF_GRAYED)));
		CheckMenuItem(hMnu, ID_NORMAL, MF_BYCOMMAND | (VARIANT_TRUE != bCompact ? MF_CHECKED : MF_UNCHECKED));
		CheckMenuItem(hMnu, ID_COMPACT, MF_BYCOMMAND | (VARIANT_TRUE == bCompact ? MF_CHECKED : MF_UNCHECKED));
	}
}

void CMainFrame::OnSignIn(const std::wstring& sUsername, const std::wstring& sPassword)
{
	if (m_spMesh)
		m_spMesh->SignIn(CComBSTR(sUsername.c_str()), CComBSTR(sPassword.c_str()), VARIANT_FALSE);
}
